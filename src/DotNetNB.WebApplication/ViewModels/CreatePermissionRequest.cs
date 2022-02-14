namespace DotNetNB.WebApplication.ViewModels
{
    public class CreatePermissionRequest
    {
        public string Key { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public IEnumerable<string> resources { get; set; }
    }
}