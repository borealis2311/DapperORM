namespace Services.Dto.Create
{
    public class FunctionForCreationDto : BaseCreationDto
    {
		public string FuncCode { get; set; }
		public string FuncDesc { get; set; }
		public string URL { get; set; }
		public string Icon { get; set; }
		public int OrderNo { get; set; }
		public int ModuleID { get; set; }
	}
}
