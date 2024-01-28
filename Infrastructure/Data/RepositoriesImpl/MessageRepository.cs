using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.RepositoriesImpl;

public class MessageRepository : IMessageRepository
{
    private readonly AppDbContext _appDbContext;

    public MessageRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;   
    }

    public async Task<IEnumerable<Message>> GetChunkMessages(string chatId, int number, int size = 40)
    {
        return await _appDbContext.Messages
            .Where(m => m.MessageChat.ChatId == chatId)
            .OrderByDescending(m => m.SendTime)
            .Skip((number-1) * size)
            .Take(size)
            .ToListAsync();
    }

    public async Task AddMessage(Message message)
    {
        await _appDbContext.Messages.AddAsync(message);
        await _appDbContext.SaveChangesAsync();
    }
}