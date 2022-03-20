using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using NetCoreFrame.Entity;
using NetCoreFrame.Entity.ViewModel;
using NetCoreFrame.Service;
using NetCoreFrame.WebUI.Extensions;
using NetCoreFrame.WebUI.Models;
using NetCoreFrame.WebUI.Views.Hubs;

namespace NetCoreFrame.WebUI.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHubContext<SignalrHubs> _countHub;
        private readonly productionstationService _productionstationService;
        private readonly NetCoreFrameDBContext _dbContext;
        public HomeController(
            ILogger<HomeController> logger,
            IHubContext<SignalrHubs> countHub,
            NetCoreFrameDBContext dbContext,
            productionstationService productionstationService)
        {
            _logger = logger;
            _countHub = countHub;
            _productionstationService = productionstationService;
            _dbContext = dbContext;
        }

        public async Task Send(string msg, string id)
        {
            await _countHub.Clients.All.SendAsync("AddMsg", $"{id}：{msg}");
            await _countHub.Clients.User("22").SendAsync("", "");
        }

        public IActionResult Index()
        {

            ViewBag.UserName = CurrentUser.UserName;
            ViewBag.UserID = CurrentUser.ID;
            ViewBag.RoleID = CurrentUser.RoleID;
            return View();
        }

        public IActionResult DataIndex()
        {
           
            return View();
        }

        [HttpGet("home/board/data")]
        public WaterResponse GetFive(bool isgas)
        {
            WaterResponse data = new WaterResponse();
            List<WaterData> waterDatas = new List<WaterData>();
            if (isgas)
            {
                var gas = _dbContext.Water_Gas.OrderByDescending(s => s.CreateTime).FirstOrDefault();
                //气体
                data.waterDatas.Add(new WaterData() { DataName = "硫化氢(mg/m3)", Data_Stand = "0.060", Data_Real = gas?.H2S.ToString("0.000") ?? "0.000" });
                data.waterDatas.Add(new WaterData() { DataName = "氯化氢(mg/m3)", Data_Stand = "0.150", Data_Real = gas?.HCL.ToString("0.000") ?? "0.000" });
                data.waterDatas2.Add(new WaterData() { DataName = "氯气(mg/m3)", Data_Stand = "0.500", Data_Real = gas?.CL2.ToString("0.000") ?? "0.000" });
                data.waterDatas2.Add(new WaterData() { DataName = "氨气(mg/m3)", Data_Stand = "1.000", Data_Real = gas?.NH3.ToString("0.000") ?? "0.000" });

            }
            else
            {
                var quality = _dbContext.Water_Quality.OrderByDescending(s => s.CreateTime).FirstOrDefault(); 
                //水质
                data.waterDatas.Add(new WaterData() { DataName = "COD(mg/L)", Data_Stand = "50.000", Data_Real = quality?.TOC.ToString("0.000") ?? "0.000" });
                data.waterDatas.Add(new WaterData() { DataName = "氨氮(mg/L)", Data_Stand = "50.000", Data_Real = quality?.AD.ToString("0.000") ?? "0.000" });
                data.waterDatas2.Add(new WaterData() { DataName = "总磷(mg/L)", Data_Stand = "0.500", Data_Real = quality?.ZL.ToString("0.000") ?? "0.000" });
                data.waterDatas2.Add(new WaterData() { DataName = "PH(无量纲)", Data_Stand = "6-9", Data_Real = quality?.PH.ToString("0.000") ?? "0.000" });
            } 
            
            data.NowDate = DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss");
            return data;
        }

          

        public IActionResult ImagesView()
        {

            ViewBag.ImgList = _productionstationService.GetProductionImgList(CurrentUser.UserName);
            ViewBag.ID = CurrentUser.UserName;
            return View();
        }
        public IActionResult Privacy()
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
