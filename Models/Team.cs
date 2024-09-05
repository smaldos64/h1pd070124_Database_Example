using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Example.Models
{
    public class Team
    {
        public int TeamID { get; set; }

        public string TeamName { get; set; }

        public virtual List<Student> Students { get; set; }

        public override string ToString()
        {
            return this.TeamID + " : " + this.TeamName;
        }

        public Team(int TeamID, string TeamName)
        {
            this.TeamID = TeamID;
            this.TeamName = TeamName;
        }

        public Team()
        {

        }
    }
}
