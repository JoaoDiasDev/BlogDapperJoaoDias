using BlogDapperJoaoDias.Entities;

namespace BlogDapperJoaoDias.Models
{
    public class ArticleViewModel
    {
        public int ArticleId { get; set; }
        public string? Guid { get; set; }
        public int CategoryId { get; set; }
        public string NameSurname { get; set; }
        public string Title { get; set; }
        public string? Image { get; set; }
        public int CityId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
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
        public Category Category { get; set; }
    }
}
