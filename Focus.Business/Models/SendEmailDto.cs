namespace Focus.Business.Models
{
    public class SendEmailDto
    {
        public string EmailTo { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string TextBody { get; set; }
        public string YourEmail { get; set; }
        public string Password { get; set; }
        
        public string UserName { get; set; }
    }
}
