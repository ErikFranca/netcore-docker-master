using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Starline;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace FluxoVendaCartoes
{
    public class Global
    {
        public static IWebDriver driver;
        public static ProcessTest processTest;
        public static StringBuilder verificationErrors;
        public static bool OSLinux = false;
        public static string PathDoProjeto = "";
        public static Actions touchActions;
        public static string printFileName = "";
        public static int reportId;
        public static ClsCCM consulta;
     
        public static String compilaRelatorio()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://54.233.230.119/auto/reports/update.php?customer_name=marisa&rpt_id=" + processTest.ReportID.ToString() + "&rpt_title=Relatorio%20de%20Teste&rpt_obs=&rpt_analyst=Rafael%20Leal");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return new StreamReader(response.GetResponseStream()).ReadToEnd();
                    
        }
    }    
}
