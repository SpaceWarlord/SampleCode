using System.Collections.Generic;

namespace SampleCode.DTO.Navigation
{
    public class RouteDTO : BaseDTO
    {
        public int Id;
        public string Name;
        public List<RouteAddressDTO>? RouteAddresses;
        public float Distance;

        public RouteDTO(int id, string name, List<RouteAddressDTO>? routeAddresses, float distance)
        {
            Id = id;
            Name = name;
            RouteAddresses = routeAddresses;
            Distance = distance;
        }
    }
}
