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
    public class AppFormViewModel
    {
  
        public int ID { get; set; }

        [DisplayName("対象日")]
        public System.DateTime ImplementationDate { get; set; }

        [DisplayName("書類名")]
        public string FormName { get; set; }

        [DisplayName("ユーザ名")]
        public string UserName { get; set; }

        [DisplayName("申請内容")]
        public virtual String contents{ get; set;}

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
            public String contents { get; set; }

            [DisplayName("清算金額")]
            public int productionAmount { get; set; }
        }
        

    }
}