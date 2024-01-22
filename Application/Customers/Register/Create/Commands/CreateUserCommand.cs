using Domain.Entities;
using MediatR;

namespace Application.Customers.Register.Create.Commands;

public class CreateUserCommand : IRequest<User>
{
    public string Name;
    public string Email;
    public string Tag;
    public string Password;
}