using Moq;
using NUnit.Framework;
using PluralSight.Moq.Code.Demo07;

namespace PluralSight.Moq.Tests.Demo07
{
    public class CustomerServiceTests
    {
        [TestFixture]
        public class When_creating_a_platinum_status_customer
        {
            [Test]
            public void a_special_save_routine_should_be_used()
            {
                /**Arrange**/

                //What we are looking to test here is that if a customer's status is set to Platinum, then the SaveSpecial method is called inside of the CustomerService Class
                var mockCustomerRepository = new Mock<ICustomerRepository>();
                var mockCustomerStatusFactory = new Mock<ICustomerStatusFactory>();

                var customerToCreate = new CustomerToCreateDto
                                           {
                                               DesiredStatus = CustomerStatus.Platinum, 
                                               FirstName = "Bob", 
                                               LastName = "Builder"
                                           };

                //Since the propery checked in the CustomerService Class is the Customer objects StatusLevel property, which is set
                //by passing in the CustomerToCreateDto to the CustomerStatusFactory, we need to Mock it out here and specify...
                mockCustomerStatusFactory.Setup( 
                    x => x.CreateFrom(
                        It.Is<CustomerToCreateDto>( //If a CustomerToCreateDto is passed into the CustomerStatusFactory
                            y => y.DesiredStatus == CustomerStatus.Platinum))) //and it's DesiredStatus property is set to CustomerStatus.Platinum
                    .Returns(CustomerStatus.Platinum); //Then the CustomerStatusFactory will return CustomerStatus.Platinum


                var customerService = new CustomerService(
                    mockCustomerRepository.Object, mockCustomerStatusFactory.Object);

                /**Act**/

                customerService.Create(customerToCreate);

                /**Assert**/

                mockCustomerRepository.Verify(
                    x=>x.SaveSpecial(It.IsAny<Customer>())); //verify the fact that the SaveSpecial method is called because the DesiredStatus value
                                                             //in the CustomerService class is set to CustomerStatus.Platinum
            }


            [Test]
            public void a_regular_save_routine_should_be_used()
            {
                /**Arrange**/

                //What we are looking to test here is that if a customer's status is set to Bronze, then the Save method is called inside of the CustomerService Class
                var mockCustomerRepository = new Mock<ICustomerRepository>();
                var mockCustomerStatusFactory = new Mock<ICustomerStatusFactory>();

                var customerToCreate = new CustomerToCreateDto
                {
                    DesiredStatus = CustomerStatus.Bronze,
                    FirstName = "Bob",
                    LastName = "Builder"
                };

                //Since the propery checked in the CustomerService Class is the Customer objects StatusLevel property, which is set
                //by passing in the CustomerToCreateDto to the CustomerStatusFactory, we need to Mock it out here and specify...
                mockCustomerStatusFactory.Setup(
                    x => x.CreateFrom(
                        It.Is<CustomerToCreateDto>( //If a CustomerToCreateDto is passed into the CustomerStatusFactory
                            y => y.DesiredStatus == CustomerStatus.Bronze))) //and it's DesiredStatus property is set to CustomerStatus.Bronze
                    .Returns(CustomerStatus.Bronze); //Then the CustomerStatusFactory will return CustomerStatus.Bronze


                var customerService = new CustomerService(
                    mockCustomerRepository.Object, mockCustomerStatusFactory.Object);

                /**Act**/

                customerService.Create(customerToCreate);

                /**Assert**/

                mockCustomerRepository.Verify(
                    x => x.Save(It.IsAny<Customer>())); //verify the fact that the Save method is called because the DesiredStatus value
                                                        //in the CustomerService class is set to CustomerStatus.Bronze
            }

            [Test]
            public void a_save_routine_should_be_used()
            {
                /**Arrange**/

                //What we are looking to test here is that if a customer's status is set to Bronze, then the Save method is called inside of the CustomerService Class
                var mockCustomerRepository = new Mock<ICustomerRepository>();
                var mockCustomerStatusFactory = new Mock<ICustomerStatusFactory>();

                var customerToCreate = new CustomerToCreateDto
                {
                    DesiredStatus = CustomerStatus.Bronze,
                    FirstName = "Bob",
                    LastName = "Builder"
                };

                //So even though we don't specify that a Value of CustomerStatus.Bronze should be returned by the factory if the CustomerToCreateDto.DesiredStatus's property is set to Bronze
                //The test will still pass because the default behaviour of MoQ is that if you don't have a setup for a scenario (ie CustomerStatus being set to Bronze), then the return value will be the default.
                //In the case of our enum, the default is going to be the first value, which in this case is Bronze (for complex types aka objects the default is Null).
                mockCustomerStatusFactory.Setup(
                    x => x.CreateFrom(
                        It.Is<CustomerToCreateDto>(
                            y => y.DesiredStatus == CustomerStatus.Platinum)))
                    .Returns(CustomerStatus.Platinum);


                var customerService = new CustomerService(
                    mockCustomerRepository.Object, mockCustomerStatusFactory.Object);

                /**Act**/

                customerService.Create(customerToCreate);

                /**Assert**/

                mockCustomerRepository.Verify(
                    x => x.Save(It.IsAny<Customer>())); //verify the fact that the SaveSpecial method is called because the DesiredStatus value
                                                               //in the CustomerService class is set to CustomerStatus.Platinum
            }
        }
    }
}