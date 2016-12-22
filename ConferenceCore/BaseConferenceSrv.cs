using ConferenceCore.Entity;
using ConferenceCore.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceCore
{
    public abstract class BaseConferenceSrv
    {

        protected ILog log = LogManager.GetLogger(typeof(BaseConferenceSrv));

        /// <summary>
        /// 核心算法如下
        /// 
        /// 1.上午 选择会议时间为3个小时的组合，因为会议时间在5-60分钟之间，并且上午会议没有network event，
        /// 
        /// 所以必须为3个小时。 
        /// 
        /// 最多为3-6个会议。 Cn3，Cn4，Cn5，Cn6.（组合）
        /// 
        /// 2. 下午的会议为可变的。因为有lighting.所以 （5<=lighting<=60）.//
        ///  
        ///   剩下的取值为上午选择之后的数据，数据之和为剩下的数据来求和 范围为[3-4]小时.（因为有network event)
        ///   
        ///   3,3分15，3分30，3分45，4小时。
        /// 
        /// 3. 下面是demo例子，但是情况太多，不在用程序一一列举
        /// 
        /// </summary>
        /// <param name="list">会议内容纪要</param>
        /// <returns>返回Trace集合</returns>
        public abstract List<ConferenceInfo> GetFittingConference(List<ConferenceInfo> list);

     
        private static BaseConferenceSrv CreateInstance(SrvType type)
        {
            BaseConferenceSrv instance = null;
            switch(type)
            {
                case SrvType.C_3_H_3:
                    instance= new _3ConferencesTotal3HoursSrv();
                    break;

                case SrvType.C_4_H_3:
                    instance = new _4ConferencesTotal3HoursSrv();
                    break;
                case SrvType.C_5_H_3:
                    instance = new _5ConferencesTotal3HoursSrv();
                    break;
                case SrvType.C_6_H_3:
                    instance = new _6ConferencesTotal3HoursSrv();
                    break;

                    
                case SrvType.C_5_H_4:
                    instance= new _5ConferencesTotal4HoursSrv();
                    break;

                case SrvType.C_4_H_4:
                    instance= new _4ConferencesTotal3HoursSrv();
                    break;

                case SrvType.C_5_H_3_Point_5:
                    instance= new _5ConferencesTotal3HalfWithLightning();
                    break;
                    
                default:
                    throw new Exception("没有找到实现类！");
            }
            
            

            return instance;
        }



        /// <summary>
        /// 生成会议清单安排
        /// </summary>
        /// <param name="list">会议信息</param>
        /// <param name="morningType">上午类型</param>
        /// <param name="afternoonType">下午类型</param>
        public static void ConferenceTrace(List<ConferenceInfo> list, SrvType morningType, SrvType afternoonType)
        {

            var instance = BaseConferenceSrv.CreateInstance(morningType);

            var morningConferences = instance.GetFittingConference(list);

            morningConferences.OutputConferenceTraceStr();

            var baseDatas = list.Except(morningConferences).ToList();

            var afternoonConferences = BaseConferenceSrv.CreateInstance(afternoonType).GetFittingConference(baseDatas);

            afternoonConferences.OutputConferenceTraceStr(false);
        }


    }


    
}
