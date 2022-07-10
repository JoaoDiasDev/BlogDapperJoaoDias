using System.ComponentModel.DataAnnotations;

namespace BlogDapperJoaoDias.Entities
{
    public class Category
    {
        [Dapper.Contrib.Extensions.Key]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Please enter your Category Name")]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
        [Required(ErrorMessage = "Please enter your Slug")]
        [Display(Name = "Slug Name")]
        public string Slug { get; set; }
        public int OrderBy { get; set; }
    }
}
