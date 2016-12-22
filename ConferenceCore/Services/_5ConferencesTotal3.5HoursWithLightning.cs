using ConferenceCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceCore.Services
{
    internal class _5ConferencesTotal3HalfWithLightning : BaseConferenceSrv
    {
        public override List<ConferenceInfo> GetFittingConference(List<Entity.ConferenceInfo> list)
        {
            var removedLightnings = list.Where(item => item.Type == ConferenceType.Fixed).ToList();

            List<ConferenceInfo> resList = new List<ConferenceInfo>();

            for (int i1 = 0; i1 < removedLightnings.Count(); i1++)
            {
                for (int i2 = 0; i2 < removedLightnings.Count(); i2++)
                {

                    for (int i3 = 0; i3 < removedLightnings.Count(); i3++)
                    {

                        for (int i4 = 0; i4 < removedLightnings.Count(); i4++)
                        {
                            for (int i5 = 0; i5 < removedLightnings.Count(); i5++)
                            {
                                if (removedLightnings[i1].Time +
                                    removedLightnings[i2].Time +
                                    removedLightnings[i3].Time +
                                    removedLightnings[i4].Time +
                                    removedLightnings[i5].Time == 210
                                    )
                                {
                                    resList.Add(removedLightnings[i1]);
                                    resList.Add(removedLightnings[i2]);
                                    resList.Add(removedLightnings[i3]);
                                    resList.Add(removedLightnings[i4]);
                                    resList.Add(removedLightnings[i5]);

                                    var count = resList.GroupBy(item => item.Id).Count();

                                    if (count == 5)
                                    {
                                        resList.Add(list.FirstOrDefault(item => item.Type == ConferenceType.Range));
                                        return resList;
									}
                                       
                                    else
                                        resList.Clear();
                                }
                            }
                        }
                    }
                }
            }

            return resList;
        }
    }
}
