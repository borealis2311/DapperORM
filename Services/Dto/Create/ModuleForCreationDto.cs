namespace Services.Dto.Create
{
    public class ModuleForCreationDto : BaseCreationDto
	{
		public string ModuleCode { get; set; }
		public string ModuleDesc { get; set; }
		public string Icon { get; set; }
		public int OrderNo { get; set; }
	}
}
