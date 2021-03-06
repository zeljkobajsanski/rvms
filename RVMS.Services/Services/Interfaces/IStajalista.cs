using System.ServiceModel;
using RVMS.Model.DTO;

namespace RVMS.Services.Services.Interfaces
{
    [ServiceContract]
    public interface IStajalista
    {
        [OperationContract]
        StajalisteDTO[] VratiStajalista(int? idOpstine, int? idMesta);

        [OperationContract]
        StajalisteDTO[] VratiSusednaStajalista(int idStajalista);
    }
}