using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Example.Settings
{
    public enum WEB_API_CONTROLLER_ENUM
    {
        STUDENT_API_CONTROLLER,
        TEAM_API_CONTROLLER,
        COURSE_API_CONTROLLER
    }

    public class ProjectSettings
    {
        private WEB_API_CONTROLLER_ENUM _web_Api_Controller_Enum;
        private string _web_Api_Controller_Url;

        public WEB_API_CONTROLLER_ENUM Web_Api_Controller_Enum
        {
            get
            {
                return (this._web_Api_Controller_Enum);
            }
            set
            {
                this._web_Api_Controller_Enum = value;
            }
        }

        public string Web_Api_Controller_Url
        {
            get
            {
                return (this._web_Api_Controller_Url);
            }
            set
            {
                this._web_Api_Controller_Url = value;
            }
        }

        public ProjectSettings(WEB_API_CONTROLLER_ENUM Web_Api_Controller_Enum,
                               string Web_Api_Controller_Url)
        {
            this._web_Api_Controller_Enum = Web_Api_Controller_Enum;
            this._web_Api_Controller_Url = Web_Api_Controller_Url;
        }
        
    }
}
