using Application.Customers.Register.Create.Commands;
using Application.Customers.Register.Create.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("api/v1")]
public sealed class RegisterController(ISender sender) : ControllerBase
{
    [HttpPost("create")]
    public async Task<IResult> CreateUser(CreateUserCommand createUserCommand)
    {
        await sender.Send(createUserCommand);
        return Results.Ok();
    }
}