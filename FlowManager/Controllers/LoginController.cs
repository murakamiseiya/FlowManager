///
/// ファイル名	: LoginController.cs
/// 作成者		: murakami
/// 制作日		: 2020/2/29
/// 最終更新者	: なし
/// 最終更新日	: なし
///
/// 更新履歴
/// 名前			日付		内容
/// murakami		2020/02/29	新規作成
///
using FlowManager.Models;
using FlowManager.Models.DBAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlowManager.Controllers
{
   
    /// <summary>
    /// ログイン用コントローラ
    /// </summary>
    public class LoginController : BaseController
    {
        /// <summary>
        /// ログインページ表示処理
        /// </summary>
        /// <returns>LoginPage</returns>
        public ActionResult LoginPage()
        {

            return View();
        }

        /// <summary>
        /// ログインできるかのチェックを行います。
        /// 成功であればメインページ
        /// 失敗であればログインページ
        /// に遷移します。
        /// </summary>
        /// <param name="userModel">入力値</param>
        /// <returns>表示するページ</returns>
        public ActionResult LoginCheck(UserModel userModel )
        {
            //入力項目のチェック
            if (!userModel.LoginCheck())
            {
                logger.Info("入力チェック失敗");
                //ログイン失敗：LoginPageに遷移する
                return RedirectToAction("LoginPage", "Login");
            }

            //テーブルよりレコードを取得
            LoginContext logintable = new LoginContext();
            UserModel LoginTableRecode = logintable.UserDataOnce(userModel.UserName);

            //取得できるレコードがなければ
            if( LoginTableRecode == null)
            {
                logger.Info("レコード取得失敗");
                //ログイン失敗：LoginPageに遷移する
                return RedirectToAction("LoginPage", "Login");
            }

            //入力されたパスワードが一致していない場合
            if (!LoginTableRecode.Password.Equals(userModel.Password))
            {
                logger.Info("パスワード不一致");
                //ログイン失敗：LoginPageに遷移する
                return RedirectToAction("LoginPage", "Login");
            }

            String Session_ID = "UserID";
            //セッションの登録
            Session[Session_ID] = LoginTableRecode.ID;


            //ログイン成功：AppFormに遷移する
            logger.Info("ログイン成功");
            return RedirectToAction("AppForm", "AppForm");

            
        }
    }
}