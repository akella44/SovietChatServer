using Application.Interfaces;
using MediatR;

namespace Application.Messaging.Session.Commands.CreateSession;

public class CreateSessionCommandHandler : IRequestHandler<CreateSessionCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly ISessionRepository _sessionRepository; 
    public CreateSessionCommandHandler(IUserRepository userRepository, ISessionRepository sessionRepository)
    {
        _userRepository = userRepository;
        _sessionRepository = sessionRepository;
    }
    
    public async Task Handle(CreateSessionCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Session session = new Domain.Entities.Session()
        {
            User = await _userRepository.GetUserByTag(request.Tag),
            Connection = request.ConnectionId
        };

        await _sessionRepository.UpdateOrAddConnection(session);
    }
}