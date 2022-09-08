using System.Collections.Generic;

namespace Domain.TableClass
{
    public partial class SAM_UserAccount : BaseEntity
    {
        public int AccountID { get; set; }
        public string AccountName { get; set; }
        public string AccPwd { get; set; }
        public bool? IsActivated { get; set; }
        public string AccountEmail { get; set; }
        public string RecoveryEmail { get; set; }
        public string PhoneNumber { get; set; }
        public int CustomerID { get; set; }
        public List<SAM_UserInRole> Usir { get; set; } = new List<SAM_UserInRole>();
    }
}
