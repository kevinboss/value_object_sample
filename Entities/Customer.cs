using value_object_sample.Entities.ValueObjects;

namespace value_object_sample.Entities
{
    public class Customer
    {
        private Customer()
        {
        }

        public static Customer Create(int id, Name name, PhoneNumber phone)
        {
            return new Customer
            {
                Id = id,
                Name = name,
                Phone = phone
            };
        }

        public int Id { get; private set; }

        public PhoneNumber Phone { get; private set; }

        public Name Name { get; private set; }
    }
}