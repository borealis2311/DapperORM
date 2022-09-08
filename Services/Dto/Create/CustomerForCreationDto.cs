namespace Services.Dto.Create
{
    public class CustomerForCreationDto : BaseCreationDto
	{
		public string CustomerCode { get; set; }
		public string FullName { get; set; }
		public string TaxCode { get; set; }
		public string Address { get; set; }
	}
}
