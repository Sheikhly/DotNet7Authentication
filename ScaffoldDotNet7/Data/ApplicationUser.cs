using Microsoft.AspNetCore.Identity;

namespace ScaffoldDotNet7.Data
{
    //2nd Step: Inherite This Class With "IdentiyUser"
    //Our Goal Is Adding These Properties to Our User Table.
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? ProfilePicture { get; set; }
    }
}
