using GitHubX.ViewModels;
using Splat;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GitHubX.Tests
{
    public class LocatorFixture : IDisposable
    {
        public LocatorFixture()
        {
            Locator.CurrentMutable.RegisterConstant(new DefaultViewModelConstructor(), typeof(IViewModelConstructor));
        }

		public void Register(Func<object> factory, Type serviceType)
        {
            Locator.CurrentMutable.Register(factory, serviceType);
        }

        public void Dispose()
        {
            Locator.Current.Dispose();
        }
    }

    public class ViewModelConstructorTests : IUseFixture<LocatorFixture>
    {
        LocatorFixture fixture;
        public void SetFixture(LocatorFixture data)
        {
            this.fixture = data;
        }

        [Fact]
        public void NotRegisterd_ThrowException()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
                 ViewModelConstructor.Current.Construct<DummyViewModel>());

            Assert.NotNull(ex);
            Assert.Equal(typeof(IDummyService).FullName, ex.ParamName);
        }

		[Fact]
		public void VerifyDependencyInjection()
        {
            fixture.Register(()=> new DummyService(), typeof(IDummyService));
            var ret = ViewModelConstructor.Current.Construct<DummyViewModel>();

            Assert.NotNull(ret);
            Assert.NotNull(ret.DummyService);
        }
    }

    public class DummyViewModel
    {
        public IDummyService DummyService { get; protected set; }
        public DummyViewModel(IDummyService dummyService)
        {
            this.DummyService = dummyService;
        }
    }

    public interface IDummyService { }

    public class DummyService : IDummyService { }
}
