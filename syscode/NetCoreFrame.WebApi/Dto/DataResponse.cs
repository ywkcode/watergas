using System.Collections.Generic;

namespace NetCoreFrame.WebApi.Dto
{
    public class ChooseData
    { 
       public string VariableName { get; set; }

        public string ChosenData { get; set; }

        public double ChosenPercent { get; set; }
    }
    public class DataResponse
    {
        public DataResponse()
        {
            this.myTableDatas = new List<MyTableData>();
        }

        public ChooseData chooseData { get; set; }

        /// <summary>
        /// 变量集合
        /// </summary>
        public  VariableNames  variableNames{ get;set;}

        /// <summary>
        /// 折线图数据
        /// </summary>
        public ChartData chartData{ get;set;}

        /// <summary>
        /// 列表信息
        /// </summary>
        public List<MyTableData> myTableDatas{ get;set;}
    }

    public class VariableNames
    { 
        public string TOC { get; set; }
        public string AD { get; set; }
        public string ZL { get; set; }

        public string PH { get; set; }
        public string LL { get; set; }
        public string H2S { get; set; }
        public string HCL { get; set; }
        public string CL2 { get; set; }
        public string NH3 { get; set; }
    }

    public class ChartData
    {
        public ChartData()
        {
            this.categories = new List<string>();
            this.series = new List<Series>();
        }
        public List<string> categories { get; set; }
        public List<Series> series { get; set; }
    }
    public class Series
    {
        public Series()
        {
            this.data = new List<string>();
        }
       public string name { get; set; }

        public List<string> data { get; set; }
    }

    public class MyTableData
    { 
        public string CreateTime { get; set; }

        public string CurrentData { get; set; }

        public bool IsCorrect { get; set; }
    }

    public class HistoryTableResponse
    { 
       public string data1 { get; set; }

       public string data2 { get; set; }

        public string data3 { get; set; }
        public string data4 { get; set; }

        public string data5 { get; set; } 

        public string CreateTime { get; set; }
        public bool IsCorrect { get; set; }
    }

    public class HistoryResponse
    {
        public HistoryResponse()
        {
            this.namelists = new List<string>();
            this.historyTableResponses = new List<HistoryTableResponse>();
        }
        public List<string> namelists { get; set; }

        public List<HistoryTableResponse> historyTableResponses { get; set; }
    }

    
    
}
