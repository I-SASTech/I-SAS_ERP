using Focus.Domain.Enum;
using System;

namespace Focus.Business.ImportExportTypes.Commands
{
    public class ImportExportTypeLookUpModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public Domain.Enum.ImportExportTypes ImportExportTypes { get; set; }
        public bool isActive { get; set; }
    }
}
