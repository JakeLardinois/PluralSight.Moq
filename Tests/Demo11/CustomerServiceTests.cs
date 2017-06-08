using Moq;
using NUnit.Framework;
using PluralSight.Moq.Code.Demo11;

namespace PluralSight.Moq.Tests.Demo11
{
    public class CustomerServiceTests
    {
        [TestFixture]
        public class When_creating_a_customer
        {
            [Test]
            public void the_workstationid_should_be_retrieved()
            {
                /**Arrange**/

                //Testing the scenario where there is a set of deep nested objects that needs to be tested.
                //Mockable objects can be automatically created for Interfaces and Classes with Virtual Methods & Properties on them


                var mockCustomerRepository = new Mock<ICustomerRepository>();
                var mockApplicationSettings = new Mock<IApplicationSettings>();

                /*In other frameworks, what you would need to do is Mock out the ApplicationSettings and have it return a mock object when the SystemConfiguration is called
                 * and have that return a mock object when its AuditingInformation property is called and have that AuditingInformation mock object return the WorkstationId...
                 * But in the Mock framework, its MUCH easier... The mock framework automatically creates the mock objects for the below; it realizes the below objects are mockable
                 * because in this case they are interfaces*/
                mockApplicationSettings.Setup(
                    x => x.SystemConfiguration.AuditingInformation.WorkstationId)
                    .Returns(123);
                
                var customerService = new CustomerService(mockCustomerRepository.Object, mockApplicationSettings.Object);

                /**Act**/

                customerService.Create(new CustomerToCreateDto());

                /**Assert**/

                mockApplicationSettings.VerifyGet(
                    x => x.SystemConfiguration.AuditingInformation.WorkstationId);
            }
        }
    }
}