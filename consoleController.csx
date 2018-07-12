#load "customerService.csx"

public class ConsoleController {
    private readonly CustomerService _customerService = new CustomerService();

    public void AddCustomer() {
        Console.WriteLine("Name:");
        var name = Console.ReadLine();
        Console.WriteLine("Phone number:");
        var phone = Console.ReadLine();

        try {
        _customerService.AddCustomer(name, phone);
        } catch (Exception ex) {
            Console.WriteLine(ex.Message);
        }
    }

    public void SearchForRepresentative() {
        Console.WriteLine("Current customers:");
        var customers = _customerService.GetAllCustomers();
        Console.WriteLine("Enter Customer-Id:");
        var id = Console.ReadLine();
    }
}