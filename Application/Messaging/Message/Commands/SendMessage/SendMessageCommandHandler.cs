using Application.Interfaces;
using Domain.Enums;
using MediatR;

namespace Application.Messaging.Message.Commands.SendMessage;

public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IChatRepository _chatRepository;
    private readonly IUserRepository _userRepository;

    public SendMessageCommandHandler(IMessageRepository messageRepository, IChatRepository chatRepository, IUserRepository userRepository)
    {
        _messageRepository = messageRepository;
        _chatRepository = chatRepository;
        _userRepository = userRepository;
    }

    public async Task Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        var chat = await _chatRepository.GetByChatId(request.ChatId);
        var user = await _userRepository.GetUserByTag(request.UserTag);
        Enum.TryParse<MessageTypes>(request.MessageType, out var enumObj);
        var message = new Domain.Entities.Message()
        {
            MessageChat = chat,
            MessageOwner = user,
            MessageType = enumObj,
            Value = request.Value
        };
        await _messageRepository.AddMessage(message);
    }
}