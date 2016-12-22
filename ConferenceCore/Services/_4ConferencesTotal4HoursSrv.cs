using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceCore.Services
{
    internal class _4ConferencesTotal4HoursSrv : BaseConferenceSrv
    {

        public override List<Entity.ConferenceInfo> GetFittingConference(List<Entity.ConferenceInfo> list)
        {
            var removedLightningAndHours = list.Where(item =>
                item.Type == ConferenceType.Fixed &&
                item.Time == 60).ToList();

            if (removedLightningAndHours.Count() < 4)
                throw new Exception("小时的整点会议不足4个");

            return removedLightningAndHours.Take(4).ToList();
        }
    }
}
