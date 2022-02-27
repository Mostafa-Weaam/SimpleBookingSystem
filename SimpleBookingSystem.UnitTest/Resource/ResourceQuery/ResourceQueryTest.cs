using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Xunit;

namespace SimpleBookingSystem.UnitTest.Resource
{
    public class ResourceQueryTest
    {
        [Fact]
        public async Task GetResources_ReturnResourcesList() 
        {
            //Arrange
            var resourceServies = TestFactory.ResourceServiceTestFactory();

            //Act
            var resources = await resourceServies.GetResourcesAsync();

            //Assert
            Assert.IsNotNull(resources);
            Assert.IsTrue(resources.Count > 0);
        }
    }
}
