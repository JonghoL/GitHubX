using GitHubX.ViewModels;
using System;
using System.Linq;
using Xunit;

namespace GitHubX.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void TestMethod1()
        {
            var target = new TestViewModel();
            var ret = target.Sum(1, 2);

            Assert.Equal(3, ret);
        }
    }
}
