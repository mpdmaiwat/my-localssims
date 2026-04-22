using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SIMSBDAL
{
    public static class ReportCredential
    {
        private static string IsDevelopment = ConfigurationManager.AppSettings.Get("IsDevelopment").ToString();
      
        /** FOR 192.168.3.93 Credential **/
        public static string UsernameAMS = IsDevelopment == "false" ? "ssiitadmin" : "sa";
        public static string PasswordAMS = IsDevelopment == "false" ? "ams@dmin20" :"P@ssw0rd";

        /** FOR 192.168.2.1 Credential **/
        public static string Username2dot1 = "sa";
        public static string Password2dot1 = IsDevelopment == "false" ? "p@ssw0rd" : "P@ssw0rd";

        /** FOR GoDaddy Credential **/
        public static string UsernameGoDaddy = IsDevelopment == "false" ? "ssiitadmin" : "sa";
        public static string PasswordGoDaddy = IsDevelopment == "false" ? "Abc12345@S" : "P@ssw0rd";

    }


}
