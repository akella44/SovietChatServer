using Application.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.RepositoriesImpl;

public class ChatRepository : IChatRepository
{
    private readonly AppDbContext _appDbContext;
    public ChatRepository(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }
    public async Task AddChat(Chat chat)
    {
        await _appDbContext.Chats.AddAsync(chat);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<Chat> GetByChatId(string chatId)
    {
        return await _appDbContext.Chats
            .FirstAsync(c => c.ChatId == chatId);
    }
}