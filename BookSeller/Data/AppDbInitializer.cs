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
                    //actor
                    if (!context.Authors.Any())
                    {
                        context.Authors.AddRange(new List<Author>()
                    {
                        new Author()
                        {
                            FullName = "Author 1",
                            Bio = "This is the Bio of the first actor",
                            ProfilePictureURL = "http://dotnethow.net/images/actors/actor-1.jpeg"

                        },
                        new Author()
                        {
                            FullName = "Author 2",
                            Bio = "This is the Bio of the second actor",
                            ProfilePictureURL = "http://dotnethow.net/images/actors/actor-2.jpeg"
                        },
                        new Author()
                        {
                            FullName = "Author 3",
                            Bio = "This is the Bio of the second actor",
                            ProfilePictureURL = "http://dotnethow.net/images/actors/actor-3.jpeg"
                        },
                        new Author()
                        {
                            FullName = "Author 4",
                            Bio = "This is the Bio of the second actor",
                            ProfilePictureURL = "http://dotnethow.net/images/actors/actor-4.jpeg"
                        },
                        new Author()
                        {
                            FullName = "Author 5",
                            Bio = "This is the Bio of the second actor",
                            ProfilePictureURL = "http://dotnethow.net/images/actors/actor-5.jpeg"
                        }
                    });
                        context.SaveChanges();
                    }
                    //producer
                    if (!context.Publishers.Any())
                    {
                        context.Publishers.AddRange(new List<Publisher>()
                    {
                        new Publisher()
                        {
                            FullName = "Publisher 1",
                            Bio = "This is the Bio of the first actor",
                            ProfilePictureURL = "http://dotnethow.net/images/producers/producer-1.jpeg"

                        },
                        new Publisher()
                        {
                            FullName = "Publisher 2",
                            Bio = "This is the Bio of the second actor",
                            ProfilePictureURL = "http://dotnethow.net/images/producers/producer-2.jpeg"
                        },
                        new Publisher()
                        {
                            FullName = "Publisher 3",
                            Bio = "This is the Bio of the second actor",
                            ProfilePictureURL = "http://dotnethow.net/images/producers/producer-3.jpeg"
                        },
                        new Publisher()
                        {
                            FullName = "Publisher 4",
                            Bio = "This is the Bio of the second actor",
                            ProfilePictureURL = "http://dotnethow.net/images/producers/producer-4.jpeg"
                        },
                        new Publisher()
                        {
                            FullName = "Publisher 5",
                            Bio = "This is the Bio of the second actor",
                            ProfilePictureURL = "http://dotnethow.net/images/producers/producer-5.jpeg"
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
                            Name = "Life",
                            Description = "This is the Life movie description",
                            Price = 39.50,
                            ImageURL = "http://dotnethow.net/images/movies/movie-3.jpeg",
                            PublishingYear = DateTime.Now.AddYears(-2),
                            AuthorId = 3,
                            PublisherId = 3,
                            BookCategory = BookCategory.TruyenNgan
                        },
                        new Book()
                        {
                            Name = "The Shawshank Redemption",
                            Description = "This is the Shawshank Redemption description",
                            Price = 29.50,
                            ImageURL = "http://dotnethow.net/images/movies/movie-1.jpeg",
                            PublishingYear = DateTime.Now,
                            AuthorId = 1,
                            PublisherId = 1,
                            BookCategory = BookCategory.NgoaiNgu
                        },
                        new Book()
                        {
                            Name = "Ghost",
                            Description = "This is the Ghost movie description",
                            Price = 39.50,
                            ImageURL = "http://dotnethow.net/images/movies/movie-4.jpeg",
                            PublishingYear = DateTime.Now,
                            AuthorId = 4,
                            PublisherId = 4,
                            BookCategory = BookCategory.TieuThuyet
                        },
                        new Book()
                        {
                            Name = "Race",
                            Description = "This is the Race movie description",
                            Price = 39.50,
                            ImageURL = "http://dotnethow.net/images/movies/movie-6.jpeg",
                            PublishingYear = DateTime.Now.AddYears(-3),
                            AuthorId = 1,
                            PublisherId = 2,
                            BookCategory = BookCategory.TruyenTranh
                        },
                        new Book()
                        {
                            Name = "Scoob",
                            Description = "This is the Scoob movie description",
                            Price = 39.50,
                            ImageURL = "http://dotnethow.net/images/movies/movie-7.jpeg",
                            PublishingYear = DateTime.Now.AddYears(-4),
                            AuthorId = 1,
                            PublisherId = 3,
                            BookCategory = BookCategory.KinhTe
                        },
                        new Book()
                        {
                            Name = "Cold Soles",
                            Description = "This is the Cold Soles movie description",
                            Price = 39.50,
                            ImageURL = "http://dotnethow.net/images/movies/movie-8.jpeg",
                            PublishingYear = DateTime.Now.AddYears(-6),
                            AuthorId = 1,
                            PublisherId = 5,
                            BookCategory = BookCategory.TieuThuyet
                        }
                    });
                        context.SaveChanges();
                    }
                }
            }
        }
    
}
