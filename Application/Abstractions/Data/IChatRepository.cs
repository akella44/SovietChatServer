using Domain.Entities;

namespace Application.Interfaces;

public interface IChatRepository
{
    Task AddChat(Chat chat);
}