using ConferenceCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceCore.Services
{
    /// <summary>
    /// 3个会议组成3个小时(上午无lightning）
    /// </summary>
    internal class _3ConferencesTotal3HoursSrv:BaseConferenceSrv
    {

        public override List<ConferenceInfo> GetFittingConference(List<ConferenceInfo> list)
        {
            var removedLightningAndHours = list.Where(item => 
                item.Type == ConferenceType.Fixed && 
                item.Time == 60).ToList();

            if (removedLightningAndHours.Count() < 3)
                throw new Exception("小时的整点会议不足3个");

            return removedLightningAndHours.Take(3).ToList();
        }
    }
}
