using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreFrame.Core.CommonHelper;
using NetCoreFrame.WebApi.Dto;
using NetCoreFrame.WebApi.Service;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreFrame.WebApi.Controllers
{

    /// <summary>
    ///  DataController
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]

    public class DataController : ControllerBase
    {
        private readonly Water_GasService _water_GasService;
        private readonly Water_QualityService _water_QualityService;
        private readonly Frame_UserService _userservice;
        public DataController(
            Water_GasService water_GasService
            , Water_QualityService water_QualityService
            , Frame_UserService userservice)
        {
            _water_GasService = water_GasService;
            _water_QualityService = water_QualityService;
            _userservice = userservice;
        }
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet("login")]
        public async Task<int> Login(string username, string password)
        {
            int IsSuccess = 1;
            password = MD5Helper.Get32MD5(password);
            var model = await _userservice.AsQueryable().FirstAsync(s => s.LoginID == username && s.Password == password);
            if (model == null) IsSuccess = 0;
            return IsSuccess;
        }



        /// <summary>
        /// 五秒刷新
        /// </summary>
        /// <param name="VariableName"></param>
        /// <param name="IsGas"></param>
        /// <returns></returns>
        [HttpGet("five")]
        public async Task<DataResponse> GetCurrentData(string VariableName, bool IsGas)
        {
            DataResponse dataResponse = new DataResponse();
            ChooseData chosendata = new ChooseData();  //当前选中值
            chosendata.VariableName = VariableName;
            chosendata.ChosenPercent = new Random().NextDouble();

            List<MyTableData> currentList = new List<MyTableData>();
            //水质
            var qualitylist = _water_QualityService.AsQueryable().OrderBy(s => s.CreateTime, SqlSugar.OrderByType.Desc).Take(6).ToList();

            //气体
            var gaslist = _water_GasService.AsQueryable().OrderBy(s => s.CreateTime, SqlSugar.OrderByType.Desc).Take(6).ToList();


            #region 变量
            VariableNames variableNames = new VariableNames();
            variableNames.TOC = qualitylist.First()?.TOC.ToString("0.000") ?? "0.000";
            variableNames.AD = qualitylist.First()?.AD.ToString("0.000") ?? "0.000";
            variableNames.ZL = qualitylist.First()?.ZL.ToString("0.000") ?? "0.000";
            variableNames.PH = qualitylist.First()?.PH.ToString("0.000") ?? "0.000";
            variableNames.LL = qualitylist.First()?.LL.ToString("0.000") ?? "0.000";

            variableNames.H2S = gaslist.First()?.H2S.ToString("0.000") ?? "0.000";
            variableNames.HCL = gaslist.First()?.HCL.ToString("0.000") ?? "0.000";
            variableNames.CL2 = gaslist.First()?.CL2.ToString("0.000") ?? "0.000";
            variableNames.NH3 = gaslist.First()?.NH3.ToString("0.000") ?? "0.000";
            dataResponse.variableNames = variableNames;
            #endregion

            #region 折线图
            ChartData chartData = new ChartData();
            if (IsGas)
            {
                chartData.categories = gaslist.OrderBy(s => s.CreateTime).Select(s => s.CreateTime.ToString("HH:mm")).ToList();
                Series series = new Series();
                series.name = VariableName;
                switch (VariableName)
                {
                    case "H2S":
                        series.data = gaslist.ToList().Select(s => s.H2S.ToString("0.000")).ToList();
                        currentList = gaslist.ToList().Select(s => new MyTableData()
                        {
                            CreateTime = s.CreateTime.ToString("yyyy-MM-dd HH:mm"),
                            CurrentData = s.H2S.ToString("0.000"),
                            IsCorrect = s.H2S < Convert.ToDecimal("0.06") ? true : false

                        }).ToList();
                        chosendata.ChosenData= gaslist.First()?.H2S.ToString("0.000") ?? "0.000";
                        break;
                    case "HCL":
                        series.data = gaslist.ToList().Select(s => s.HCL.ToString("0.000")).ToList();
                        currentList = gaslist.ToList().Select(s => new MyTableData()
                        {
                            CreateTime = s.CreateTime.ToString("yyyy-MM-dd HH:mm"),
                            CurrentData = s.HCL.ToString("0.000"),
                            IsCorrect = s.HCL < Convert.ToDecimal("0.15") ? true : false

                        }).ToList();
                        chosendata.ChosenData = gaslist.First()?.HCL.ToString("0.000") ?? "0.000";
                        break;
                    case "CL2":
                        series.data = gaslist.ToList().Select(s => s.CL2.ToString("0.000")).ToList();
                        currentList = gaslist.ToList().Select(s => new MyTableData()
                        {
                            CreateTime = s.CreateTime.ToString("yyyy-MM-dd HH:mm"),
                            CurrentData = s.CL2.ToString("0.000"),
                            IsCorrect = s.CL2 < Convert.ToDecimal("0.5") ? true : false

                        }).ToList();
                        chosendata.ChosenData = gaslist.First()?.CL2.ToString("0.000") ?? "0.000";
                        break;
                    case "NH3":
                        series.data = gaslist.ToList().Select(s => s.NH3.ToString("0.000")).ToList();
                        currentList = gaslist.ToList().Select(s => new MyTableData()
                        {
                            CreateTime = s.CreateTime.ToString("yyyy-MM-dd HH:mm"),
                            CurrentData = s.NH3.ToString("0.000"),
                            IsCorrect = s.NH3 < Convert.ToDecimal("1.0") ? true : false

                        }).ToList();
                        chosendata.ChosenData = gaslist.First()?.NH3.ToString("0.000") ?? "0.000";
                        break;
                    default:
                        break;
                }
                chartData.series.Add(series);
            }
            else
            {

                chartData.categories = qualitylist.OrderBy(s => s.CreateTime).Select(s => s.CreateTime.ToString("HH:mm")).ToList();
                Series series = new Series();
                series.name = VariableName;
                switch (VariableName)
                {
                    case "TOC":

                        series.data = qualitylist.ToList().Select(s => s.TOC.ToString("0.000")).ToList();
                        currentList = qualitylist.ToList().Select(s => new MyTableData()
                        {
                            CreateTime = s.CreateTime.ToString("yyyy-MM-dd HH:mm"),
                            CurrentData = s.TOC.ToString("0.000"),
                            IsCorrect = s.TOC < Convert.ToDecimal("50") ? true : false

                        }).ToList();
                        chosendata.ChosenData = qualitylist.First()?.TOC.ToString("0.000") ?? "0.000";
                        break;
                    case "AD":
                        series.data = qualitylist.ToList().Select(s => s.AD.ToString("0.000")).ToList();
                        currentList = qualitylist.ToList().Select(s => new MyTableData()
                        {
                            CreateTime = s.CreateTime.ToString("yyyy-MM-dd HH:mm"),
                            CurrentData = s.AD.ToString("0.000"),
                            IsCorrect = s.AD < Convert.ToDecimal("5") ? true : false

                        }).ToList();
                        chosendata.ChosenData = qualitylist.First()?.AD.ToString("0.000") ?? "0.000";
                        break;
                    case "PH":
                        series.data = qualitylist.ToList().Select(s => s.PH.ToString("0.000")).ToList();
                        currentList = qualitylist.ToList().Select(s => new MyTableData()
                        {
                            CreateTime = s.CreateTime.ToString("yyyy-MM-dd HH:mm"),
                            CurrentData = s.PH.ToString("0.000"),
                            IsCorrect = (s.PH < Convert.ToDecimal("6") || s.PH > Convert.ToDecimal("9")) ? false : true
                        }).ToList();
                        chosendata.ChosenData = qualitylist.First()?.PH.ToString("0.000") ?? "0.000";
                        break;
                    case "ZL":
                        series.data = qualitylist.ToList().Select(s => s.ZL.ToString("0.000")).ToList();
                        currentList = qualitylist.ToList().Select(s => new MyTableData()
                        {
                            CreateTime = s.CreateTime.ToString("yyyy-MM-dd HH:mm"),
                            CurrentData = s.ZL.ToString("0.000"),
                            IsCorrect = s.ZL < Convert.ToDecimal("0.5") ? true : false

                        }).ToList();
                        chosendata.ChosenData = qualitylist.First()?.ZL.ToString("0.000") ?? "0.000";
                        break;
                    case "LL":
                        series.data = qualitylist.ToList().Select(s => s.LL.ToString("0.000")).ToList();
                        currentList = qualitylist.ToList().Select(s => new MyTableData()
                        {
                            CreateTime = s.CreateTime.ToString("yyyy-MM-dd HH:mm"),
                            CurrentData = s.LL.ToString("0.000"),
                            IsCorrect = true
                        }).ToList();
                        chosendata.ChosenData = qualitylist.First()?.LL.ToString("0.000") ?? "0.000";
                        break;
                    default:
                        break;
                }

                chosendata.ChosenData = chosendata.ChosenData + (IsGas == true ? "mg/m3" : ((VariableName == "LL" || VariableName == "PH") ? "" : "mg/L"));
                chartData.series.Add(series);

            }
            dataResponse.chartData = chartData;
            dataResponse.chooseData = chosendata;
            #endregion

            #region 列表

            dataResponse.myTableDatas = currentList;
            #endregion

            return dataResponse;
        }
        /// <summary>
        /// 历史数据
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="IsGas"></param>
        /// <returns></returns>
        [HttpGet("history")]
        public async Task<HistoryResponse> GetHistory(int PageSize, bool IsGas)
        {
            HistoryResponse historyResponse = new HistoryResponse();
            if (IsGas)
            {
                historyResponse.namelists = "H2S,HCL,CL2,NH3,日期,状态".Split(',').ToList();
                var datalist = _water_GasService.AsQueryable()
                    .OrderBy(s => s.CreateTime, SqlSugar.OrderByType.Desc)
                    .Take(PageSize)
                    .Select(s => new
                    {
                        data1 = s.H2S,
                        data2 = s.HCL,
                        data3 = s.CL2,
                        data4 = s.NH3,
                        CreateTime = s.CreateTime,
                        IsCorrect = s.IsError
                    }).ToList();
                List<HistoryTableResponse> retlist = new List<HistoryTableResponse>();
                foreach (var model in datalist)
                {
                    retlist.Add(new HistoryTableResponse()
                    {
                        data1 = model.data1.ToString("0.000"),
                        data2 = model.data2.ToString("0.000"),
                        data3 = model.data3.ToString("0.000"),
                        data4 = model.data4.ToString("0.000"),
                        CreateTime = model.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                        IsCorrect = model.IsCorrect
                    });
                }
                historyResponse.historyTableResponses = retlist;
                 
            }
            else
            {

                historyResponse.namelists = "TOC,AD,ZL,PH,LL,日期,状态".Split(',').ToList();
                var  datalist=
                    _water_QualityService.AsQueryable()
                    .OrderBy(s => s.CreateTime, SqlSugar.OrderByType.Desc)
                    .Take(PageSize)
                    .Select(s => new  
                    {
                        data1 = s.TOC,
                        data2 = s.AD,
                        data3 = s.ZL,
                        data4 = s.PH,
                        data5 = s.LL,
                        CreateTime = s.CreateTime,
                        IsCorrect = s.IsError
                    }).ToList();
                List<HistoryTableResponse> retlist = new List<HistoryTableResponse>();
                foreach (var model in datalist)
                {
                    retlist.Add(new HistoryTableResponse()
                    {
                        data1 = model.data1.ToString("0.000"),
                        data2 = model.data2.ToString("0.000"),
                        data3 = model.data3.ToString("0.000"),
                        data4 = model.data4.ToString("0.000"),
                        data5 = model.data5.ToString("0.000"),
                        CreateTime = model.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                        IsCorrect = model.IsCorrect
                    });
                }
                historyResponse.historyTableResponses = retlist;
            }
            return historyResponse;
        }
        /// <summary>
        /// 异常数据
        /// </summary>
        /// <param name="PageSize"></param>
        /// <param name="IsGas"></param>
        /// <returns></returns>
        [HttpGet("error")]
        public async Task<HistoryResponse> GetError(int PageSize, bool IsGas)
        {
            HistoryResponse historyResponse = new HistoryResponse();
            if (IsGas)
            {
                historyResponse.namelists = "H2S,HCL,CL2,NH3,日期,状态".Split(',').ToList();
                historyResponse.historyTableResponses =
                    _water_GasService.AsQueryable()
                    .OrderBy(s => s.CreateTime, SqlSugar.OrderByType.Desc)
                    .Where(s=>s.IsError==true)
                    .Take(PageSize)
                    .Select(s => new HistoryTableResponse()
                    {
                        data1 = s.H2S.ToString("0.000"),
                        data2 = s.HCL.ToString("0.000"),
                        data3 = s.CL2.ToString("0.000"),
                        data4 = s.NH3.ToString("0.000"),
                        CreateTime = s.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                        IsCorrect = s.IsError
                    }).ToList();
            }
            else
            {

                historyResponse.namelists = "TOC,AD,ZL,PH,LL,日期,状态".Split(',').ToList();
                historyResponse.historyTableResponses =
                    _water_QualityService.AsQueryable()
                    .OrderBy(s => s.CreateTime, SqlSugar.OrderByType.Desc)
                     .Where(s => s.IsError == true)
                    .Take(PageSize)
                    .Select(s => new HistoryTableResponse()
                    {
                        data1 = s.TOC.ToString("0.000"),
                        data2 = s.AD.ToString("0.000"),
                        data3 = s.ZL.ToString("0.000"),
                        data4 = s.PH.ToString("0.000"),
                        data5 = s.LL.ToString("0.000"),
                        CreateTime = s.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"),
                        IsCorrect = s.IsError
                    }).ToList();

            }
            return historyResponse;
        }
    }
}
