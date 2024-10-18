namespace Project.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ICollection<Product>? Products { get; set; }= new List<Product>();//ICollection thì I là Interface Collection
    }
}
