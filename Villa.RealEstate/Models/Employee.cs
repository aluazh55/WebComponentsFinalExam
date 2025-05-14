namespace Villa.RealEstate.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int JobPositionId { get; set; }
        public JobPosition JobPosition { get; set; }
        public string Bio { get; set; }
        public byte[] Image { get; set; }
    }

    public class JobPosition
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
