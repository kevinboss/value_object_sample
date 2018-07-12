using System;
using System.Collections.Generic;
using System.Linq;
using Optional;
using Optional.Unsafe;
using value_object_sample.Entities;

namespace value_object_sample
{
    public class CustomerService
    {
        private static readonly IList<Customer> Customers = new List<Customer>();

        private static readonly IDictionary<string, string>
            CustomerRepresenativesMap = new Dictionary<string, string>();


        static CustomerService()
        {
            SeedCustomers();
            SeedRepresentatives();
        }

        public void AddCustomer(string name, string phone)
        {
            if (!IsNumeric(phone))
            {
                throw new ArgumentException("Must be numeric", nameof(phone));
            }

            if (phone.Length != 10)
            {
                throw new ArgumentException("Must be exactly 10 digits long", nameof(phone));
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Can not be empty", nameof(name));
            }

            var newId = Customers.Select(e => e.Id).Max() + 1;
            Customers.Add(Customer.Create(newId, name, phone));
        }

        public IReadOnlyList<Customer> GetAllCustomers()
        {
            return Customers.ToList();
        }

        public Option<string> GetSalesRepresentativeNumber(int customerId)
        {
            var customer = Customers
                .Where(e => e.Id == customerId)
                .Select(Option.Some)
                .DefaultIfEmpty(Option.None<Customer>())
                .First();
            if (!customer.HasValue)
            {
                return Option.None<string>();
            }

            var phone = customer
                .ValueOrFailure()
                .Phone;

            if (phone.Length < 3)
            {
                return Option.None<string>();
            }

            var prefix = phone.Substring(0, 3);

            return CustomerRepresenativesMap.TryGetValue(prefix, out var representativeNumber)
                ? Option.Some(representativeNumber)
                : Option.None<string>();
        }

        private bool IsNumeric(string value)
        {
            return int.TryParse(value, out _);
        }

        private static void SeedCustomers()
        {
            Customers.Add(Customer.Create(0, "Hans", "0794451234"));
            Customers.Add(Customer.Create(1, "Peter", "0411451634"));
            Customers.Add(Customer.Create(2, "Thomas", "0764554214"));
        }

        private static void SeedRepresentatives()
        {
            CustomerRepresenativesMap.Add("079", "0123456789");
            CustomerRepresenativesMap.Add("041", "0125052749");
            CustomerRepresenativesMap.Add("076", "4153426769");
        }
    }
}