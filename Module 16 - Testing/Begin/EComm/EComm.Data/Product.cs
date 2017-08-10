using System.ComponentModel.DataAnnotations.Schema;

namespace EComm.Data
{
    [Table("Product")]
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal? UnitPrice { get; set; }
        public string Package { get; set; }
        public bool IsDiscontinued { get; set; }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public string FormattedUnitPrice
        {
            get { return string.Format("{0:c}", UnitPrice); }
        }
    }
}
