using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Messaging.Session.Queries;

public class GetUserChatIdsQueryHandler : IRequestHandler<GetUserChatIdsQuery, IEnumerable<string>>
{
    private readonly IUserRepository _userRepository;

    public GetUserChatIdsQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<string>> Handle(GetUserChatIdsQuery request, CancellationToken cancellationToken)
    {
        User user = await _userRepository.GetUserWithLinkedDataByTag(request.UserTag);
        return user.Chats.Select(c => c.ChatId).ToList();
    }
}