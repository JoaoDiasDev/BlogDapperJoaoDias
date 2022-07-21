using System.ComponentModel.DataAnnotations;

namespace BlogDapperJoaoDias.Entities
{
    public class Article
    {
        [Dapper.Contrib.Extensions.Key]
        public int ArticleId { get; set; }
        public string? Guid { get; set; }
        [Required(ErrorMessage = "Please select a category")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Please enter a name and surname")]
        public string NameSurname { get; set; }
        [Required(ErrorMessage = "Please enter a title")]
        public string Title { get; set; }
        public string? Image { get; set; }
        [Required(ErrorMessage = "Please select a city")]
        public int CityId { get; set; }
        [Required(ErrorMessage = "Please enter a email")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Please enter valid email")]
        public string Email { get; set; }
        public string Phone { get; set; }
        [Required(ErrorMessage = "Please enter a content")]
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int Seen { get; set; }
        public int Status { get; set; }
        public int HomeView { get; set; }
        public int Hit { get; set; }
        public int CommentCount { get; set; }
        public int Slider { get; set; }

    }
}
