using Application.Cryptography.Providers;
using Application.Customers.InitSession;
using Domain.Cryptography;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;
[ApiController]
[Route("api/v1")]
public sealed class InitialSessionController(ISender sender) : ControllerBase
{
    [HttpPost("init")]
    [Consumes("text/plain")]
    public async Task<IResult> Init([FromBody] string body)
    {
        var session = await sender.Send(new InitSessionCommand(body));
        Response.Headers["signature"] = session.Signature;
        string encryptedSessionKey =
            await sender.Send(new EncryptCommand<RsaProvider>(session.SessionKey, session.PublicKey));
        
        return Results.Content(encryptedSessionKey);
    }
}