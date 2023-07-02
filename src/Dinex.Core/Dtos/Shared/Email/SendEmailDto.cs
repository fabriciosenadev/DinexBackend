namespace Dinex.Core
{
    public class SendEmailDto
    {
        public string GeneratedCode { get; set; }
        public string FullName { get; set; }
        public string EmailTo { get; set; }
        public string EmailTemplateFileName { get; set; }
        public string EmailSubject { get; set; }
        public string Origin { get; set; }
        public string TemplateFieldToName { get; set; }
        public string TemplateFieldToUrl { get; set; }
    }
}
