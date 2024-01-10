using System.ComponentModel.DataAnnotations;

namespace TodoAPI.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; } 
        public string? Status { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy {  get; set; } 
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }

    }
}
