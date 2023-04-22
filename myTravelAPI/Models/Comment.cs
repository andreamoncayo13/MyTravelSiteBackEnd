using System.ComponentModel.DataAnnotations;

namespace myTravelAPI.Models
{
    public class Comment
    {
        public Comment() { 
            this.CreatedDate = DateTime.Now;
        }

        [Key]
        public int CommentId { get; set; }

        [Required]
        [StringLength(100)]
        public string Comments { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [StringLength(100)]
        public DateTime CreatedDate { get; set; }
    }
}
