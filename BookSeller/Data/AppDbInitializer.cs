using BookSeller.Data.Enums;
using BookSeller.Models;

namespace BookSeller.Data
{
        public class AppDbInitializer
        {
            public static void Seed(IApplicationBuilder applicationBuilder)
            {
                using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
                {
                    var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                    context.Database.EnsureCreated();
                    //Tác giả
                    if (!context.Authors.Any())
                    {
                        context.Authors.AddRange(new List<Author>()
                    {                       
                        new Author()
                        {
                            FullName = "Nguyễn Nhật Ánh",
                            Bio = "Giới thiệu tác giả Nguyễn Nhật Ánh",
                            ProfilePictureURL = "nguyen-nhat-anh.jpg"
                        },
                        new Author()
                        {
                            FullName = "Paulo Coelho",
                            Bio = "Giới thiệu tác giả Paulo Coelho",
                            ProfilePictureURL = "Paulo-Coel.jpg"
                        }
                    });
                        context.SaveChanges();
                    }
                    //Nhà Xuất Bản
                    if (!context.Publishers.Any())
                    {
                        context.Publishers.AddRange(new List<Publisher>()
                    {
                        new Publisher()
                        {
                            FullName = "NXB Trẻ",
                            Bio = "This is the Bio of the first publisher",
                            ProfilePictureURL = "nhaxb-tre.png"
                        },
                        new Publisher()
                        {
                            FullName = "NXB Giáo dục",
                            Bio = "This is the Bio of the second publisher",
                            ProfilePictureURL = "nhaxb-giaoduc.png"
                        }
                    });
                        context.SaveChanges();
                    }
                    //movie
                    if (!context.Books.Any())
                    {
                        context.Books.AddRange(new List<Book>()
                    {                       
                        new Book()
                        {
                            Name = "Tôi thấy hoa vàng trên cỏ xanh",
                            Description = "Tôi thấy hoa vàng trên cỏ xanh <br/> Tái bản 2018",
                            Price = 150000,
                            ImageURL = "toi-thay-hoa-vang-tren-co-xanh.jpg",
                            PublishingYear = 2005,
                            AuthorId = 1,
                            PublisherId = 1,
                            BookCategory = BookCategory.TruyenTranh
                        },
                        new Book()
                        {
                            Name = "Chúc một ngày tốt lành",
                            Description = "Mô tả các thứ",
                            Price = 100000,
                            ImageURL = "chuc-mot-ngay-tot-lanh.jpg",
                            PublishingYear = 2007,
                            AuthorId = 1,
                            PublisherId = 1,
                            BookCategory = BookCategory.KinhTe
                        },
                        new Book()
                        {
                            Name = "Nhà Giả Kim",
                            Description = "Mô tả các thứ",
                            Price = 90000,
                            ImageURL = "image_nhagiakim.jpg",
                            PublishingYear = 2010,
                            AuthorId = 2,
                            PublisherId = 2,
                            BookCategory = BookCategory.TieuThuyet
                        }
                    });
                        context.SaveChanges();
                    }
                }
            }
        }
    
}
