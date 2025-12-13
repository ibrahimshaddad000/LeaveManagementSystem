using System.ComponentModel.DataAnnotations;

namespace LeaveManagementSystem.Models.LeaveTypes
{
    public class LeaveTypeEditVM : BaseLeaveTypeVM
    {
        [Required]
        [Length(4, 150, ErrorMessage = "you have violated the length requirements")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(1, 90, ErrorMessage = "Please enter a value between 1 and 90")]
        [Display(Name = "Maximum Number of Days")]
        public int Days { get; set; }
    }



}