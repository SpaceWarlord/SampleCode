namespace SampleCode.DTO.Navigation
{
    public class SuburbDTO : BaseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PostCode { get; set; }

        public SuburbDTO(int id, string name, string postCode)
        {
            Id = id;
            Name = name;
            PostCode = postCode;
        }
    }
}
