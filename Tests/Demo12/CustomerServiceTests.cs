using Moq;
using NUnit.Framework;
using PluralSight.Moq.Code.Demo12;

namespace PluralSight.Moq.Tests.Demo12
{
    public class CustomerServiceTests
    {
        [TestFixture]
        public class When_creating_a_customer
        {
            [Test]
            public void the_workstation_id_should_be_retrieved()
            {
                /**Arrange**/

                //Take a look at stubbing out properties on Mock objects and setting initial values for those properties.


                var mockCustomerRepository = new Mock<ICustomerRepository>();
                var mockApplicationSettings = new Mock<IApplicationSettings>();

                //Sets the WorkstationId as a stub property.
                //mockApplicationSettings.SetupProperty(x => x.WorkstationId);
                //Sets the return value.
                //mockApplicationSettings.Object.WorkstationId = 2345;

                //Sets the WorkstationId as a stub property AND sets the initial return value.
                //mockApplicationSettings.SetupProperty(x => x.WorkstationId, 1234);

                //This says that every property on that Mockable object is stubbable and can be set
                mockApplicationSettings.SetupAllProperties();
                mockApplicationSettings.Object.WorkstationId = 2345;


                var customerService = new CustomerService(
                    mockCustomerRepository.Object, 
                    mockApplicationSettings.Object);

                /**Act**/

                customerService.Create(new CustomerToCreateDto());

                /**Assert**/

                mockApplicationSettings.VerifyGet(x=>x.WorkstationId);
            }
        }
    }
}