using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Guestbook
    {
        public int GuestbookId { get; set; }
        [Required(ErrorMessage = "留言标题不能为空")]
        [MaxLength(20, ErrorMessage = "留言标题不超过20个字符")]
        public string Title { get; set; }
        [Required(ErrorMessage = "留言内容不能为空")]
        [MinLength(10, ErrorMessage = "留言内容不少于10个字符")]
        public string Content { get; set; }
        [Required(ErrorMessage = "留言人的邮箱不能为空")]
        [EmailAddress(ErrorMessage = "email格式不对")]
        public string AuthorEmail { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedOn { get; set; }
    }
}
