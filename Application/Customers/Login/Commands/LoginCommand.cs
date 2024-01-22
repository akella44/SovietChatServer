using MediatR;

namespace Application.Customers.Login;

public record LoginCommand(string Email, string Password) : IRequest<string>;