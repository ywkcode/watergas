using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetCoreFrame.Core.Request;
using NetCoreFrame.Core.Response;
using NetCoreFrame.Entity;
using NetCoreFrame.Entity.FrameEntity;
using NetCoreFrame.Repository.Interface;
using System.Linq.Expressions;
using NetCoreFrame.Core.Utils;
using System.Data;
using System.Collections;
using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace NetCoreFrame.Service
{
    /// <summary>
    /// Frame_Charts
    /// </summary>
    public class Frame_ChartsService : BaseService<Frame_Charts>
    {

        private NetCoreFrameDBContext _dbContext;
        public Frame_ChartsService(IRepository<Frame_Charts> repository
            , NetCoreFrameDBContext dBContext) : base(repository)
        {

            _dbContext = dBContext;
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="role"></param>
        public void Add(Frame_Charts model)
        {
            _repository.Add(model);
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public TableData Load(PageRequest request, Frame_Charts model)
        {

            Expression<Func<Frame_Charts, bool>> expression = t => true;
            if (!string.IsNullOrEmpty(model.ChartInfo))
            {
                expression = expression.And(t => t.ChartInfo.Contains(model.ChartInfo));
            }
            if (!string.IsNullOrEmpty(model.DataBase))
            {
                expression = expression.And(t => t.DataBase.Contains(model.DataBase));
            }
            if (!string.IsNullOrEmpty(model.ChartType))
            {
                expression = expression.And(t => t.ChartType.Contains(model.ChartType));
            }
            if (!string.IsNullOrEmpty(model.DataSQL))
            {
                expression = expression.And(t => t.DataSQL.Contains(model.DataSQL));
            }
            if (!string.IsNullOrEmpty(model.ChartTitle))
            {
                expression = expression.And(t => t.ChartTitle.Contains(model.ChartTitle));
            }
            if (!string.IsNullOrEmpty(model.ChartStyle))
            {
                expression = expression.And(t => t.ChartStyle.Contains(model.ChartStyle));
            }

            var datalist = _repository.Find(request.page, request.limit, "CreateTime desc", expression);
            return new TableData
            {
                code=200,
                count = datalist.Count(),
                data = datalist
            };

        }

        public TableData ToDataTable(string chartid)
        {
            TableData tableData = new TableData();
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(chartid))
            {
                sql = Get(chartid)?.DataSQL??""; 
            }
            //DataTable dt = Query(sql, null);
            List<string> headlist = new List<string>();
            //for (int i = 0; i < dt.Columns.Count; i++)
            //{
            //    headlist.Add(dt.Columns[i].ColumnName);
            //}
            //DataView dv = dt.DefaultView;

            //List<ArrayList> list = new List<ArrayList>();
            //foreach (DataRowView dr in dv)

            //{
            //    ArrayList arrlist = new ArrayList();
            //    foreach (var name in headlist)
            //    {
            //        arrlist.Add(dr[name]);
            //    }
            //    list.Add(arrlist);
            //}

            //tableData.data = new
            //{
            //    headlist = headlist,
            //    datalist = list
            //};
            return tableData;
        }

        //private DataTable Query(string SQL, List<MySqlParameter> out_params)
        //{
        //    DataTable dataTable = new DataTable();
        //    DataTable result;
        //    var conn = _dbContext.Database.GetDbConnection();
        //    DbCommand dbCommand = conn.CreateCommand(); 
        //    dbCommand.CommandText = SQL;
        //    if (out_params != null)
        //    {
        //        dbCommand.Parameters.AddRange(out_params.ToArray());
        //    }

        //    try
        //    {
        //        conn.Open();
        //        DbDataReader dbDataReader = dbCommand.ExecuteReader();
        //        dataTable.Load(dbDataReader);
        //        dbDataReader.Close();
        //    }
        //    catch (Exception ex)
        //    {

        //        dataTable = null;
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //    result = dataTable;
        //    return result;
        //}

        public class piedata { 
           public string value { get; set; }

            public string name { get; set; }
        }
        public TableData PieChart(string chartid)
        {
            TableData tableData = new TableData();
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(chartid))
            {
                sql = Get(chartid)?.DataSQL ?? "";
            }
            //DataTable dt = Query(sql, null);
            //List<string> headlist = new List<string>();
            //List<string> showheadlist = new List<string>();
            //for (int i = 0; i < dt.Columns.Count; i++)
            //{
            //    headlist.Add(dt.Columns[i].ColumnName);
            //}
            //DataView dv = dt.DefaultView;

            //List<piedata> list = new List<piedata>();
            //foreach (DataRowView dr in dv) 
            //{
            //    piedata model = new piedata();
            //    model.name = dr[headlist[0]].ToString();
            //    showheadlist.Add(model.name);
            //    model.value = dr[headlist[1]].ToString();
            //    list.Add(model); 
            //}

           
            //tableData.data = new
            //{
            //    headlist = showheadlist,
            //    datalist = list
            //};
            return tableData;
        }

        public TableData ColmunChart(string chartid)
        {
            TableData tableData = new TableData();
            string sql = string.Empty;
            if (!string.IsNullOrEmpty(chartid))
            {
                sql = Get(chartid)?.DataSQL ?? "";
            }
            //DataTable dt = Query(sql, null);
            //List<string> headlist = new List<string>();
            //List<string> showheadlist = new List<string>();
            //List<string> showdatalist = new List<string>();
            //for (int i = 0; i < dt.Columns.Count; i++)
            //{
            //    headlist.Add(dt.Columns[i].ColumnName);
            //}
            //DataView dv = dt.DefaultView;

           
            //foreach (DataRowView dr in dv)
            //{
            //    showheadlist.Add(dr[headlist[0]].ToString());
            //    showdatalist.Add(dr[headlist[1]].ToString()); 
            //}


            //tableData.data = new
            //{
            //    headlist = showheadlist,
            //    datalist = showdatalist
            //};
            return tableData;
        }
    }
}
