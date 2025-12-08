using Microsoft.AspNetCore.Identity;

namespace ShopTARge24.Core.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string City { get; set; }
    }
}
