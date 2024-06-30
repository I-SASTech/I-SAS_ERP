using System;

namespace Focus.Business.Models
{
    public class SyncRecordModel
    {
        public int Id { get; set; }
        public Guid CompanyId { get; set; }
        public string Table { get; set; }
        public string ColumnId { get; set; }
        public string ColumnType { get; set; }
        public bool Push { get; set; } = false;
        public bool Pull { get; set; } = false;
        public string Action { get; set; }
        public DateTime ChangeDate { get; set; }
        public string PushDate { get; set; }
        public string PullDate { get; set; }
        public string ColumnName { get; set; }
        public string Code { get; set; }
        public Guid? BranchId { get; set; }
    }
}
