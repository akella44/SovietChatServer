namespace Application.Customers.Register.Create.Request;

public class CreateUserRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Tag { get; set; }
    public string Password { get; set; }
}