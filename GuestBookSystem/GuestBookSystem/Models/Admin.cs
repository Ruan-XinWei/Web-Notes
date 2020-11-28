using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GuestBookSystem.Models
{
    public class Admin
    {
        public int AdminId { get; set; }
        [DisplayName("用户名")]
        [Required(ErrorMessage = "用户名不能为空")]
        public string Name { get; set; }
        [DisplayName("密码")]
        [Required(ErrorMessage = "密码不能为空")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
