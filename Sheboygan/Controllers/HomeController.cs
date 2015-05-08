using OSGeo.MapGuide;
using Sheboygan.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sheboygan.Controllers
{
    public class HomeController : Controller
    {
        public class SetupInputModel
        {
            [Required]
            public string Username { get; set; }

            [Required]
            public string Password { get; set; }

            public HttpPostedFileBase Package { get; set; }
        }

        // GET: Home
        public ActionResult Index()
        {
            MgUserInformation user = new MgUserInformation("Anonymous", "");
            MgSiteConnection conn = new MgSiteConnection();
            conn.Open(user);

            MgResourceService resSvc = (MgResourceService)conn.CreateService(MgServiceType.ResourceService);

            var vm = new HomeViewModel()
            {
                HasSampleResources = true,
                AjaxLayout = AJAX_LAYOUT,
                FlexLayout = FLEX_LAYOUT
            };

            MgResourceIdentifier mdfId = new MgResourceIdentifier("Library://Samples/Sheboygan/Maps/Sheboygan.MapDefinition");
            MgResourceIdentifier sample1 = new MgResourceIdentifier(vm.AjaxLayout);
            MgResourceIdentifier sample2 = new MgResourceIdentifier(vm.FlexLayout);

            vm.HasSampleResources = resSvc.ResourceExists(mdfId) &&
                                    resSvc.ResourceExists(sample1) &&
                                    resSvc.ResourceExists(sample2);

            return View(vm);
        }

        const string AJAX_LAYOUT = "Library://Samples/Sheboygan/Layouts/SheboyganAspMvc.WebLayout";
        const string FLEX_LAYOUT = "Library://Samples/Sheboygan/FlexibleLayouts/SheboyganAspMvc.ApplicationDefinition";

        // GET: Overview
        public ActionResult Overview()
        {
            return View();
        }

        // GET: Setup
        public ActionResult Setup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoadSampleData(SetupInputModel input)
        {
            MgUserInformation user = new MgUserInformation(input.Username, input.Password);
            MgSiteConnection conn = new MgSiteConnection();
            conn.Open(user);

            MgResourceService resSvc = (MgResourceService)conn.CreateService(MgServiceType.ResourceService);

            //Load the package file if specified
            if (input.Package != null && input.Package.ContentLength > 0)
            {
                var path = Path.GetTempFileName();
                try
                {
                    input.Package.SaveAs(path);
                    MgByteSource bs = new MgByteSource(path);
                    MgByteReader br = bs.GetReader();

                    resSvc.ApplyResourcePackage(br);
                }
                finally
                {
                    try
                    {
                        if (System.IO.File.Exists(path))
                            System.IO.File.Delete(path);
                    }
                    catch { }
                }
            }

            //Load in our sample-specific resources
            MgResourceIdentifier sample1 = new MgResourceIdentifier(AJAX_LAYOUT);
            MgResourceIdentifier sample2 = new MgResourceIdentifier(FLEX_LAYOUT);

            MgByteSource bs1 = new MgByteSource(Server.MapPath("~/App_Data/SheboyganAspMvc.WebLayout.xml"));
            MgByteReader br1 = bs1.GetReader();

            resSvc.SetResource(sample1, br1, null);

            MgByteSource bs2 = new MgByteSource(Server.MapPath("~/App_Data/SheboyganAspMvc.ApplicationDefinition.xml"));
            MgByteReader br2 = bs2.GetReader();

            resSvc.SetResource(sample2, br2, null);

            return RedirectToAction("Index");
        }
    }
}