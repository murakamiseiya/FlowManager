///
/// ファイル名	: LoginContext.cs
/// 作成者		: murakami
/// 制作日		: 2020/3/10
/// 最終更新者	: なし
/// 最終更新日	: なし
///
/// 更新履歴
/// 名前			日付		内容
/// murakami		2020/03/10	新規作成
///
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FlowManager.Models.FormParam
{
    [Table("AppForm_view")]
    public class AppFormViewModel : BaseModel
    {
  
        public int ID { get; set; }

        [DisplayName("対象日")]
        public DateTime ImplementationDate { get; set; }

        [DisplayName("書類名")]
        public string FormName { get; set; }

        [DisplayName("ユーザID")]
        public int UserID { get; set; }

        [DisplayName("ユーザ名")]
        public string UserName { get; set; }

        [DisplayName("申請内容")]
        public virtual String Contents{ get; set;}

        [DisplayName("管理役職名")]
        public string ManagerName { get; set; }

        [DisplayName("所属名")]
        public string BelongName { get; set; }

        
        //申請詳細一覧
        public virtual List<AppFormDetail> ditails { get; set; }

        //コメントの一覧
        public virtual List<CommentModel> comments { get; set; }

        public class AppFormDetail
        {
            [DisplayName("申請内容")]
            public String Contents { get; set; }

            [DisplayName("清算金額")]
            public int ProductionAmount { get; set; }

            [DisplayName("申請種別")]
            public int AppLine { get; set; }
        }
        
        
        /// <summary>
        /// 新規登録の際の入力項目のチェック
        /// </summary>
        /// <returns>入力項目が正常であればfalse、異常であればtrue</returns>
        public bool NewAppFormCheck()
        {
            //対象日
            if (RequiredCheck(ImplementationDate) )
            {
                //対象日に異常があった場合
                return true;
            }

            //書類名のチェック
            if (StringLengthCheck(FormName, 63, 0) ||
                RequiredCheck(FormName) ||
                StringProhiditionTo2Byte(FormName))
            {
                //書類名に異常があった場合
                return true;
            }

            //申請内容のチェック
            if (StringLengthCheck(Contents, 2147483647, 0) ||
                RequiredCheck(Contents) ||
                StringProhiditionTo2Byte(Contents))
            {
                //申請内容に異常があった場合
                return true;
            }

            //申請詳細のチェック

            foreach(AppFormDetail appFormDetail in ditails)
            {
                //申請内容のチェック
                if (StringLengthCheck(appFormDetail.Contents, 63, 0) ||
                    RequiredCheck(appFormDetail.Contents) ||
                    StringProhiditionTo2Byte(appFormDetail.Contents))
                {
                    //申請内容に異常があった場合
                    return true;
                }

                //清算金額のチェック
                if(IntSizeCheck(appFormDetail.ProductionAmount,0, 1000000000))
                {
                    //清算金額に異常があった場合
                    return true;
                }

                //申請項目のチェック
                if (IntSizeCheck(appFormDetail.AppLine, -1, 5))
                {
                    //申請項目に異常があった場合
                    return true;
                }
                
            }

            //各項目が正常の場合
            return false;
        }


    }
}