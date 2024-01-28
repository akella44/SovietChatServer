using System.Text.Json;
using Application.Cryptography.Providers;
using Application.Customers.Login;
using Application.Customers.Login.Quries;
using Domain.Cryptography;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;
[ApiController]
[Route("api/v1/login")]
public sealed class LoginController(ISender sender) : ControllerBase
{
    [HttpPost("")]
    [Consumes("text/plain")]
    public async Task<IResult> Login([FromBody] string body)
    {
        Request.Headers.TryGetValue("signature", out var signature );
        var sessionQuery = new GetSessionQuery(signature);
        
        var session = await sender.Send(sessionQuery);
        var decryptedData = await sender.Send(new DecryptCommand<AesProvider>(body, session.SessionKey));
        LoginCommand? loginCommand = JsonSerializer.Deserialize<LoginCommand>(decryptedData);
        
        if (loginCommand == null)
            return Results.BadRequest();
        
        string jwtToken = await sender.Send(loginCommand);
        string encryptedJwtToken = await sender.Send(new EncryptCommand<AesProvider>(jwtToken, session.SessionKey));
        
        return Results.Content(encryptedJwtToken);
    }
    
    
    [HttpPost("login-without-encrypt")]
    public async Task<IResult> LoginWithoutEncrypt(LoginCommand loginCommand)
    {
        string jwtToken = await sender.Send(loginCommand);
        return Results.Ok(jwtToken);
    }
}