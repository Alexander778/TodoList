namespace TodoListAppData.Models
{
    public class TodoTask
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Text { get; set; }
        public int StatusId { get; set; }

        public virtual Status Status { get; set; }
        public virtual Category Category { get; set; }
    }
}
