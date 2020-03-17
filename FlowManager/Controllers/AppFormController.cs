///
/// ファイル名	: AppFormController.cs
/// 作成者		: murakami
/// 制作日		: 2020/3/12
/// 最終更新者	: なし
/// 最終更新日	: なし
///
/// 更新履歴
/// 名前			日付		内容
/// murakami		2020/03/12	新規作成
///

using FlowManager.Models;
using FlowManager.Models.DBAccess;
using FlowManager.Models.FormParam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;


namespace FlowManager.Controllers
{
    /// <summary>
    /// 申請画面のコントローラクラス
    /// </summary>
    public class AppFormController : BaseController
    {
        // GET: AppForm
        public ActionResult AppForm()
        {
            return View();
        }

        public ActionResult NewAppForm()
        {
            return View();
        }

        /// <summary>
        /// 申請一覧を表示するメソッド
        /// </summary>
        /// <returns>申請一覧ページ</returns>
        public ActionResult AppFormList()
        {
            logger.Debug("Start");

            //役職IDが従業員
            const int EmployeeID = 5; 

            //自身の情報をDBより取得
            UserContext userContext = new UserContext();
            UserModel loginUserModel = userContext.UserDataOnce(SessionUserID());

            //申請情報を取得するためのクラスをインスタンス化
            SqlContext sqlContext = new SqlContext();

            //自身の役職が従業員の場合
            if(loginUserModel.ManagerID == EmployeeID)
            {
                //自分の起票した申請を取得
                ViewBag.appFormViewModels = sqlContext.AppFormList(SessionUserID());
            }
            //自身の役職が従業員以外の場合
            else
            {
                //全ての申請を取得
                ViewBag.appFormViewModels = sqlContext.AppFormList();
            }

            logger.Debug("End");
            return View();
        }

        public ActionResult AppFormDetails()
        {
            //AppFormViewModel model = new AppFormViewModel();
            //model.ImplementationDate = "2019/11/11";
            //model.FormName = "経費申請";
            /*
            model.contents = "出張に伴う宿泊費の精算";
            model.ditails = new List<AppFormViewModel.AppFormDetail>();
            model.ditails.Add(new AppFormViewModel.AppFormDetail());
            model.ditails[0].contents = "夕食";
            model.ditails[0].productionAmount = 2000;
            model.ditails.Add( new AppFormViewModel.AppFormDetail());
            model.ditails[1].contents = "ホテル代";
            model.ditails[1].productionAmount = 12000;
            */
            return View();
        }

        public ActionResult ReAppForm()
        {
            //AppFormViewModel model = new AppFormViewModel();
            //model.ImplementationDate = "2019/11/11";
            //model.FormName = "経費申請";
            /*
            model.contents = "出張に伴う宿泊費の精算";
            model.ditails = new List<AppFormViewModel.AppFormDetail>();
            model.ditails.Add(new AppFormViewModel.AppFormDetail());
            model.ditails[0].contents = "夕食";
            model.ditails[0].productionAmount = 2000;
            model.ditails.Add(new AppFormViewModel.AppFormDetail());
            model.ditails[1].contents = "ホテル代";
            model.ditails[1].productionAmount = 12000;
            */
            return View();
        }

        public ActionResult AjaxSearch(String s)
        {
            return PartialView("テスト成功");
        }

        /// <summary>
        /// セッションよりユーザIDを取得
        /// </summary>
        /// <returns>ユーザIDを返します。ない場合はnullを返します。</returns>
        public int SessionUserID()
        {
            logger.Debug("Start");
            logger.Debug("End");
            return (int)Session[Session_Id];
        }

        /// <summary>
        /// 新規申請登録処理
        /// </summary>
        public ContentResult SaveNewAppForm()
        {
            try
            {
                logger.Debug("Start");
                //申請データ取得
                AppFormViewModel appForm = new AppFormViewModel();
                appForm.ImplementationDate = DateTime.Parse(Request.Form.Get("ImplementationDate"));
                appForm.FormName = Request.Form.Get("FormName");
                appForm.Contents = Request.Form.Get("Contents");
                appForm.ditails = new List<AppFormViewModel.AppFormDetail>();

                //申請詳細データ取得
                for (int i = 0; Request.Form.Get("AppFormDitails[" + i + "].Contents") != null; i++)
                {
                    AppFormViewModel.AppFormDetail appFormDetail = new AppFormViewModel.AppFormDetail();
                    appFormDetail.Contents = Request.Form.Get("AppFormDitails[" + i + "].Contents");
                    appFormDetail.ProductionAmount = int.Parse(Request.Form.Get("AppFormDitails[" + i + "].ProductionAmount"));
                    appFormDetail.AppLine = int.Parse(Request.Form.Get("AppFormDitails[" + i + "].AppLine"));
                    appForm.ditails.Add(appFormDetail);

                }

                //新規登録の入力項目のチェックを行う
                if (appForm.NewAppFormCheck())
                {
                    logger.Debug("End");
                    return this.Content("false");

                }

                //ユーザIDの設定
                appForm.UserID = SessionUserID();

                SqlContext sqlContext = new SqlContext();
                if (!sqlContext.NewAppFormInsert(appForm))
                {
                    logger.Debug("End");
                    return this.Content("false");
                }
                logger.Debug("End");
                return this.Content("true");
            }
            catch(Exception e)
            {
                logger.Error(e.Message);
                logger.Debug("End");
                return this.Content("false");
            }
        }
    }
}