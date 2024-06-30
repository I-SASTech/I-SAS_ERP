using System;

namespace Focus.Business.SmsSetup.GsmSmsSetupCommand
{
    public class GsmSmsSetupModel
    {
        public Guid Id { get; set; }
        public string ComPort { get; set; }
        public string DefaultMessage { get; set; }
        public string[] Ports { get; set; }
        public Guid? BranchId { get; set; }
    }
}
