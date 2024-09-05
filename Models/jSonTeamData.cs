using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Example.Models
{

    //public class Rootobject
    //{
    //    public Class1[] Property1 { get; set; }
    //}

    //public class Class1
    public class jSonTeamData
    {
        public int TeamID { get; set; }
        public string TeamName { get; set; }

        public override string ToString()
        {
            return this.TeamID + " : " + this.TeamName;
        }
    }

}
