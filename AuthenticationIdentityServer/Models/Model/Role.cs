namespace AuthenticationIdentityServer.Models.Model
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public ICollection<UserRoles> userRoles { get; set; }
    }
}
