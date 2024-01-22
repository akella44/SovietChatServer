using System.Security.Authentication;
using Application.Abstractions.Login;
using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Customers.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;
    
    public LoginCommandHandler(IUserRepository userRepository, 
        IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
    }

    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetUserByEmail(request.Email);

        if (user is null || user.Password != request.Password)
        {
            throw new InvalidCredentialException();
        }

        var token = _jwtProvider.GenerateJwtToken(user);
        
        return token;
        /*return AesService.EncryptData(Convert.ToBase64String(Encoding.UTF8.GetBytes(token)),
            session.SessionKey);*/
    }
}