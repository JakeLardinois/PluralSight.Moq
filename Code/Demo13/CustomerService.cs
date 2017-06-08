namespace PluralSight.Moq.Code.Demo13
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMailingRepository _mailingRepository;

        public CustomerService(
            ICustomerRepository customerRepository, 
            IMailingRepository mailingRepository)
        {
            _customerRepository = customerRepository;
            _mailingRepository = mailingRepository;

            //Wire up the event so that then the NotifySalesTeam event fires, the NotifySalesTeam method is fired.
            _customerRepository.NotifySalesTeam += NotifySalesTeam;
        }

        private void NotifySalesTeam(object sender, NotifySalesTeamEventArgs e)
        {
            _mailingRepository.NewCustomerMessage(e.Name);  //We are testing if this method gets executed.
        }

        public void Create(CustomerToCreateDto customerToCreate)
        {
            var customer = new Customer(customerToCreate.Name);

            _customerRepository.Save(customer);
        }
    }
}