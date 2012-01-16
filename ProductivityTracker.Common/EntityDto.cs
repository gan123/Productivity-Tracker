namespace ProductivityTracker.Common
{
    public class EntityDto
    {
        public string Id { get; set; }
    }

    public class UserDto : EntityDto
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}