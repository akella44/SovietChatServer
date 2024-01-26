using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Customers.Login.Quries;

public class GetSessionQueryHandler : IRequestHandler<GetSessionQuery, InitialSession>
{
    public ISessionRepository _sessionRepository;

    public GetSessionQueryHandler(ISessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository;
    }
    
    public async Task<InitialSession> Handle(GetSessionQuery request, CancellationToken cancellationToken)
    {
        return await _sessionRepository.GetBySignature(request.Signature);
    }
}