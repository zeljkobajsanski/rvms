using System.Configuration;

namespace RVMS.Win
{
    public class ApplicationContext
    {
        private static ApplicationContext _instance = new ApplicationContext();

        public string WebServiceHome { get; set; }

        private ApplicationContext()
        {
            WebServiceHome = ConfigurationManager.AppSettings["WebserviceHome"];
        }

        public static ApplicationContext Current
        {
            get { return _instance; }
        }
    }
}