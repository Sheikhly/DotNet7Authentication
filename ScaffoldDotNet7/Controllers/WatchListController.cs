using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScaffoldDotNet7.Constants;
using ScaffoldDotNet7.Data;
using ScaffoldDotNet7.Models;

namespace ScaffoldDotNet7.Controllers
{
    public class WatchListController : Controller
    {
        private readonly ApplicationDbContext movieDbContext;

        public WatchListController(ApplicationDbContext movieDbContext)
        {
            this.movieDbContext = movieDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var movies = await movieDbContext.Movies.ToListAsync();
            return View(movies);
        }

        [Authorize(Roles = "Admin")]

        [HttpGet]
        public IActionResult Add()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Add(AddMovieViewModel addMovieViewModel)
        {
            var movie = new Movie()
            {
                Id = Guid.NewGuid(),
                Name = addMovieViewModel.Name,
                Year = addMovieViewModel.Year,
                Genre = addMovieViewModel.Genre
            };

            await movieDbContext.Movies.AddAsync(movie);
            await movieDbContext.SaveChangesAsync();
            return RedirectToAction("Add");
        }


        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            // id = What User Want to Update
            //Id = What We Send To DB
            var movie = await movieDbContext.Movies.FirstOrDefaultAsync(x => x.Id == id);

            if (movie != null)

            {
                //We Want to Use From UpdateModel a TypeConversion needed.
                var viewModel = new UpdateMovieViewModel()
                {
                    Id = movie.Id,
                    Name = movie.Name,
                    Year = movie.Year,
                    Genre = movie.Genre,
                };

                return await Task.Run(() => View("View", viewModel));
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> View(UpdateMovieViewModel model)
        {
            // Get The Specific Movie From The DataBase
            var movie = await movieDbContext.Movies.FindAsync(model.Id);

            if (movie != null)
            {
                movie.Name = model.Name;
                movie.Year = model.Year;
                movie.Genre = model.Genre;

                await movieDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateMovieViewModel model)
        {
            var movie = await movieDbContext.Movies.FindAsync(model.Id);

            if (movie != null)
            {
                movieDbContext.Movies.Remove(movie);
                await movieDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
