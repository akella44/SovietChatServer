using Application.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Messaging.CreateChat;

public class CreateChatCommandHandler : IRequestHandler<CreateChatCommand>
{
    private readonly IChatRepository _chatRepository;
    private readonly IUserRepository _userRepository;

    public CreateChatCommandHandler(IChatRepository chatRepository, IUserRepository userRepository)
    {
        _chatRepository = chatRepository;
        _userRepository = userRepository;
    }

    public async Task Handle(CreateChatCommand request, CancellationToken cancellationToken)
    {
        var chat = new Chat();
        foreach (var tag in request.UserTags)
        {
            chat.Users.Add(await _userRepository.GetUserByTag(tag));
        }
        chat.ChatName = string.IsNullOrEmpty(request.ChatName) ? string.Join(", ", chat.Users.Take(3).Select(user => user.Name)) : request.ChatName;  
        await _chatRepository.AddChat(chat);
    }
}