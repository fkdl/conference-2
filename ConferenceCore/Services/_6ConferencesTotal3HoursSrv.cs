using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceCore.Services
{
    internal class _6ConferencesTotal3HoursSrv : BaseConferenceSrv
    {
        public override List<Entity.ConferenceInfo> GetFittingConference(List<Entity.ConferenceInfo> list)
        {
            var removedLightningAndHours = list.Where(item =>
               item.Type == ConferenceType.Fixed &&
               item.Time == 30).ToList();

            if (removedLightningAndHours.Count() < 6)
                throw new Exception("半小时的整点会议不足6个");

            return removedLightningAndHours.Take(6).ToList();
        }
    }
}
