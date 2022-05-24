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
    public class LoansController : Controller
    {
        private readonly RopeyDVDContext _context;
        private readonly ILogger<LoansController> _logger;

        public LoansController(RopeyDVDContext context, ILogger<LoansController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Loans
        [Authorize(Policy = "AssistantOrAdmin")]
        public async Task<IActionResult> Index()
        {

            return View(await _context.Loan.Where(loan => loan.DateReturned == null).Include("LoanType").Include("Member").Include(loan => loan.Copy).ThenInclude(copy => copy.DVDTitle).ToListAsync());
        }

        [Authorize(Policy = "AssistantOrAdmin")]
        public async Task<IActionResult> LoanByCopyId(SearchInt searchInt)
        {
            if (ModelState.IsValid && searchInt.SearchValue != 0)
            {
                var loan = await _context.Loan.Include("Member").Include(loan => loan.Copy).ThenInclude(copy => copy.DVDTitle).Where(loan => loan.Copy.CopyId == searchInt.SearchValue).OrderByDescending(loan => loan.DateOut).FirstOrDefaultAsync();

                if (loan == null)
                {
                    ModelState.AddModelError("CustomError", "Either That DVD Copy doesn't exist or it has not been loaned yet!");
                }

                ViewData["Loan"] = loan;
            }
            return View();
        }

       public async Task<IActionResult> Return(int? id)
        {
            if (id == null)
                return NotFound();

            var loan = await _context.Loan.Where(loan => loan.LoanId == id).Include("Copy").FirstOrDefaultAsync();

            if(loan.DateReturned != null)
            {
                return RedirectToAction("Index");
            }

            loan.Copy.IsLoaned = false;
            loan.DateReturned = DateTime.Now;

            _context.Update(loan);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        // Question 6
        // GET: Loans/Create
        [Authorize(Policy = "AssistantOrAdmin")]
        public async Task<IActionResult> Create()
        {
            // Available DVDs to borrow
            var loanTypes = await _context.LoanType.ToListAsync();


            return View();
        }

        // POST: Loans/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "AssistantOrAdmin")]
        public async Task<IActionResult> Create(LoanInputModel loanInput)
        {
            if (ModelState.IsValid)
            {
                // Check if the member is valid

                var member = await _context.Member.Include("MembershipCategory").FirstOrDefaultAsync(m => m.MemberId == loanInput.MemberId);
                if (member == null)
                {
                    ModelState.AddModelError("CustomError", "Member with that member Id doesn't exist");
                    return View(loanInput);
                }

                // Check if the member can loan or no
                var loansByMember = _context.Loan.Include("Member").Where(loan => loan.Member.MemberId == member.MemberId).Count();
                if (loansByMember >= member.MembershipCategory.MembershipCategoryTotalLoans)
                {
                    ModelState.AddModelError("CustomError", "This member cannot loan DVDs. Already has maximum limit.");
                    return View(loanInput);
                }
                // Check if the copy is available
                var dvdCopy = await _context.DVDCopy.Include(copy => copy.DVDTitle).ThenInclude(title => title.Category).FirstAsync();
                if (dvdCopy == null)
                {
                    ModelState.AddModelError("CustomError", "DVD Copy with this ID doesn't exist.");
                    return View(loanInput);
                }

                // check if the dvd is age restricted
                var memberAge = (DateTime.Today - member.Dob).TotalDays / 365.25;

                if (dvdCopy.DVDTitle.Category.AgeRestricted && memberAge < 18.0)
                {
                    ModelState.AddModelError("CustomError", "This member cannot loan Age Restricted DVD");
                }

                // No errors were found. Create the loan record

                Loan newLoan = new Loan();
                var loanId = _context.Loan.OrderByDescending(loan => loan.LoanId).FirstOrDefault().LoanId + 1;
                newLoan.LoanId = loanId;
                newLoan.DateOut = DateTime.Today;

                var loanType = _context.LoanType.Where(t => t.Type == loanInput.loanType).First();
                newLoan.LoanType = loanType;

                newLoan.DateDue = DateTime.Today.AddDays(loanType.Duration);
                newLoan.Member = member;
                newLoan.Copy = dvdCopy;

                try
                {
                    await _context.Loan.AddAsync(newLoan);
                    await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Loan ON;");
                    await _context.SaveChangesAsync();
                    await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Loan OFF;");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("CustomError", ex.Message);
                    return (View(loanInput));
                }

                // Redirect To the details page
                return RedirectToAction("Detail", new { id = loanId });
            }
            return View(loanInput);
        }

       
        

    }
}
