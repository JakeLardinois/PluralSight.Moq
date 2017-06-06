using Moq;
using NUnit.Framework;
using PluralSight.Moq.Code.Demo04;

namespace PluralSight.Moq.Tests.Demo04
{
    public class CustomerServiceTests
    {
        [TestFixture]
        public class When_creating_a_customer
        {
            [Test]
            public void the_customer_should_be_persisted()
            {
                /**Arrange**/

                var mockCustomerRepository = new Mock<ICustomerRepository>();
                var mockMailingAddressFactory = new Mock<IMailingAddressFactory>();

                //What we're doing here is defining the MailingAddress object that will be returned by the out parameter
                //of the MailingAddressFactory
                var mailingAddress = new MailingAddress {Country = "Canada"};
                //Now setup the mockMailingAddressFactory so that when it's TryParse method is called and It is passed Any Type of String,
                //that it will return the mailingAddress object that was created above.
                mockMailingAddressFactory
                    .Setup(x => x.TryParse(It.IsAny<string>(), out mailingAddress))
                    .Returns(true); //The value returned from the TryParse method is to be true (note that we don't use this value in CustomerService
                                    //and instead use 'if (mailingAddress == null)', but we could!

                var customerService = new CustomerService(
                    mockCustomerRepository.Object, mockMailingAddressFactory.Object);

                /**Act**/

                customerService.Create(new CustomerToCreateDto());

                /**Assert**/

                mockCustomerRepository.Verify(x=>x.Save(It.IsAny<Customer>()));
            }
        }
    }
}