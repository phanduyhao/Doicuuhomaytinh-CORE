﻿using System.ComponentModel.DataAnnotations;

namespace Doicuuhomaytinh_CORE.ModelViews
{
    public class LoginViewModel
    {
        [Key]
        [MaxLength(50)]
        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [Display(Name = "Địa chỉ Email")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Vui lòng nhập địa chỉ Email")]

        public string Email { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [MaxLength(30, ErrorMessage = "Mật khẩu chỉ được sử dụng 30 ký tự")]
        public string Password { get; set; }

    }
}