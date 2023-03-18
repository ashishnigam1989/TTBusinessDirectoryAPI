using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonService.RequestModel
{
    public class RegionRequestModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Region is requred")]
        public string NameEng { get; set; }

        [Required(ErrorMessage = "Country is requred")]
        public int CountryId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
    }
}
