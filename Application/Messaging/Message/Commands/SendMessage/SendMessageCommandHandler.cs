using Application.Interfaces;
using Domain.Entities;
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
        var message = new Domain.Entities.Message
        {
            MessageChat = await _chatRepository.GetByChatId(request.ChatId),
            MessageOwner = await _userRepository.GetUserByTag(request.UserTag),
            MessageType = request.MessageType,
            Value = request.Value
        };
        await _messageRepository.AddMessage(message);
        
        
    }
}