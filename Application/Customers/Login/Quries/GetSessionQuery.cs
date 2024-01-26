using Domain.Entities;
using MediatR;

namespace Application.Customers.Login.Quries;

public record GetSessionQuery(string Signature): IRequest<InitialSession>;