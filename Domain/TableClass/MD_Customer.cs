using System.Collections.Generic;

namespace Domain.TableClass
{
    public partial class MD_Customer : BaseEntity
    {
        public int CustomerID { get; set; }
        public string CustomerCode { get; set; }
        public string FullName { get; set; }
        public string TaxCode { get; set; }
        public string Address { get; set; }
        public List<SAM_UserAccount> Users { get; set; } = new List<SAM_UserAccount>();
    }
}
