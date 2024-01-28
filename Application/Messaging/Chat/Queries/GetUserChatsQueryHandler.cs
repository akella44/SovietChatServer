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
            var messageCollection = lastMessageCollection as Domain.Entities.Message[] ?? lastMessageCollection.ToArray();
            var chatDto = new ChatDto()
            {
                ChatId = chat.ChatId,
                ChatName = chat.ChatName,
                ChatType = chat.ChatType.ToString(),
                LastMessageValue = messageCollection.Count() != 0 ? messageCollection.First().Value : null,
                LastMessageType = messageCollection.Count() != 0 ? messageCollection.First().MessageType.ToString() : null,
                NameOfLastMessageSender =  messageCollection.Count() != 0 ? messageCollection.First().MessageOwner.Name : null,
                TimeOfLastMessage = messageCollection.Count() != 0 ? messageCollection.First().SendTime : null
            };
            listOfChatDtos.Add(chatDto);
        }

        return listOfChatDtos;
    }
}