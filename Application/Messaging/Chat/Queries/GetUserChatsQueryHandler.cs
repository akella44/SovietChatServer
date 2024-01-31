using Application.Interfaces;
using Application.Model;
using MediatR;

namespace Application.Messaging.Chat.Queries;

public class GetUserChatsQueryHandler : IRequestHandler<GetUserChatsQuery, IEnumerable<ChatDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMessageRepository _messageRepository;

    public GetUserChatsQueryHandler(IUserRepository userRepository, IMessageRepository messageRepository)
    {
        _userRepository = userRepository;
        _messageRepository = messageRepository;
    }

    public async Task<IEnumerable<ChatDto>> Handle(GetUserChatsQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserWithLinkedDataByTag(request.Tag);
        List<ChatDto> listOfChatDtos = new List<ChatDto>();
        
        foreach (var chat in user.Chats)
        {
            var lastMessageCollection = await _messageRepository.GetChunkMessages(chat.ChatId, 1, 1);
            Domain.Entities.Message? lastMessage = lastMessageCollection.Count() != 0 ? lastMessageCollection.First() : null;
            ChatDto chatDto = new ChatDto()
            {
                ChatId = chat.ChatId,
                ChatName = chat.ChatName,
                ChatType = chat.ChatType.ToString(),
                LastMessageValue = lastMessage != null ? lastMessage.Value : null,
                LastMessageType = lastMessage != null ? lastMessage.MessageType.ToString() : null,
                NameOfLastMessageSender = lastMessage != null ? lastMessage.MessageOwner.Name : null,
                TimeOfLastMessage = lastMessage != null ? lastMessage.SendTime : null
            };
            listOfChatDtos.Add(chatDto);
        }

        return listOfChatDtos;
    }
}