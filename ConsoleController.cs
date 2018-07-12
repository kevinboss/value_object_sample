using System;
using System.Linq;
using Optional.Unsafe;
using value_object_sample.Entities.ValueObjects;

namespace value_object_sample
{
    public class ConsoleController
    {
        private readonly CustomerService _customerService = new CustomerService();

        public void AddCustomerAction()
        {
            Console.WriteLine("Name:");
            var name = Console.ReadLine();
            Console.WriteLine("Phone number:");
            var phone = Console.ReadLine();

            var nameValidationResult = Name.IsValid(name);
            if (!nameValidationResult.IsValid)
            {
                foreach (var error in nameValidationResult.Errors)
                {
                    Console.WriteLine(error);
                }
            }

            var phoneNumberValidationResult = PhoneNumber.IsValid(phone);
            if (!phoneNumberValidationResult.IsValid)
            {
                foreach (var error in phoneNumberValidationResult.Errors)
                {
                    Console.WriteLine(error);
                }
            }

            _customerService.AddCustomer(Name.Create(name), PhoneNumber.Create(phone));
            Console.WriteLine("Customer successfully added");
        }

        public void SearchForRepresentativeAction()
        {
            Console.WriteLine("Current customers:");
            if (TryPrintCustomers())
            {
                return;
            }

            var keepSearching = true;
            while (keepSearching)
            {
                keepSearching = SearchForRepresentative();
            }
        }

        private bool TryPrintCustomers()
        {
            var customers = _customerService.GetAllCustomers();

            if (!customers.Any())
            {
                Console.WriteLine("None");
                return true;
            }

            foreach (var customer in customers)
            {
                Console.WriteLine($"Id: {customer.Id}, Name: {customer.Name}, Phone: {customer.Phone}");
            }

            return false;
        }

        private bool SearchForRepresentative()
        {
            Console.WriteLine("Enter Customer-Id:");
            var id = Console.ReadLine();

            if (!int.TryParse(id, out var parsedId))
            {
                Console.WriteLine("Please enter a numeric Customer-Id");
                return true;
            }

            var result = _customerService.GetSalesRepresentativeNumber(parsedId);
            if (!result.HasValue)
            {
                Console.WriteLine("No representative exists for given Customer-Id");
                return false;
            }

            Console.WriteLine(
                $"Customer representative number for Customer-Id [{id}] is: {result.ValueOrFailure()}");
            return false;
        }
    }
}