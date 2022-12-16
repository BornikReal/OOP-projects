﻿using Application.Abstractions.DataAccess;
using Domain.Accounts;
using MediatR;
using static Application.Contracts.Sessions.LogOut;

namespace Application.Sessions;

public class LogOutHandler : IRequestHandler<Command, Response>
{
    private readonly IDatabaseContext _context;

    public LogOutHandler(IDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
    {
        Session? session = _context.ActiveSessions.SingleOrDefault(session => session.SessionId == request.sessionId);
        
        if (session == null)
            throw new Exception();

        _context.ActiveSessions.Remove(session);
        await _context.SaveChangesAsync(cancellationToken);

        return new Response();
    }
}