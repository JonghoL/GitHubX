using GitHubX.Utils;
using Splat;
using System;
using System.Reflection;
using System.Linq;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace GitHubX
{
    public static class ViewModelConstructor
	{
        public static IViewModelConstructor Current
        {
            get
            {
                var ret = Locator.Current.GetService<IViewModelConstructor>();
                if (ret == null)
                {
                    throw new Exception("Could not find a default ViewModelConstructor. This should never happen, your dependency resolver is broken");
                }
                return ret;
            }
        }
	}

    class DefaultViewModelConstructor : IViewModelConstructor
    {
        private delegate object ObjectConstructor(params object[] parameters);
        private static readonly Cache<ConstructorInfo, ObjectConstructor> _ObjectConstructorCache = new Cache<ConstructorInfo, ObjectConstructor>();

        public object Construct(Type type)
        {
            var info = type.GetTypeInfo();
            var constructor = info.DeclaredConstructors.First();
            var parameters = constructor.GetParameters();
            var args = new List<object>(parameters.Length);
            foreach (var p in parameters)
            {
                var argument = Locator.Current.GetService(p.ParameterType);
                if (argument == null)
                {
                    throw new ArgumentNullException(p.ParameterType.FullName);
                }
                args.Add(argument);
            }

            var objConstructor = CreateObjectConstructionDelegateWithCache(constructor);

            return objConstructor.Invoke(args.ToArray());
        }

        private static ObjectConstructor CreateObjectConstructionDelegateWithCache(ConstructorInfo constructor)
        {
            ObjectConstructor objectConstructor;
            if (_ObjectConstructorCache.TryGetValue(constructor, out objectConstructor))
                return objectConstructor;

            var constructorParams = constructor.GetParameters();
            var lambdaParams = Expression.Parameter(typeof(object[]), "parameters");
            var newParams = new Expression[constructorParams.Length];

            for (int i = 0; i < constructorParams.Length; i++)
            {
                var paramsParameter = Expression.ArrayIndex(lambdaParams, Expression.Constant(i));
                var curType = constructorParams[i].ParameterType;
                newParams[i] = Expression.Convert(paramsParameter, curType);
            }

            var newExpression = Expression.New(constructor, newParams);
            var constructionLambda = Expression.Lambda(typeof(ObjectConstructor), newExpression, lambdaParams);
            objectConstructor = (ObjectConstructor)constructionLambda.Compile();

            _ObjectConstructorCache.Add(constructor, objectConstructor);

            return objectConstructor;
        }
    }

    public static class IViewModelConstructorExtensions
    {
        public static T Construct<T>(this IViewModelConstructor viewModelConstructor)
        {
            return (T)viewModelConstructor.Construct(typeof(T));
        }
    }
}

