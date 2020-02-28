using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FlowManager.Models.FormParam
{
    public class UserModel
    {
        //[Display(Name ="Name", ResourceType = typeof(FlowManager.Properties.Resources))]
        [DisplayName("ログイン")]
        [StringLength(20, ErrorMessage = "{0}は{1}文字以内で入力してください。")]
        public String UserID { get; set; }

        [DisplayName("ユーザ名")]
        public String UserName { get; set; }

        [DisplayName("パスワード")]
        [StringLength(20, ErrorMessage = "{0}は{1}文字以内で入力してください。")]
        public String Password { get; set; }




    }
}