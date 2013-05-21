namespace RVMS.Model.DTO
{
    public class RelacijaSaMedjustanicnimRastojanjimaDTO
    {
        public int IdRelacije { get; set; }
        public string NazivRelacije { get; set; }
        public MedjustanicnoRastojanjeDTO[] Stanice { get; set; }
    }
}