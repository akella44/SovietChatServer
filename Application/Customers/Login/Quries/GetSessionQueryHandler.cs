using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Customers.Login.Quries;

public class GetSessionQueryHandler : IRequestHandler<GetSessionQuery, InitialSession>
{
    public IInitialSessionRepository InitialSessionRepository;

    public GetSessionQueryHandler(IInitialSessionRepository initialSessionRepository)
    {
        InitialSessionRepository = initialSessionRepository;
    }
    
    public async Task<InitialSession> Handle(GetSessionQuery request, CancellationToken cancellationToken)
    {
        return await InitialSessionRepository.GetBySignature(request.Signature);
    }
}