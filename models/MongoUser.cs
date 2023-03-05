using Microsoft.AspNetCore.Identity;

namespace RedMango_API.models
{
    public class MongoUser: IdentityUser
    {
        public string Name { get; set; }
    }
}
