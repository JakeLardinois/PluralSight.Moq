using Moq;
using NUnit.Framework;
using PluralSight.Moq.Code.Demo08;

namespace PluralSight.Moq.Tests.Demo08
{
    public class CustomerServiceTests
    {
        [TestFixture]
        public class When_creating_a_customer_which_has_an_invalid_address
        {
            [Test]
            public void an_exception_should_be_raised()
            {
                /**Arrange**/

                var mockCustomerRepository = new Mock<ICustomerRepository>();
                var mockCustomerAddressFactory = new Mock<ICustomerAddressFactory>();

                mockCustomerAddressFactory
                    .Setup(x => x.From(It.IsAny<CustomerToCreateDto>())) //When any CustomerToCreateDto is passed into the From Method of the CustomerAddressFactory Class
                    .Throws<InvalidCustomerAddressException>(); //an InvalidCustomerAddressException is thrown...

                var customerService = new CustomerService(
                    mockCustomerRepository.Object, 
                    mockCustomerAddressFactory.Object);

                /**Act**/
                /**Assert**/

                //In the CustomerService Class there is a Try Catch that catches the above InvalidCustomerAddressException and then throws a CustomerCreationException
                //That we are testing for here...
                Assert.That(() => customerService.Create(new CustomerToCreateDto()),
                    Throws.TypeOf<CustomerCreationException>());
            }
        }
    }
}