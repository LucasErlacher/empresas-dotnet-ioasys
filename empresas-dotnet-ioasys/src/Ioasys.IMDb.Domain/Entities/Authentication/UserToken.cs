using System;

namespace Ioasys.IMDb.Domain.Entities.Authentication
{
    public class UserToken
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
