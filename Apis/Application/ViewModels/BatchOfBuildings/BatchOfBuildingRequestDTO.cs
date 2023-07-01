using Domain.Entities;

namespace Application.ViewModels.BatchOfBuildings
{
    public class BatchOfBuildingRequestDTO
    {
        //public Guid? BatchId { get; set; }
        public Guid? BuildingId { get; set; }
        public virtual Batch? Batch { get; set; }
        public virtual Building? Building { get; set; }
    }
}