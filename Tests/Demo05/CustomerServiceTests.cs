using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using PluralSight.Moq.Code.Demo05;

namespace PluralSight.Moq.Tests.Demo05
{
    public class CustomerServiceTests
    {
        [TestFixture]
        public class When_creating_a_customer
        {
            [Test]
            public void each_customer_should_be_assigned_an_id()
            {
                /**Arrange**/

                var listOfCustomersToCreate = new List<CustomerToCreateDto>
                                                  {
                                                      new CustomerToCreateDto(),
                                                      new CustomerToCreateDto()
                                                  };

                var mockCustomerRepository = new Mock<ICustomerRepository>();
                var mockIdFactory = new Mock<IIdFactory>();

                //We want to test that each customer gets a unique Id
                var i = 1;
                mockIdFactory.Setup(x => x.Create()) //the IdFactory.Create takes in no parameters...
                    .Returns(() => i) //returns a value of 1 the first time it is called.
                    .Callback(() => i++); //this is how you handle subsequent calls of the Create method...  So if you didn't specify this, then 
                                          //the Create method would just always return 1.

                var customerService = new CustomerService(
                    mockCustomerRepository.Object, mockIdFactory.Object);

                /**Act**/

                customerService.Create(listOfCustomersToCreate);

                /**Assert**/

                //Make sure that the Create method of the IdFactory is called at least 1 time
                //mockIdFactory.Verify(x => x.Create(), Times.AtLeastOnce());

                //This is a better test because it tests that the IdFactory's Create method (which is used to give the Id to the customer)
                //Is called once for each of the Customers that it creates, thereby ensuring that they have a unique Id.
                mockIdFactory.Verify(x => x.Create(), Times.Exactly(listOfCustomersToCreate.Count));
            }
        }
    }
}
 
 