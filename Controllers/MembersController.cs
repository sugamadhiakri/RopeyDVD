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
    public class MembersController : Controller
    {
        private readonly RopeyDVDContext _context;

        public MembersController(RopeyDVDContext context)
        {
            _context = context;
        }

        // Question 8
        // GET: Members
        [Authorize(Policy = "AssistantOrAdmin")]
        public async Task<IActionResult> Index()
        {
            var loans = await _context.Loan.ToListAsync();
            ViewData["loans"] = loans;
            return View(await _context.Member.Include("MembershipCategory").ToListAsync());
        }

        // GET: Members/Details/5
        [Authorize(Policy = "AssistantOrAdmin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Member
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }


    }
}
