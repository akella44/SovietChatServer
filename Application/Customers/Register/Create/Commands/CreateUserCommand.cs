using Domain.Entities;
using MediatR;

namespace Application.Customers.Register.Create.Commands;

public class CreateUserCommand : IRequest<User>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Tag { get; set; }
    public string Password { get; set; }
}