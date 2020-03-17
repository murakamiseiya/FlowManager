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
using System.Text;
using System.Text.RegularExpressions;
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
        /// inset(プレースホルダ)を実行します。
        /// </summary>
        /// <param name="sql">実行するsql</param>
        /// <param name="Parameters">設定するデータ</param>
        /// <returns></returns>
        private bool InsertData(String sql,Dictionary<String,String> Parameters)
        {

            // 接続文字列の取得
            var connectionString = ConfigurationManager.ConnectionStrings["FlowManagerDB"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                try
                {
                    // SQLの設定
                    command.CommandText = sql;

                    //sqlに設定するデータを入れる
                    foreach(KeyValuePair<String,String> param in Parameters)
                    {
                        command.Parameters.Add(new SqlParameter(param.Key, escapeString(param.Value)));
                    }

                    // データベースの接続開始
                    connection.Open();

                    String s = command.ToString();
               

                    // SQLの実行
                    command.ExecuteNonQuery();

                }
                catch (Exception e)
                {
                    throw e;
                    //return false;
                }
                finally
                {
                    // データベースの接続終了
                    connection.Close();
                }
            }
            return true;
        }

        /// <summary>
        /// sqlのエスケープ処理
        /// </summary>
        /// <param name="name">エスケープしたい文字列</param>
        /// <returns>エスケープ処理後の文字列</returns>
        private string escapeString(string name)
        {
            return name;
            //ワイルドカードをエスケープ
            //return "%" + Regex.Replace(name, "[_%\\[#]", "#$0") + "%";
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

        /// <summary>
        /// 新規申請を登録する処理です。
        /// </summary>
        /// <param name="appFormModel">登録データ</param>
        /// <returns>登録成功はtrue,登録失敗はfalse</returns>
        public bool NewAppFormInsert(AppFormViewModel appFormModel)
        {
            try { 
            //ユーザデータの取得
            UserModel user = new UserContext().UserDataOnce(appFormModel.ID);

            //sql作成
            String appForm_sql = "INSERT INTO AppForm_tbl(DeclarationDate,ImplementationDate,AppUser,FormName,NextApprovalManager,NextApprovalBelong,Contents,AppStatus,CreateDate) VALUES(CURRENT_TIMESTAMP,@ImplementationDate, @userID, @FormName, 4, @BelongID, @Contents, 0, CURRENT_TIMESTAMP)";

            //登録するデータ
            Dictionary<String,String> Parameters = new Dictionary<String,String>();
            Parameters.Add("@ImplementationDate", appFormModel.ImplementationDate.ToString());
            Parameters.Add("@userID", appFormModel.UserID.ToString());
            Parameters.Add("@FormName", appFormModel.FormName);
            Parameters.Add("@BelongID", user.BelongID.ToString());
            Parameters.Add("@Contents", appFormModel.Contents);

            //AppForm_tblに登録
            InsertData(appForm_sql, Parameters);

            //申請IDの取得
            string AppFormID = GetData("Select ID From AppForm_tbl order by ID DESC").Rows[0].Field<int>("ID").ToString();

            String Ditail_sql = "INSERT INTO AppFormDetail_tbl(AppFormID, Contents, ProductionAmount, AppEventID, CreateDate) VALUES(@AppFormID, @Contents, @ProductionAmount, @AppEventID, CURRENT_TIMESTAMP)";

            //AppFormDitail_tblの登録処理
            foreach ( AppFormViewModel.AppFormDetail formDetail in appFormModel.ditails)
            {
                Dictionary<String, String> Ditail_Parameters = new Dictionary<String, String>();
                Ditail_Parameters.Add("@AppFormID", AppFormID );
                Ditail_Parameters.Add("@Contents", formDetail.Contents);
                Ditail_Parameters.Add("@ProductionAmount", formDetail.ProductionAmount.ToString());
                Ditail_Parameters.Add("@AppEventID", formDetail.AppLine.ToString());

                InsertData(Ditail_sql, Ditail_Parameters);

            }

            return true;
        }
    }
}