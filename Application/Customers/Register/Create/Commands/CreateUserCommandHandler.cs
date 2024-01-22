using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Customers.Register.Create.Commands;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        User user = new User()
        {
            Email = request.Email,
            Name = request.Name,
            Password = request.Password,
            Tag = request.Tag
        };

        await _userRepository.AddUser(user);

        return user;
    }
}