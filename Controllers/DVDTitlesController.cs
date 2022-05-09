#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RopeyDVD.Data;
using RopeyDVD.Models;

namespace RopeyDVD.Controllers
{
    public class DVDTitlesController : Controller
    {
        private readonly RopeyDVDContext _context;

        public DVDTitlesController(RopeyDVDContext context)
        {
            _context = context;
        }

        // GET: DVDTitles

        public IActionResult Index()
        {
            return View();
        }
        // Question 1
        public async Task<IActionResult> Search(SearchString searchTerm)
        {
            // get every dvds
            var dvds = await GetDVDByActorsLastName(searchTerm.SearchValue);

            // return all the saved dvds
            return View("SearchDVDList", dvds);
        }


        // Question 2
        public async Task<IActionResult> SearchOnShelve(SearchString searchTerm)
        {

            var dvds = await GetDVDByActorsLastName(searchTerm.SearchValue);

            var ourDvd = new Dictionary<DVDTitle, int>();

            foreach (var dvd in dvds)
            {
                int c = GetNumberOfCopiesOnShelves(dvd);
                if (c > 0)
                {
                    ourDvd.Add(dvd, c);
                    continue;
                }
            }


            return View("SearchDVDonShelve", ourDvd);
        }

        // Question 3
        [Authorize(Policy = "AssistantOrAdmin")]
        public async Task<IActionResult> SearchLoanedDVD(SearchString searchTerm)
        {
            var loanedDvds = await LoanedDVD(searchTerm.SearchValue);

            ViewData["loanedDvds"] = loanedDvds;

            return View();
        }

        // Question 4
        [Authorize(Policy = "AssistantOrAdmin")]
        public async Task<IActionResult> AllDVDs()
        {
            var allDvds = await GetAllDVDSortedByReleaseDate();
            return View(allDvds);
        }
        public async Task<Dictionary<DVDCopy, DateTime>> LoanedDVD(string memberName)
        {
            var lastMonth = DateTime.Today.AddDays(-31);

            var loans = await _context.Loan.Include("Member").Include("Copy").Where(l => l.Member.LastName.Contains(memberName) && l.DateOut >= lastMonth).ToListAsync();

            var dvdCopies = new Dictionary<DVDCopy, DateTime>();

            foreach (var l in loans)
            {
                var copy = _context.DVDCopy.Include("DVDTitle").Where(c => l.Copy.CopyId == c.CopyId).First();
                dvdCopies.Add(copy, l.DateOut);
            }

            return dvdCopies;
        }


        private async Task<IEnumerable<DVDTitle>> GetDVDByActorsLastName(string searchTerm)
        {
            // get every dvds
            var dvds = await _context.DVDTitle.Include("Actors").Where(d => d.Actors.Where(a => a.Lastname.Contains(searchTerm)).Count() > 0).ToListAsync();

            return dvds;
        }

        private int GetNumberOfCopiesOnShelves(DVDTitle dvd)
        {
            var copies = _context.DVDCopy.Where(c => c.DVDTitle.DVDId == dvd.DVDId && !c.IsLoaned).Count();

            return copies;
        }



        private async Task<IEnumerable<DVDTitle>> GetAllDVDSortedByReleaseDate()
        {
            var dvds = await _context.DVDTitle.Include("Actors").Include("Producer").Include("Studio").Include("Category").ToListAsync();

            dvds.Sort(delegate (DVDTitle c1, DVDTitle c2) { return c1.DateReleased.CompareTo(c2.DateReleased); });

            return dvds;
        }
    }
}
