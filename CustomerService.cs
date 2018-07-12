using System;
using System.Collections.Generic;
using System.Linq;
using Optional;
using Optional.Unsafe;
using value_object_sample.Entities;
using value_object_sample.Entities.ValueObjects;

namespace value_object_sample
{
    public class CustomerService
    {
        private static readonly IList<Customer> Customers = new List<Customer>();

        private static readonly IDictionary<PhoneNumberPrefix, PhoneNumber>
            CustomerRepresenativesMap = new Dictionary<PhoneNumberPrefix, PhoneNumber>();


        static CustomerService()
        {
            SeedCustomers();
            SeedRepresentatives();
        }

        public void AddCustomer(Name name, PhoneNumber phone)
        {
            var newId = Customers.Select(e => e.Id).Max() + 1;
            Customers.Add(Customer.Create(newId, name, phone));
        }

        public IReadOnlyList<Customer> GetAllCustomers()
        {
            return Customers.ToList();
        }

        public Option<PhoneNumber> GetSalesRepresentativeNumber(int customerId)
        {
            var customer = Customers
                .Where(e => e.Id == customerId)
                .Select(Option.Some)
                .DefaultIfEmpty(Option.None<Customer>())
                .First();
            if (!customer.HasValue)
            {
                return Option.None<PhoneNumber>();
            }

            var phone = customer
                .ValueOrFailure()
                .Phone;

            var prefix = phone.Prefix;

            return CustomerRepresenativesMap.TryGetValue(prefix, out var representativeNumber)
                ? Option.Some(representativeNumber)
                : Option.None<PhoneNumber>();
        }

        private static void SeedCustomers()
        {
            Customers.Add(Customer.Create(0, Name.Create("Hans"), PhoneNumber.Create("0794451234")));
            Customers.Add(Customer.Create(1, Name.Create("Peter"), PhoneNumber.Create("0411451634")));
            Customers.Add(Customer.Create(2, Name.Create("Thomas"), PhoneNumber.Create("0764554214")));
        }

        private static void SeedRepresentatives()
        {
            CustomerRepresenativesMap.Add(PhoneNumberPrefix.Create("079"), PhoneNumber.Create("0123456789"));
            CustomerRepresenativesMap.Add(PhoneNumberPrefix.Create("041"), PhoneNumber.Create("0125052749"));
            CustomerRepresenativesMap.Add(PhoneNumberPrefix.Create("076"), PhoneNumber.Create("4153426769"));
        }
    }
}