using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Noble.Api.Models
{
    public class ProductFilterVm
    {
        public List<Guid?> originId { get; set; }
        public List<Guid?> sizeId { get; set; }
        public List<Guid?> colorId { get; set; }
        public List<Guid?> subCategoryId { get; set; }
        public List<Guid?> productMasterId { get; set; }
        public Guid? warehouseId { get; set; }
        public List<Guid?> categoryId { get; set; }
        public Guid? branchId { get; set; }
        public string BarCodeType { get; set; }
        public bool? isService { get; set; }
        public bool isProductList { get; set; }
        public bool isRaw { get; set; }
        public bool isDropdown { get; set; }
        public bool colorVariants { get; set; }
        public int? pageNumber { get; set; }
        public int openBatch { get; set; }
        public int pageSize { get; set; }
        public bool isFifo { get; set; }
        public string status { get; set; }
        public string searchTerm { get; set; }
    }
}
