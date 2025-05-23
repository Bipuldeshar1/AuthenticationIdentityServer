﻿namespace AuthenticationIdentityServer.Models.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ICollection<UserRoles> userRoles { get; set; }
    }
}
