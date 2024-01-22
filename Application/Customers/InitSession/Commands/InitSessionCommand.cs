using Domain.Entities;
using MediatR;

namespace Application.Customers.InitSession;

public record InitSessionCommand(string ClientPublicKey) : IRequest<Session>;