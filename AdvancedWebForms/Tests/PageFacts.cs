using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Moq;
using Xunit;

namespace Tests
{
    public class PageFacts
    {
        [Fact]
        public void GetSelectedCategory_GetsCategoryFromQueryString()
        {
            // Arrange
            var httpContext = new Mock<HttpContextBase>();
            var httpRequest = new Mock<HttpRequestBase>();
            var httpResponse = new Mock<HttpResponseBase>();
            var httpSession = new Mock<HttpSessionStateBase>();
            var httpApplication = new Mock<HttpApplicationStateBase>();

            httpRequest.Setup(r => r.QueryString).Returns(new NameValueCollection { { "category", "Test" } });

            // Act
            var page = new AdvancedWebForms.UnitTesting.Default(httpContext.Object, httpRequest.Object, httpResponse.Object, httpSession.Object, httpApplication.Object);
            var category = page.GetSelectedCategory();

            // Assert
            Assert.Equal("Test", category);
        }
    }
}
