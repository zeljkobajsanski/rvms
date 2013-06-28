using System.ServiceModel;

namespace RVMS.Services.Services
{
    [ServiceContract]
    public class Test
    {
         [OperationContract]
         public void Ping()
         {
             
         }
    }
}