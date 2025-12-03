namespace SampleCode.DTO.Navigation
{
    public class AddressDTO : BaseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? UnitNum { get; set; }
        public string StreetNum { get; set; }
        public string StreetName { get; set; }
        public StreetTypeDTO StreetType { get; set; }
        public SuburbDTO Suburb { get; set; }
        public string City { get; set; }
        public string? GPS { get; set; }

        public AddressDTO(int id, string name, string? unitNum, string streetNum, string streetName, StreetTypeDTO streetType, SuburbDTO suburb, string city, string? gps)
        {
            Id = id;
            Name = name;
            UnitNum = unitNum;
            StreetNum = streetNum;
            StreetName = streetName;
            StreetType = streetType;
            Suburb = suburb;
            City = city;
            GPS = gps;
        }
    }
}
