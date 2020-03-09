///
/// ファイル名	: UserModel.cs
/// 作成者		: murakami
/// 制作日		: 2020/3/1
/// 最終更新者	: なし
/// 最終更新日	: なし
///
/// 更新履歴
/// 名前			日付		内容
/// murakami		2020/03/01	新規作成
///
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using FlowManager.Properties;

namespace FlowManager.Models
{
    //ユーザテーブル用モデル
    [Table("User_tbl",Schema = "dbo")]
    public partial class UserModel : BaseModel
    {

        //ユーザID
        [Key]
        public int ID { get; set; }

        //ユーザ名
        [Display(Name = "Login_UserID",ResourceType =typeof(Resources))]
        [Required(ErrorMessageResourceName ="Login_Required_ErrorMsg",ErrorMessageResourceType =typeof(Resources))]
        [MaxLength(20, ErrorMessageResourceName = "Login_MaxLength_ErrorMsg", ErrorMessageResourceType = typeof(Resources))]
        [RegularExpression(@"[a-zA-Z0-9]+", ErrorMessageResourceName = "Login_RegularExpression_ErrorMsg",ErrorMessageResourceType =typeof(Resources))]
        public String UserName { get; set; }

        //パスワード       
        [Display(Name = "Login_Password",ResourceType =typeof(Resources))]
        [Required(ErrorMessageResourceName = "Login_Required_ErrorMsg", ErrorMessageResourceType = typeof(Resources))]
        [MaxLength(20, ErrorMessageResourceName = "Login_MaxLength_ErrorMsg", ErrorMessageResourceType = typeof(Resources))]
        [RegularExpression(@"[a-zA-Z0-9]+", ErrorMessageResourceName = "Login_RegularExpression_ErrorMsg", ErrorMessageResourceType = typeof(Resources))]

        public String Password { get; set; }

        //所属ID
        public int BelongID { get; set; }

        //役職ID
        public int ManagerID { get; set; }

        //作成日
        public DateTime CreateDate { get; set; }

        //更新日
        public Nullable<DateTime> UpdateDate { get; set; }

        /// <summary>
        /// ログイン時入力された項目のチェックを行います。
        /// </summary>
        /// <returns>入力項目が正常であればtrue,異常であればfalse</returns>
        public bool LoginCheck()
        {
            //ユーザ名のチェック
            if (!StringLengthCheck(UserName,20,0) ||
                !StringRequiredCheck(UserName) ||
                !StringProhidition(UserName) )
            {
                //ユーザ名に異常があった場合
                return false;
            }

            // パスワードのチェック
            if (!StringLengthCheck(Password, 20, 0) ||
                !StringRequiredCheck(Password) ||
                !StringProhidition(Password))
            {
                //パスワードに異常があった場合
                return false;
            }

            //全ての項目のチェックが正常に終了した
            return true;
        }
    }
}