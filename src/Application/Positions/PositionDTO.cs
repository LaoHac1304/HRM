using hrOT.Application.Common.Mappings;

//using hrOT.Application.Levels;
using hrOT.Domain.Entities;

namespace hrOT.Application.Positions;

public class PositionDTO : IMapFrom<Position>
{
    public Guid Id { get; set; }
    public Guid DepartmentId { get; set; }
    public string Name { get; set; }

    //public ICollection<LevelDTO>? Levels { get; set; }
}