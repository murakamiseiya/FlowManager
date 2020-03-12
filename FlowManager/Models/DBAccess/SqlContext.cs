///
/// ファイル名	: SqlContext.cs
/// 作成者		: murakami
/// 制作日		: 2020/3/11
/// 最終更新者	: なし
/// 最終更新日	: なし
///
/// 更新履歴
/// 名前			日付		内容
/// murakami		2020/03/11	新規作成
///

using FlowManager.Models.FormParam;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FlowManager.Models.DBAccess
{
    /// <summary>
    /// sql文を実行するクラス
    /// </summary>
    public class SqlContext
    {
        /// <summary>
        /// sql文を実行する
        /// </summary>
        /// <param name="sql">実行したいSQL</param>
        /// <returns>テーブルより取得したデータ</returns>
        private DataTable GetData(String sql)
        {
            var table = new DataTable();

            // 接続文字列の取得
            var connectionString = ConfigurationManager.ConnectionStrings["FlowManagerDB"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                try
                {
                    // データベースの接続開始
                    connection.Open();

                    // SQLの設定
                    command.CommandText = sql ;

                    // SQLの実行
                    var adapter = new SqlDataAdapter(command);
                    adapter.Fill(table);
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    // データベースの接続終了
                    connection.Close();
                }
            }
            return table;
        }

        /// <summary>
        /// AppFormリストからデータを全て取得
        /// </summary>
        /// <returns>AppForm_viewを全て取得</returns>
        public List<AppFormViewModel> AppFormList()
        {
            List<AppFormViewModel> appforms = new List<AppFormViewModel>();

            //データを取得します。
            DataTable AppForm_view = GetData("SELECT * FROM AppForm_view");

            //取得したレコード分データを入れます。
            foreach(DataRow recode in AppForm_view.Rows)
            {
                AppFormViewModel model = new AppFormViewModel();

                model.ID = recode.Field<int>("ID");
                model.ImplementationDate = recode.Field<DateTime>("ImplementationDate");
                model.ManagerName = recode.Field<string>("ManagerName");
                model.UserName = recode.Field<string>("UserName");
                model.FormName = recode.Field<string>("FormName");
                model.BelongName = recode.Field<string>("BelongName");

                appforms.Add(model);
            }

            return appforms;
        }

        /// <summary>
        /// AppFormリストからデータを取得
        /// </summary>
        /// <param name="userID">取得したいユーザID</param>
        /// <returns>AppForm_viewより特定のレコードを取得</returns>
        public List<AppFormViewModel> AppFormList(int userID)
        {
            List<AppFormViewModel> appforms = new List<AppFormViewModel>();

            //データを取得します。
            DataTable AppForm_view = GetData("SELECT * FROM AppForm_view where UserID = " + userID);

            //取得したレコード分データを入れます。
            foreach (DataRow recode in AppForm_view.Rows)
            {
                AppFormViewModel model = new AppFormViewModel();

                model.ID = recode.Field<int>("ID");
                model.ImplementationDate = recode.Field<DateTime>("ImplementationDate");
                model.ManagerName = recode.Field<string>("ManagerName");
                model.UserName = recode.Field<string>("UserName");
                model.FormName = recode.Field<string>("FormName");
                model.BelongName = recode.Field<string>("BelongName");

                appforms.Add(model);
            }

            return appforms;
        }
    }
}