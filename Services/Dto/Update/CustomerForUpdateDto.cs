namespace Services.Dto.Update
{
    public class CustomerForUpdateDto : BaseUpdateDto
	{
		public string FullName { get; set; }
		public string TaxCode { get; set; }
		public string Address { get; set; }
	}
}
