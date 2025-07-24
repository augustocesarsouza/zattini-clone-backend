﻿namespace Zattini.Domain.Authentication
{
    public interface ICurrentUser
    {
        public string? Email { get; }
        public bool IsValid { get; }
    }
}
