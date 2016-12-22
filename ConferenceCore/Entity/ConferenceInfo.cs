using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceCore.Entity
{
    public class ConferenceInfo
    {
        public int Id { set; get; }


        public string Title { set; get; }


        public ConferenceType Type { set; get; }


        public int Time { set; get; }



        public override string ToString()
        {
            return string.Format("id={0},title={1},type={2},Time={3}", Id, Title, Type, Time);
            //return base.ToString();
        }
    }
}
