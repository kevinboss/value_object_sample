namespace value_object_sample.Entities
{
    public class Customer
    {
        private Customer()
        {
        }

        public static Customer Create(int id, string name, string phone)
        {
            return new Customer
            {
                Id = id,
                Name = name,
                Phone = phone
            };
        }

        public int Id { get; private set; }

        public string Phone { get; private set; }

        public string Name { get; private set; }
    }
}