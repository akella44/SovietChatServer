using System.Text.Json;
using Application.Cryptography.Providers;
using Application.Customers.Login;
using Application.Customers.Login.Quries;
using Domain.Cryptography;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;
[ApiController]
[Route("api/v1")]
public class LoginController : ControllerBase
{
    private readonly ISender _sender;

    public LoginController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("login")]
    [Consumes("text/plain")]
    public async Task<IResult> Login([FromBody] string body)
    {
        Console.WriteLine($"New login request with {body}");
        
        Request.Headers.TryGetValue("signature", out var signature );
        var sessionQuery = new GetSessionQuery(signature);
        
        var session = await _sender.Send(sessionQuery);
        var decryptedData = await _sender.Send(new DecryptCommand<AesProvider>(body, session.SessionKey));
        LoginCommand? loginCommand = JsonSerializer.Deserialize<LoginCommand>(decryptedData);

        string jwtToken = await _sender.Send(loginCommand);
        string encryptedJwtToken = await _sender.Send(new EncryptCommand<AesProvider>(jwtToken, session.SessionKey));
        
        Console.WriteLine("encrypted jwt token " + encryptedJwtToken);
        return Results.Content(encryptedJwtToken);
    }
}