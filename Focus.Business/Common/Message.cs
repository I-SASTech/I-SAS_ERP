using System;


namespace Focus.Business.Common
{
    public class Message
    {
        public Guid Id { get; set; }
        public bool IsSuccess { get; set; }
        public string IsAddUpdate { get; set; }
        public string IsDeleted { get; set; }
        public Guid SaleId { get; set; }
        public bool IsDoublePrint { get; set; }

    }
}