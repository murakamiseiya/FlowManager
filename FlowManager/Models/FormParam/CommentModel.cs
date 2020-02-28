using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace FlowManager.Models.FormParam
{
    public class CommentModel
    {
        public int id { get; set; }
        public int manager { get; set;}
        public int appCommentUser { get; set; }
        [DisplayName("コメント")]
        public string comment { get; set; } 
    }
}