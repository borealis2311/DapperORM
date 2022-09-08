namespace Services.Dto.Update
{
	public class UserForUpdateDto : BaseUpdateDto
	{
		public string AccPwd { get; set; }
		public string AccountEmail { get; set; }
		public string RecoveryEmail { get; set; }
		public string PhoneNumber { get; set; }
	}
}
