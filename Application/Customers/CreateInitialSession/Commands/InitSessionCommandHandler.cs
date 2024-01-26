﻿using System.Text;
using Application.Interfaces;
using Domain.Cryptography;
using Domain.Entities;
using MediatR;

namespace Application.Customers.InitSession;

public class InitSessionCommandHandler : IRequestHandler<InitSessionCommand, InitialSession>
{
    public ISessionRepository _sessionRepository;
    
    public InitSessionCommandHandler(ISessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository;
    }

    public async Task<InitialSession> Handle(InitSessionCommand request, CancellationToken cancellationToken)
    {
        var session = new InitialSession()
        {
            PublicKey = request.ClientPublicKey
        };

        await _sessionRepository.AddSession(session);

        return session;
        /*return Convert.ToBase64String(RsaEncoder.EncryptData(Convert.FromBase64String(session.SessionKey), session.PublicKey));*/
    }
}