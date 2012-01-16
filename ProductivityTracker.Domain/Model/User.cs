namespace ProductivityTracker.Domain.Model
{
    public class User : EntityBase
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}