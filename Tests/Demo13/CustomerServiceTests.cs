using Moq;
using NUnit.Framework;
using PluralSight.Moq.Code.Demo13;

namespace PluralSight.Moq.Tests.Demo13
{
    public class CustomerServiceTests
    {
        [TestFixture]
        public class When_creating_a_new_customer
        {
            [Test]
            public void an_email_should_be_sent_to_the_sales_team()
            {
                /**Arrange**/

                //Testing an Event.

                var mockCustomerRepository = new Mock<ICustomerRepository>();
                var mockMailingRepository = new Mock<IMailingRepository>();

                var customerService = new CustomerService(
                    mockCustomerRepository.Object, mockMailingRepository.Object);

                /**Act**/

                //mockCustomerRepository.Raise( //raises an event
                //    x=>x.NotifySalesTeam += null,  //we want to raise the NotifySalesTeam event (null could be some type of handler; object property?)
                //    new NotifySalesTeamEventArgs("jim")); //pass in the event arguments. Note the custom implementation of EventArgs, so that the constructor takes in a parameter
                //                                          //which sets a Name property on NotifySalesTeamEventArgs...

                //Using delegate
                mockCustomerRepository.Raise( //raises an event
                    x => x.NotifySalesTeam += null,
                    "jim", false); //Takes a parameter array of objects as arguments
                                                          

                /**Assert**/

                //We want to verify that the NewCustomerMessage method of the MailingRepository object is called and we don't care what value of 
                //parameter (string) being passed in is.
                mockMailingRepository.Verify(
                    x => x.NewCustomerMessage(It.IsAny<string>()));
            }
        }
    }
}