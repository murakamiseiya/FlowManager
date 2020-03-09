///
/// ファイル名	: BaseController.cs
/// 作成者		: murakami
/// 制作日		: 2020/2/29
/// 最終更新者	: なし
/// 最終更新日	: なし
///
/// 更新履歴
/// 名前			日付		内容
/// murakami		2020/02/29	新規作成
///
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace FlowManager.Controllers
{
    /// <summary>
    /// すべてのコントローラの基底クラス
    /// </summary>
    public class BaseController : Controller
    {

        #region フィールド

        public const string Session_Id = "Id ";    //Sessionの名前「Id」
        public ILog logger; //ロガー

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BaseController()
        {
            //デバッグログの初期化
            logger = LogManager.GetLogger(Assembly.GetExecutingAssembly().FullName);
        }


        /// <summary>
        /// セッション中のIdをチェックし、ログインしているかを確認する
        /// </summary>
        /// <returns>ログイン中：true ログインしていない：false</returns>
        protected bool LoginCheck()
        {
            logger.Debug("Start");
            if (Session[Session_Id] == null)
            {
                //sessionなし
                logger.Debug("SessionIDなし");
                logger.Debug("End");
                return false;
            }

            ////Sessionがあった場合ユーザIDの再登録
            Session[Session_Id] = Session[Session_Id];

            logger.Debug("End");
            return true;
        }

        /// <summary>
        /// 遷移時の最初に実行される処理（アクションメソッドの前に実行される処理）
        /// </summary>
        /// <param name="filterContext">現在のリクエストとアクションに関する情報</param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            logger.Debug("Start");
            //コントローラ名
            String controllerName = RouteData.Values["controller"].ToString();
            //アクション名
            String actionName = RouteData.Values["action"].ToString();

            logger.Info(new StringBuilder().Append("Start ").Append(controllerName).Append(":").Append(actionName));

            //ログイン時以外の場合
            if (!"Login".Equals(controllerName))
            {
                //ログインチェック
                if (!LoginCheck())
                {
                    //ログイン画面へリダイレクト
                    filterContext.Result = RedirectToAction("LoginPage", "Login");
                    logger.Debug("End");
                    return;
                }
            }
            logger.Debug("End");
        }

        /// <summary>
        /// ページ遷移の最後に処理される
        /// </summary>
        /// <param name="filterContext">現在のリクエストとアクションに関する情報</param>
        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            logger.Debug("Start");
            //コントローラ名
            String controllerName = RouteData.Values["controller"].ToString();
            //アクション名
            String actionName = RouteData.Values["action"].ToString();

            logger.Info(new StringBuilder().Append("End ").Append(controllerName).Append(":").Append(actionName));
            logger.Debug("End");
        }

        /// <summary>
        /// アクションでハンドルされない例外が発生したときに呼び出されます。
        /// </summary>
        /// <param name="filterContext">現在の要求およびアクションに関する情報。</param>
        protected override void OnException(ExceptionContext filterContext)
        {
            logger.Debug("Start");
            logger.Error("エラー内容:" + filterContext.Exception.ToString());

            if (typeof(HttpRequestValidationException) == filterContext.Exception.GetType())
            {
                //ここにバリデーションエラー時の処理を書く
            }

            logger.Debug("End");
        }
    }
}