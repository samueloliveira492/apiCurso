namespace curso.domain.Entities
{
    public class Curso: BaseEntity
    {
        public string Status { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Quantity { get; set; }

        public Curso(int Id, string Name, string Description, string Status, string Quantity)
        {
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
            this.Status = Status;
            this.Quantity = Quantity;
        }
    }
}
