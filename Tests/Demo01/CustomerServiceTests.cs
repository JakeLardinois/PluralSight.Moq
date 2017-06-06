using Moq;
using NUnit.Framework;
using PluralSight.Moq.Code.Demo01;

namespace PluralSight.Moq.Tests.Demo01
{
    public class CustomerServiceTests
    {
        [TestFixture]
        public class When_creating_a_customer
        {
            //shows the basic arrange, act, assert pattern
            //shows the simple verification of a method call
            [Test]
            public void the_repository_save_should_be_called()
            {
                /**Arrange**/

                var mockRepository = new Mock<ICustomerRepository>();

                //Checks if the .Save method is called by the repository on any customer
                mockRepository.Setup(x => x.Save(It.IsAny<Customer>()));  

                var customerService = new CustomerService(mockRepository.Object);

                /**Act**/

                //The create method of the Service should call the Save method on the Repository.
                customerService.Create(new CustomerToCreateDto());

                /**Assert**/

                mockRepository.VerifyAll();
            }            
        }
    }
}