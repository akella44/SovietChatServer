using Domain.Entities;

namespace Application.Interfaces;

public interface IChatRepository
{
    Task AddChat(Chat chat);
    Task<Chat> GetByChatId(string chatId);
}