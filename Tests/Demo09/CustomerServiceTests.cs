using Moq;
using NUnit.Framework;
using PluralSight.Moq.Code.Demo09;

namespace PluralSight.Moq.Tests.Demo09
{
    public class CustomerServiceTests
    {
        [TestFixture]
        public class When_creating_a_customer
        {
            [Test]
            public void the_local_timezone_should_be_set()
            {
                /**Arrange**/

                //We want to test that the LocalTimeZone property has been set on the CustomerRepository Class in the CustomerService Class.

                var mockCustomerRepository = new Mock<ICustomerRepository>();

                var customerService = new CustomerService(
                    mockCustomerRepository.Object);

                /**Act**/

                customerService.Create(new CustomerToCreateDto());

                /**Assert**/

                mockCustomerRepository.VerifySet( //this method is used to verify that LocalTimeZone on the CustomerRepository object is set.
                    x =>x.LocalTimeZone = It.IsAny<string>());
            }
        }
    }
}