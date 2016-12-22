using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceCore
{
   public enum ConferenceType:int
   {
       /// <summary>
       /// 固定时间
       /// </summary>
       Fixed=1,

       /// <summary>
       /// 可变时间在5-60分钟之内变化，并且为5的倍数
       /// </summary>
       Range=2
   }
}
