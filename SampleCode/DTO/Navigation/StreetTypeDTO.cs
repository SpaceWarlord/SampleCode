namespace SampleCode.DTO.Navigation
{
    public class StreetTypeDTO : BaseDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Common { get; set; } = false;

        public StreetTypeDTO(int id, string code, string name, bool commmon)
        {
            Id = id;
            Code = code;
            Name = name;
            Common = commmon;
        }
    }
}
