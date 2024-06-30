using System;

namespace Focus.Business.CompanyActionProcess.Commands
{
    public class ProcessLookUpModel
    {
        public Guid Id { get; set; }
        public string ProcessNameArabic { get; set; }
        public string ProcessName { get; set; }
        public string Type { get; set; }
        public bool Status { get; set; }
    }
}
