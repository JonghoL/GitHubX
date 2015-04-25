using System;
using Splat;
using System.Diagnostics;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace GitHubX
{
	public class ServiceConstructor : IServiceConstructor
	{
		public object Construct(Type type)
		{
			var info = type.GetTypeInfo ();
			var constructor = info.DeclaredConstructors.First();
			var parameters = constructor.GetParameters();
			var args = new List<object>(parameters.Length);
			foreach (var p in parameters)
			{
				var argument = Locator.Current.GetService(p.ParameterType);
				if (argument == null)
					Debugger.Break();
				args.Add(argument);
			}
			return Activator.CreateInstance(type, args.ToArray());
		}
	}

	public static class ServiceConstructorExtensions
	{
		public static T Construct<T>(this IServiceConstructor serviceConstructor)
		{
			return (T)serviceConstructor.Construct(typeof(T));
		}
	}
}

