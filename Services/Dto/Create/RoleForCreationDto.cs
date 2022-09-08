namespace Services.Dto.Create
{
    public class RoleForCreationDto : BaseCreationDto
    {
		public string RoleCode { get; set; }
		public string RoleName { get; set; }
		public string RoleNotes { get; set; }
	}
}
