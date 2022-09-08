using System;

namespace Services.Dto.Update
{
    public class BaseUpdateDto
    {
        public bool IsBlocked { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
