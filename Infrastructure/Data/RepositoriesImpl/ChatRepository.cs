using Application.Interfaces;
using Domain.Entities;

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
}