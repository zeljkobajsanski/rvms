using System.Configuration;

namespace RVMS.Win
{
    public class ApplicationContext
    {
        private static ApplicationContext _instance = new ApplicationContext();

        public string WebServiceHome { get; private set; }

        public string LoginService { get; private set; }

        public int LogId { get; set; }

        public string KorisnickoIme { get; set; }

        public string ImeIPrezime { get; set; }

        private ApplicationContext()
        {
            WebServiceHome = ConfigurationManager.AppSettings["WebserviceHome"];
            LoginService = ConfigurationManager.AppSettings["LoginService"];
        }

        public static ApplicationContext Current
        {
            get { return _instance; }
        }
    }
}