using ConferenceCore.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConferenceCore
{
    public static class ConferenceInfoUtil
    {
        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <returns></returns>
        public static List<ConferenceInfo> LoadConferenceInfos()
        {

            string fileName = @".\Config\ConferenceCfg.xml";

            XDocument doc = XDocument.Load(fileName);
            
            List<ConferenceInfo> list = new List<ConferenceInfo>();

            var itemList = from item in doc.Descendants("conferenceInfo")  //找到所有conferenceInfo元素
                     select item;


            itemList.ToList().ForEach(item =>
                {
                    list.Add(new ConferenceInfo
                    {
                        Id = Convert.ToInt32(item.Attribute("id").Value),
                        Title = item.Attribute("title").Value,
                        Type = (ConferenceType)Enum.Parse(typeof(ConferenceType), item.Attribute("type").Value),
                        Time = Convert.ToInt32(item.Attribute("time").Value)
                    }); 
                });

            return list;
        }

        /// <summary>
        /// 打印会议清单
        /// </summary>
        /// <param name="list"></param>
        /// <param name="isMorning"></param>
        public static void OutputConferenceTraceStr(this List<ConferenceInfo> list,bool isMorning=true)
        {
            StringBuilder sb = new StringBuilder();

            DateTimeFormatInfo myDTFI = new CultureInfo("en-US").DateTimeFormat;

            DateTime dtStart = DateTime.Now;

            if (isMorning)
            {
                dtStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                    9, 0, 0);
            }
            else
            {
                dtStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                   13, 0, 0);
            }

            foreach (var item in list)
            {

                sb.AppendLine(string.Format("{0}:{1} {2}",
                    dtStart.ToString("T", myDTFI),
                    item.Title,
                    item.Time));

                dtStart = dtStart.AddMinutes(item.Time);
            }

            if (!isMorning)
            {
                dtStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day,
                 17, 0, 0);
                sb.AppendLine(string.Format("{0}:{1}",
                  dtStart.ToString("T", myDTFI),
                  "network event"));
            }
            else
            {
                sb.AppendLine(string.Format("{0}:{1}",
                 dtStart.ToString("T", myDTFI),
                 "Lunch"));
            }

            Console.WriteLine(sb.ToString());

           
        }

        /// <summary>
        /// 检查是否合法范围在（0，60]
        /// </summary>
        /// <param name="list"></param>
        public static void CheckConferenceValidation(this List<ConferenceInfo> list)
        {
            foreach (var item in list)
            {
                if (item.Type == ConferenceType.Fixed)
                {
                    if (item.Time > 60 || item.Time <= 0)
                    {
                        throw new Exception(string.Format("input type error id=[{0}],title=[{1}]", item.Id, item.Title));
                    }
                }
            }
        }
    }
}