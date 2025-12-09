using BookSeller.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookSeller.Data.ViewModels
{
    public class BookEditViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Họ tên")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Display(Description = "Mô tả")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Display(Name = "Giá")]
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }

        // Existing ImageURL
        public string? ImageURL { get; set; }

        [Display(Name = "Hình ảnh")]
        // Not required for edit, if null we keep the old one
        public IFormFile? PictureFile { get; set; }

        [Display(Name = "Năm xuất bản")]
        [Required(ErrorMessage = "PublishingYear is required")]
        public int PublishingYear { get; set; }

        [Display(Name = "Thể loại")]
        [Required(ErrorMessage = "BookCategory is required")]
        public int CategoryId { get; set; }

        [Display(Name = "Tác giả")]
        [Required(ErrorMessage = "Author is required")]
        public int AuthorId { get; set; }

        [Display(Name = "NXB")]
        [Required(ErrorMessage = "Publisher is required")]
        public int PublisherId { get; set; }
    }
}
