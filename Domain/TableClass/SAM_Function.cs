using System.Collections.Generic;

namespace Domain.TableClass
{
    public partial class SAM_Function : BaseEntity
    {
        public int FuncID { get; set; }
        public string FuncCode { get; set; }
        public string FuncDesc { get; set; }
        public string URL { get; set; }
        public string Icon { get; set; }
        public int OrderNo { get; set; }
        public int ModuleID { get; set; }
        public List<SAM_FuncInRole> Fuir { get; set; } = new List<SAM_FuncInRole>();
    }
}
