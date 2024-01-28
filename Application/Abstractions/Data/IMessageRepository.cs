using Domain.Entities;

namespace Application.Interfaces;

public interface IMessageRepository
{ 
    Task<IEnumerable<Message>> GetChunkMessages(string chatId, int number, int size = 40);
    Task AddMessage(Message message);
}