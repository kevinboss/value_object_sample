#load "entities.csx"

public class CustomerService {
    private readonly IList<Customer> _customers = new List<Customer>();

    public void AddCustomer(string name, string phone){
        if ()
        if (!Justnumbers(phone) && phone.Length == 10) {

        }
    }
    public IReadOnlyList<Customer> GetAllCustomers(){
        return _customers.ToList();
    }

    private bool Justnumbers(string value) {
        
    }
}