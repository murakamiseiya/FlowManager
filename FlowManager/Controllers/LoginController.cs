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

            logger.Debug("Start");
            try
            {
                //入力項目のチェック
                if (!userModel.LoginCheck())
                {
                    logger.Info("入力チェック失敗");
                    logger.Debug("End");
                    //ログイン失敗：LoginPageに遷移する
                    return RedirectToAction(LOGIN_ACTION, LOGIN_CONTROLLER);
                }

                //テーブルよりレコードを取得
                UserContext logintable = new UserContext();
                UserModel LoginTableRecode = logintable.UserDataOnce(userModel.UserName);

                //取得できるレコードがなければ
                if (LoginTableRecode == null)
                {
                    logger.Info("レコード取得失敗");
                    logger.Debug("End");
                    //ログイン失敗：LoginPageに遷移する
                    return RedirectToAction(LOGIN_ACTION, LOGIN_CONTROLLER);
                }

                //入力されたパスワードが一致していない場合
                if (!LoginTableRecode.Password.Equals(userModel.Password))
                {
                    logger.Info("パスワード不一致");
                    logger.Debug("End");
                    //ログイン失敗：LoginPageに遷移する
                    return RedirectToAction(LOGIN_ACTION, LOGIN_CONTROLLER);
                }

                //セッションの登録
                Session[Session_Id] = LoginTableRecode.ID;


                //ログイン成功：AppFormに遷移する
                logger.Info("ログイン成功");
                logger.Debug("End");
                return RedirectToAction(APPFORM_COMMON_ACTION, APPFORM_COMMON_CONTROLLER);

            }catch(Exception e)
            {
                throw e;
            }
        }
    }
}