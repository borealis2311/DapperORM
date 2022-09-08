using System;

namespace Services.Dto.Create
{
    public class UirForCreationDto : BaseCreationDto
    {
        public int RoleID { get; set; }
        public int AccountID { get; set; }
        public DateTime ValidDateFrom { get; set; }
    }
}
