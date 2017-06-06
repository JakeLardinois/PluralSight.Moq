using System;
using Moq;
using NUnit.Framework;
using PluralSight.Moq.Code.Demo06;

namespace PluralSight.Moq.Tests.Demo06
{
    public class CustomerServiceTests
    {
        [TestFixture]
        public class When_creating_a_customer
        {
            //verify that specific parameter values are passed to the mock object
            [Test]
            public void a_full_name_should_be_created_from_first_and_last_name()
            {
                //Here is a test that ensures the correct values are being passed to the CustomerFullNameBuilder dependency on its From Method inside of the
                //CustomerService's Create method
                /**Arrange**/

                var customerToCreateDto = new CustomerToCreateDto
                                              {
                                                  FirstName = "Bob", 
                                                  LastName = "Builder"
                                              };

                var mockCustomerRepository = new Mock<ICustomerRepository>();
                var mockFullNameBuilder = new Mock<ICustomerFullNameBuilder>();

                //The FullNameBuilder is setup so that we expect it to be called and we don't care about the values going into it (any string)
                mockFullNameBuilder.Setup(
                    x => x.From(It.IsAny<string>(), It.IsAny<string>()));

                var customerService = new CustomerService(
                    mockCustomerRepository.Object, mockFullNameBuilder.Object);

                /**Act**/

                customerService.Create(customerToCreateDto);

                /**Assert**/

                //An assertion that the values from the customerToCreateDto are the exact values being passed into the CustomerFullNameBuilder's From method.
                //So on our mockFullNameBuilder we want to verify that on the .From method, It is a <string> and that string is going to be checking the equals
                //to make sure that the string value being passed in is equal to the customerToCreateDto.FirstName (and the same for customerToCreateDto.LastName)
                mockFullNameBuilder.Verify(x=>x.From(
                    It.Is<string>(
                        fn=>fn.Equals(customerToCreateDto.FirstName,
                            StringComparison.InvariantCultureIgnoreCase)),
                    It.Is<string>(
                        fn=>fn.Equals(customerToCreateDto.LastName,
                            StringComparison.InvariantCultureIgnoreCase))));
            }
        }
    }
}