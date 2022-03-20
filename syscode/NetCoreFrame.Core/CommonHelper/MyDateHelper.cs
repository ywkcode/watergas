using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;


namespace NetCoreFrame.Core.CommonHelper
{
    /// <summary>
    /// 日期处理
    /// </summary>
    public static class MyDateHelper
    {
        /// <summary>
        /// 农历日期
        /// </summary>
        public static readonly string[] ChineseDayName = new string[] {
            "初一","初二","初三","初四","初五","初六","初七","初八","初九","初十",
            "十一","十二","十三","十四","十五","十六","十七","十八","十九","二十",
            "廿一","廿二","廿三","廿四","廿五","廿六","廿七","廿八","廿九","三十"};

        /// <summary>
        /// 月份英文名
        /// </summary>
        public static readonly string[] MonthEnName = new string[] {
            "January","February","March","April"," May"," June","July","August"," September","October",
            "November","December" };

        /// <summary>
        /// 月份中文名
        /// </summary>
        public static readonly string[] MonthChName = new string[] {
            "一月","二月","三月","四月"," 五月"," 六月","七月","八月"," 九月","十月",
            "十一月","十二月" };
        private static ChineseLunisolarCalendar calendar = new ChineseLunisolarCalendar();

        /// <summary>
        /// 获取农历日期
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string GetNongLiDay(DateTime time)
        {
            return ChineseDayName[calendar.GetDayOfMonth(time) - 1];
        }
        /// <summary>
        /// 获取当月天数
        /// </summary>
        /// <returns></returns>
        public static int GetCurrentMonthDays()
        {
            DateTime dtNow = DateTime.Now;
            int days = DateTime.DaysInMonth(dtNow.Year, dtNow.Month);
            return days;
        }
        /// <summary>
        /// 获取当月的月份和英文
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentMonthEn()
        {
            DateTime dtNow = DateTime.Now;
            return MonthChName[dtNow.Month - 1] + "," + MonthEnName[dtNow.Month - 1].ToUpper();
        }
        /// <summary>
        /// 获得日期的星期几
        /// </summary>
        /// <param name="CurrentDate"></param>
        /// <returns></returns>
        public static string GetWeekOf(DateTime CurrentDate)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(CurrentDate.DayOfWeek);
        }

        #region Timestamp 时间戳
        /// <summary>
        /// 将c# DateTime时间格式转换为Unix时间戳格式 
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime GetStampDateTime(long timeStamp)
        {
            DateTime time = new DateTime();
            try
            {
                DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                long lTime = long.Parse(timeStamp + "0000000");
                TimeSpan toNow = new TimeSpan(lTime);
                time = dtStart.Add(toNow);
            }
            catch
            {
                time = DateTime.Now.AddDays(-30);
            }
            return time;
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string Timestamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();

        }
        #endregion
    }
}
