using ConferenceCore;
using ConferenceCore.Entity;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingTrace
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            ILog log = LogManager.GetLogger("mainLog");

            var list = ConferenceInfoUtil.LoadConferenceInfos();

            //检查数据合法性 范围为（0，60】
            list.CheckConferenceValidation();

            try
            {
                //上午3个，下午5个会议

                Console.WriteLine("上午3个，下午5个会议");

                BaseConferenceSrv.ConferenceTrace(list,SrvType.C_3_H_3,SrvType.C_5_H_4);

                
                //上午4个，下午5个会议
                Console.WriteLine("上午4个，下午5个会议");
                BaseConferenceSrv.ConferenceTrace(list, SrvType.C_4_H_3, SrvType.C_5_H_4);

               

                Console.WriteLine("上午4个，下午5个会议并且有lightning");
                //上午4个，下午5个会议并且有lightning
                BaseConferenceSrv.ConferenceTrace(list, SrvType.C_4_H_3, SrvType.C_5_H_3_Point_5);


                Console.WriteLine("上午5个，下午5个会议并且有lightning");
                //上午5个，下午5个会议并且有lightning
                BaseConferenceSrv.ConferenceTrace(list, SrvType.C_5_H_3, SrvType.C_5_H_3_Point_5);


                Console.WriteLine("上午6个，下午5个会议并且有lightning");
                //上午5个，下午5个会议并且有lightning
                BaseConferenceSrv.ConferenceTrace(list, SrvType.C_6_H_3, SrvType.C_5_H_3_Point_5);

            }
            catch(Exception ex)
            {
                log.Error("error", ex);

                Console.WriteLine(ex.Message + ex.Source + ex.StackTrace);
            }


            Console.Read();
            
        }

     

       
    }
}
