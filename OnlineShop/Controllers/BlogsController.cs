using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Models;
using OnlineShop.ViewModel;

namespace OnlineShop.Controllers
{
    public class BlogsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BlogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Blogs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Blogs.Include(b => b.Category).Include(b => b.Thumbnail);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> IndexCommonBlogs()
        {
            ViewData["ListTags"] = await _context.Tags.ToListAsync();
            var blogsContext = _context.Blogs.Include(b => b.Category).Include(b => b.Thumbnail);
            return View("IndexCommonBlogs", await blogsContext.ToListAsync());
        }

        public async Task<IActionResult> BlogPageView(int id)
        {

            ViewData["BlogTags"] = await _context.TagBlogs.Where(t => t.BlogId == id).ToListAsync();
            var blogContext = _context.Blogs.Include(b => b.Category).Include(b => b.Thumbnail).FirstOrDefault(blog => blog.Id == id);
            return View("BlogPageView", blogContext);
        }

        // GET: Blogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Blogs == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .Include(b => b.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // GET: Blogs/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.BlogCategories, "Id", "Name");
            ViewData["ThumbnailId"] = new SelectList(_context.Thumbnails, "Id", "ThumbnailName");
            ViewData["ListTag"] = new SelectList(_context.Tags, "Id", "Name");
            return View();
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogVM blogVM, List<int> SelectedTags)
        {
            if (ModelState.IsValid)
            {
                var blog = new Blog()
                {
                    Title = blogVM.Title,
                    MetaTitle = blogVM.MetaTitle,
                    MetaDescription = blogVM.MetaDescription,
                    Slug = blogVM.Slug,
                    Summary = blogVM.Summary,
                    Content = blogVM.Content,
                    PublishedDate = blogVM.PublishedDate,
                    CategoryId = blogVM.CategoryId,
                    ThumbnailId = blogVM.ThumbnailId,
                };

                _context.Add(blog);
                await _context.SaveChangesAsync();
                var currentblogCreate = _context.Blogs.FirstOrDefault(b => b.Title == blog.Title && b.Content == blog.Content);
                if (SelectedTags != null || SelectedTags.Count > 0)
                {
                    foreach (var tag in SelectedTags)
                    {
                        _context.Add(new TagBlog()
                        {
                            BlogId = currentblogCreate.Id,
                            TagId = tag

                        });
                    }
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));

            }
            ViewData["CategoryId"] = new SelectList(_context.BlogCategories, "Id", "Name", blogVM.CategoryId);
            ViewData["ThumbnailId"] = new SelectList(_context.Thumbnails, "Id", "ThumbnailName", blogVM.ThumbnailId);
            return View(blogVM);
        }

        // GET: Blogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Blogs == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            var blogVM = new BlogVM()
            {
                Id = blog.Id,
                ThumbnailId = blog.ThumbnailId,
                CategoryId = blog.CategoryId,
                Title = blog.Title,
                MetaTitle = blog.MetaTitle,
                MetaDescription = blog.MetaDescription,
                Slug = blog.Slug,
                Summary = blog.Summary,
                Content = blog.Content,
                PublishedDate = blog.PublishedDate,
                AuthorId = blog.AuthorId,
                IsPublished = blog.IsPublished,

            };
            ViewData["CategoryId"] = new SelectList(_context.BlogCategories, "Id", "Name", blog.CategoryId);
            ViewData["ThumbnailId"] = new SelectList(_context.Thumbnails, "Id", "ThumbnailName", blog.ThumbnailId);
            var tagOfBlog = await _context.TagBlogs
                .Include(t => t.Tag)
                .Where(t => t.BlogId == id)
                .Select(t => t.Tag).ToListAsync();
            ViewData["tagOfBlog"] = new SelectList(tagOfBlog, "Id", "Name");
            ViewData["ListTag"] = new SelectList(_context.Tags,"Id","Name");
            return View(blogVM);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BlogVM blogVM, List<int> SelectedTags)
        {
            if (ModelState.IsValid)
            {
                var blogUpdate = await _context.Blogs.FirstAsync(blog => blog.Id == blogVM.Id);
                if (blogUpdate == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                blogUpdate.Title = blogVM.Title;
                blogUpdate.MetaTitle = blogVM.MetaTitle;
                blogUpdate.MetaDescription = blogVM.MetaDescription;
                blogUpdate.IsPublished = blogVM.IsPublished;
                blogUpdate.PublishedDate = blogVM.PublishedDate;
                blogUpdate.Slug = blogVM.Slug;
                blogUpdate.Summary = blogVM.Summary;
                blogUpdate.Content = blogVM.Content;
                blogUpdate.ThumbnailId = blogVM.ThumbnailId;
                blogUpdate.CategoryId = blogVM.CategoryId;
                blogUpdate.AuthorId = blogVM.AuthorId;
                try
                {
                    _context.Update(blogUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException exception)
                {
                    throw new DbUpdateConcurrencyException(exception.Message);
                }
                return RedirectToAction(nameof(Index));
            }


            ViewData["CategoryId"] = new SelectList(_context.BlogCategories, "Id", "Name", blogVM.CategoryId);
            ViewData["ThumbnailId"] = new SelectList(_context.Thumbnails, "Id", "ThumbnailName", blogVM.ThumbnailId);

            return View(blogVM);
        }


        // GET: Blogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Blogs == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .Include(b => b.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Blogs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Blogs'  is null.");
            }
            var blog = await _context.Blogs.FindAsync(id);
            if (blog != null)
            {
                _context.Blogs.Remove(blog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogExists(int id)
        {
            return (_context.Blogs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
