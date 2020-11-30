using ServiceStack.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GuestBookSystem.Models
{
    public class User
    {
        public int UserId { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "邮箱不能为空")]
        /*[Display(Name = "留言人邮箱", Order = 15000)]*/
        [EmailAddress(ErrorMessage ="邮箱格式不对")]
        public string Email { get; set; }
        [Unique]
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "用户名不能为空")]
        [MaxLength(10,ErrorMessage ="用户名长度不能大于10个字符")]
        public string Name { get; set; }
        [System.ComponentModel.DataAnnotations.Required(ErrorMessage = "密码不能为空")]
        [MaxLength(20, ErrorMessage = "密码长度不能大于20个字符")]
        [MinLength(6,ErrorMessage ="密码长度不能少于6个字符")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public SystemRole SRole { get; set; }
        public virtual ICollection<Guestbook> Guestbooks { get; set; }
    }

    public enum SystemRole { 管理员, 普通用户 }
}
