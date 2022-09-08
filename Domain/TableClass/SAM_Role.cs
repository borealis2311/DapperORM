using System.Collections.Generic;

namespace Domain.TableClass
{
    public partial class SAM_Role : BaseEntity
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string RoleNotes { get; set; }
        public string RoleCode { get; set; }
        public List<SAM_UserInRole> Usir { get; set; } = new List<SAM_UserInRole>();
        public List<SAM_FuncInRole> Fuir { get; set; } = new List<SAM_FuncInRole>();
    }
}
