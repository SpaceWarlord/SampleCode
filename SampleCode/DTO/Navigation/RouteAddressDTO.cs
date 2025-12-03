namespace SampleCode.DTO.Navigation
{
    public class RouteAddressDTO : BaseDTO
    {
        public int Id { get; set; }
        public RouteDTO Route { get; set; }
        public AddressDTO Address { get; set; }
        public int Order { get; set; }
        public RouteAddressDTO(int id, RouteDTO route, AddressDTO address, int order)
        {
            Id = id;
            Route = route;
            Address = address;
            Order = order;
        }
    }
}
