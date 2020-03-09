///
/// ファイル名	: LoginContext.cs
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
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace FlowManager.Models.DBAccess
{
    public class LoginContext : DbContext
    {
        public virtual DbSet<UserModel> UserModel_tbl { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LoginContext()
            : base("name=Login")
        {
        }

        /// <summary>
        /// エンティティタイプから慣例により発見されたモデルをさらに構成します。
        /// </summary>
        /// <param name="modelBuilder">このコンテキストのモデルを構築するために使用されているビルダー</param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserModel>()
                .Property(e => e.UserName);
            modelBuilder.Entity<UserModel>()
                .Property(e => e.Password);
        }

        /// <summary>
        /// 指定したユーザのデータを取得します。
        /// </summary>
        /// <param name="userName">検索するユーザ名</param>
        /// <returns>取得したユーザレコード</returns>
        public UserModel UserDataOnce(String userName)
        {
            IQueryable <UserModel> userTable = from usertable in this.UserModel_tbl where( usertable.UserName == userName) select usertable ;

            //もし取得したレコードがなければnull
            if(userTable.ToList().Count == 0)
            {
                return null;
            }

            return userTable.ToList()[0];
        }
    }
}