using Application.Interfaces;
using Domain.Enums;
using MediatR;

namespace Application.Messaging.Chat.Commands.CreateGroupChat;

public class CreateGroupChatCommandHandler : IRequestHandler<CreateGroupChatCommand>
{
    private readonly IChatRepository _chatRepository;
    private readonly IUserRepository _userRepository;

    public CreateGroupChatCommandHandler(IChatRepository chatRepository, IUserRepository userRepository)
    {
        _chatRepository = chatRepository;
        _userRepository = userRepository;
    }

    public async Task Handle(CreateGroupChatCommand request, CancellationToken cancellationToken)
    {
        var chat = new Domain.Entities.Chat
        {
            ChatType = ChatTypes.Group
        };
        foreach (var tag in request.UserTags)
        {
            chat.Users.Add(await _userRepository.GetUserByTag(tag));
        }
        chat.ChatName = string.IsNullOrEmpty(request.ChatName) ? string.Join(", ", chat.Users.Take(3).Select(user => user.Name)) : request.ChatName;  
        await _chatRepository.AddChat(chat);
    }
}