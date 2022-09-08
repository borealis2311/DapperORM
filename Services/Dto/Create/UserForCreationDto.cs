namespace Services.Dto.Create
{
	public class UserForCreationDto : BaseCreationDto
	{
		public string AccountName { get; set; }
		public string AccPwd { get; set; }
		public bool IsActivated { get; set; }
		public string AccountEmail { get; set; }
		public string RecoveryEmail { get; set; }
		public string PhoneNumber { get; set; }
		public int CustomerID { get; set; }
	}
}
