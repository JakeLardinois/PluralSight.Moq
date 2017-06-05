﻿using Moq;
using NUnit.Framework;
using PluralSight.Moq.Code.Demo11;

namespace PluralSight.Moq.Tests.Demo11
{
    public class CustomerServiceTests
    {
        [TestFixture]
        public class When_creating_a_customer
        {
            [Test]
            public void the_workstationid_should_be_retrieved()
            {
                //Arrange
                var mockCustomerRepository = new Mock<ICustomerRepository>();
                var mockApplicationSettings = new Mock<IApplicationSettings>();

                mockApplicationSettings.Setup(
                    x => x.SystemConfiguration.AuditingInformation.WorkstationId)
                    .Returns(123);
                
                var customerService = new CustomerService(mockCustomerRepository.Object, mockApplicationSettings.Object);

                //Act
                customerService.Create(new CustomerToCreateDto());

                //Assert
                mockApplicationSettings.VerifyGet(
                    x => x.SystemConfiguration.AuditingInformation.WorkstationId);
            }
        }
    }
}