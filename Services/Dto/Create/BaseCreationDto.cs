using System;

namespace Services.Dto.Create
{
    public class BaseCreationDto
    {
        public bool IsBlocked { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
