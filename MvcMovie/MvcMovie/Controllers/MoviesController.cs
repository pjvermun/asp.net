using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models.ViewModels;

namespace MvcMovie.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MovieContext _context;

        public MoviesController(MovieContext context)
        {
            _context = context;
        }

        public IActionResult List(int? ratingID)
        {
            var movies = from m in _context.Movies.OrderBy(m => m.Title) select m;

            if (ratingID != null && ratingID != 0)
            {
                movies = movies.Where(m => m.RatingID == ratingID);
            }

            var listMoviesVM = new ListMoviesViewModel();
            listMoviesVM.Movies = movies.ToList();
            listMoviesVM.Ratings =
                new SelectList(_context.Ratings.OrderBy(r => r.Name),
                "RatingID", "Name");
            listMoviesVM.ratingID = (ratingID == null) ? 0 : (int)ratingID;

            return View(listMoviesVM);
        }

        public IActionResult Details(int id)
        {
            var movie = _context.Movies
                            .Include(m => m.Rating)
                            .SingleOrDefault(m => m.MovieID == id);
            return View(movie);
        }
        
        public IActionResult Create()
        {
            ViewData["Ratings"] =
                new SelectList(_context.Ratings.OrderBy(r => r.Name),
                "RatingID", "Name");

            return View();
        }
    }
}