using Domain.Entities;

namespace Application.Abstractions.Login;

public interface IJwtProvider
{
    public string GenerateJwtToken(User user);
}