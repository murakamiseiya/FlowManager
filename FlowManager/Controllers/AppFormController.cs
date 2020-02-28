using FlowManager.Models.FormParam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlowManager.Controllers
{
    public class AppFormController : Controller
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

        public ActionResult AppFormList()
        {
            return View();
        }

        public ActionResult AppFormDetails()
        {
            AppFormModel model = new AppFormModel();
            model.ImplementationDate = "2019/11/11";
            model.FormName = "経費申請";
            model.contents = "出張に伴う宿泊費の精算";
            model.ditails = new List<AppFormModel.AppFormDetail>();
            model.ditails.Add(new AppFormModel.AppFormDetail());
            model.ditails[0].contents = "夕食";
            model.ditails[0].productionAmount = 2000;
            model.ditails.Add( new AppFormModel.AppFormDetail());
            model.ditails[1].contents = "ホテル代";
            model.ditails[1].productionAmount = 12000;

            return View(model);
        }

        public ActionResult ReAppForm()
        {
            AppFormModel model = new AppFormModel();
            model.ImplementationDate = "2019/11/11";
            model.FormName = "経費申請";
            model.contents = "出張に伴う宿泊費の精算";
            model.ditails = new List<AppFormModel.AppFormDetail>();
            model.ditails.Add(new AppFormModel.AppFormDetail());
            model.ditails[0].contents = "夕食";
            model.ditails[0].productionAmount = 2000;
            model.ditails.Add(new AppFormModel.AppFormDetail());
            model.ditails[1].contents = "ホテル代";
            model.ditails[1].productionAmount = 12000;

            return View(model);
        }

        public ActionResult AjaxSearch(String s)
        {
            return PartialView("テスト成功");
        }
    }
}