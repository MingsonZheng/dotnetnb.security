namespace DotNetNB.Security.Core.Models
{
    public class Permission
    {
        public string Key { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public IEnumerable<Resource> Resources { get; set; }
    }
}