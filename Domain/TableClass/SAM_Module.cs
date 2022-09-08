using System.Collections.Generic;

namespace Domain.TableClass
{
    public partial class SAM_Module : BaseEntity
    {
        public int ModuleID { get; set; }
        public string ModuleCode { get; set; }
        public string ModuleDesc { get; set; }
        public string Icon { get; set; }
        public int OrderNo { get; set; }
        public List<SAM_Function> Func { get; set; } = new List<SAM_Function>();
    }
}
