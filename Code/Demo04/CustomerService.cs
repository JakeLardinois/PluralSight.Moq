namespace PluralSight.Moq.Code.Demo04
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMailingAddressFactory _mailingAddressFactory;

        public CustomerService(ICustomerRepository customerRepository, 
            IMailingAddressFactory mailingAddressFactory)
        {
            _customerRepository = customerRepository;
            _mailingAddressFactory = mailingAddressFactory;
        }

        public void Create(CustomerToCreateDto customerToCreate)
        {
            var customer = new Customer(customerToCreate.Name);

             MailingAddress mailingAddress;
             var mailingAddressSuccessfullyCreated = 
                 _mailingAddressFactory.TryParse( //A TryParse method that takes in a string and returns a MailingAddress object
                    customerToCreate.Address, 
                    out mailingAddress);

             if (mailingAddress == null) //Note that we could use the mailingAddressSuccessfullyCreated value as well...
             {
                 throw new InvalidMailingAddressException();
             }
             customer.MailingAddress = mailingAddress;
             _customerRepository.Save(customer);
         }
    }
}