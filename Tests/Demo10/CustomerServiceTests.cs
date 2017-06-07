using Moq;
using NUnit.Framework;
using PluralSight.Moq.Code.Demo10;

namespace PluralSight.Moq.Tests.Demo10
{
    public class CustomerServiceTests
    {
        [TestFixture]
        public class When_creating_a_customer
        {
            [Test]
            public void the_workstation_id_should_be_used()
            {
                /**Arrange**/

                //We want to test that the Get is called on WorkStationId Property

                var mockCustomerRepository = new Mock<ICustomerRepository>();
                var mockApplicationSettings = new Mock<IApplicationSettings>();

                //the WorkstationId property of the ApplicationSettings class is nullable, so we set a value here
                mockApplicationSettings
                    .Setup(x => x.WorkstationId)
                    .Returns(123);
                
                var customerService = new CustomerService(
                    mockCustomerRepository.Object, 
                    mockApplicationSettings.Object);

                /**Act**/

                customerService.Create(new CustomerToCreateDto());

                /**Assert**/

                mockApplicationSettings.VerifyGet(x=>x.WorkstationId); //VerifyGet is used to check if the WorkstationId property has been "read"
            }
        }
    }
}