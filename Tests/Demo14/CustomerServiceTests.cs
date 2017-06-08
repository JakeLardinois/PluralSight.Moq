using Moq;
using NUnit.Framework;
using PluralSight.Moq.Code.Demo14;

namespace PluralSight.Moq.Tests.Demo14
{
    public class CustomerServiceTests
    {
        [TestFixture]
        public class When_creating_a_customer
        {
            [Test]
            public void the_customer_should_be_saved()
            {
                /**Arrange**/

                //Differences between Strict and Loose behaviour in the Moq Framework.

                //This will cause the test to fail because inside of CustomerService the FetchAll method is called against the CustomerRepository and there
                //isn't an explicit setup called for the FetchAll method...
                var mockCustomerRepository =
                    new Mock<ICustomerRepository>(MockBehavior.Strict);

                //Here the test will pass because the default behaviour of a Moq object is Loose
                //var mockCustomerRepository =
                //    new Mock<ICustomerRepository>();

                //Explicit setup for the Save method of the CustomerRepository
                mockCustomerRepository.Setup(x => x.Save(It.IsAny<Customer>()));

                var customerService = new CustomerService(
                    mockCustomerRepository.Object);

                /**Act**/

                customerService.Create(new CustomerToCreateDto());

                /**Assert**/

                mockCustomerRepository.Verify();
            }
        }
    }
}