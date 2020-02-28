using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace FlowManager.Models.FormParam
{
    public class AppFormModel
    {

        [DisplayName("対象日")]
        public String ImplementationDate { get; set; }

        [DisplayName("書類名")]
        public String FormName { get; set; }

        [DisplayName("申請内容")]
        public String contents{ get; set;}

        //申請詳細一覧
        public List<AppFormDetail> ditails { get; set; }

        //コメントの一覧
        public List<CommentModel> comments { get; set; }

        public class AppFormDetail
        {
            [DisplayName("申請内容")]
            public String contents { get; set; }

            [DisplayName("清算金額")]
            public int productionAmount { get; set; }
        }

    }
}