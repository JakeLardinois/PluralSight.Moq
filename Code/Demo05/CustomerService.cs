﻿using System.Collections.Generic;

namespace PluralSight.Moq.Code.Demo05
{
    public class CustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IIdFactory _idFactory;

        public CustomerService(
            ICustomerRepository customerRepository, 
            IIdFactory idFactory)
        {
            _customerRepository = customerRepository;
            _idFactory = idFactory;
        }

        public void Create(IEnumerable<CustomerToCreateDto> customersToCreate)
        {
            foreach (var customerToCreateDto in customersToCreate)
            {
                var customer = new Customer(
                    customerToCreateDto.FirstName, 
                    customerToCreateDto.LastName);

                customer.Id = _idFactory.Create(); //unique Id is assigned
                
                _customerRepository.Save(customer);
            }
        }
    }
}