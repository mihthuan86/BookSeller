using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookSeller.Data;
using BookSeller.Models;
using Microsoft.AspNetCore.Hosting;
using NuGet.Protocol;
using Microsoft.AspNetCore.Authorization;
using BookSeller.Data.Static;
using BookSeller.Data.ViewModels;
using BookSeller.Data.Service;

namespace BookSeller.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class BooksController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IFileUploadService _fileUploadService;

        public BooksController(AppDbContext context, IFileUploadService fileUploadService)
        {
            _context = context;
            _fileUploadService = fileUploadService;
        }

        [AllowAnonymous]
        // GET: Books
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Books.Include(b => b.Author).Include(b => b.Publisher).Include(b =>b.Category);
            return View(await appDbContext.ToListAsync());
        }

        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allBooks = from b in _context.Books.Include(b => b.Author).Include(b => b.Publisher).Include(b => b.Category) select b;
            if (!string.IsNullOrEmpty(searchString))
            {
                var filterBooks = allBooks.Where(s=>s.Name.Contains(searchString)|| s.Author.FullName.Contains(searchString)).ToList();
                return View("Index", filterBooks);
            }
            return View("Index",allBooks.ToList());
        }

        [AllowAnonymous]
        public async Task<IActionResult> Category(int category)
        {
            var allBooks = from b in _context.Books.Include(b => b.Author).Include(b => b.Publisher).Include(b => b.Category) select b;           
            var filterBooks = allBooks.Where(s => ((int)s.CategoryId) == category).ToList();
            return View("Index", filterBooks);                   
        }

       [AllowAnonymous]
        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }       

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(_context.Authors.OrderBy(n => n.FullName).ToList(),"Id","FullName");
            ViewBag.PublisherId = new SelectList(_context.Publishers.OrderBy(n => n.FullName).ToList(), "Id", "FullName");
            ViewBag.CategoryId = new SelectList(_context.Categories.OrderBy(n => n.Name).ToList(), "Id", "Name");
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookViewModel bookVM)
        {
            if (ModelState.IsValid)
            {
                string fileName = await _fileUploadService.UploadFileAsync(bookVM.PictureFile, AppConstants.BookImagePath);

                var book = new Book
                {
                    Name = bookVM.Name,
                    Description = bookVM.Description,
                    Price = bookVM.Price,
                    ImageURL = fileName,
                    PublishingYear = bookVM.PublishingYear,
                    CategoryId = bookVM.CategoryId,
                    AuthorId = bookVM.AuthorId,
                    PublisherId = bookVM.PublisherId
                };

                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.AuthorId = new SelectList(_context.Authors.OrderBy(n => n.FullName).ToList(), "Id", "FullName", bookVM.AuthorId);
            ViewBag.PublisherId = new SelectList(_context.Publishers.OrderBy(n => n.FullName).ToList(), "Id", "FullName", bookVM.PublisherId);
            ViewBag.CategoryId = new SelectList(_context.Categories.OrderBy(n => n.Name).ToList(), "Id", "Name", bookVM.CategoryId);
            return View(bookVM);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            var bookVM = new BookEditViewModel
            {
                Id = book.Id,
                Name = book.Name,
                Description = book.Description,
                Price = book.Price,
                ImageURL = book.ImageURL,
                PublishingYear = book.PublishingYear,
                CategoryId = book.CategoryId,
                AuthorId = book.AuthorId,
                PublisherId = book.PublisherId
            };

            ViewBag.AuthorId = new SelectList(_context.Authors.OrderBy(n => n.FullName).ToList(), "Id", "FullName", book.AuthorId);
            ViewBag.PublisherId = new SelectList(_context.Publishers.OrderBy(n => n.FullName).ToList(), "Id", "FullName", book.PublisherId);
            ViewBag.CategoryId = new SelectList(_context.Categories.OrderBy(n => n.Name).ToList(), "Id", "Name", book.CategoryId);
            return View(bookVM);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookEditViewModel bookVM)
        {
            if (id != bookVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var book = await _context.Books.FindAsync(id);
                    if (book == null)
                    {
                        return NotFound();
                    }

                    if (bookVM.PictureFile != null)
                    {
                         string fileName = await _fileUploadService.UploadFileAsync(bookVM.PictureFile, AppConstants.BookImagePath);
                         book.ImageURL = fileName;
                    }

                    book.Name = bookVM.Name;
                    book.Description = bookVM.Description;
                    book.Price = bookVM.Price;
                    book.PublishingYear = bookVM.PublishingYear;
                    book.CategoryId = bookVM.CategoryId;
                    book.AuthorId = bookVM.AuthorId;
                    book.PublisherId = bookVM.PublisherId;

                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(bookVM.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.AuthorId = new SelectList(_context.Authors.OrderBy(n => n.FullName).ToList(), "Id", "FullName", bookVM.AuthorId);
            ViewBag.PublisherId = new SelectList(_context.Publishers.OrderBy(n => n.FullName).ToList(), "Id", "FullName", bookVM.PublisherId);
            ViewBag.CategoryId = new SelectList(_context.Categories.OrderBy(n => n.Name).ToList(), "Id", "Name", bookVM.CategoryId);
            return View(bookVM);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Books == null)
            {
                return NotFound();
            }

            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Books == null)
            {
                return Problem("Entity set 'AppDbContext.Books'  is null.");
            }
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
          return _context.Books.Any(e => e.Id == id);
        }
    }
}
