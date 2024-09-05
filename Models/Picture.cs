using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Example.Models
{
    public class Picture
    {
        public int PictureID { get; set; }

        public string PictureName { get; set; }

        public int StudentID { get; set; }
        public virtual Student Student { get; set; }
    }
}
