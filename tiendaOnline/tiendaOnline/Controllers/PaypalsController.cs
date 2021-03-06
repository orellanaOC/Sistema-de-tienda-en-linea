﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using tiendaOnline.Data;
using tiendaOnline.Models;

namespace tiendaOnline.Controllers
{
    public class PaypalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaypalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Paypals
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Paypal.Include(p => p.tiendaOnlineUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Paypals/Details/5
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paypal = await _context.Paypal
                .Include(p => p.tiendaOnlineUser)
                .FirstOrDefaultAsync(m => m.PaypalID == id);
            if (paypal == null)
            {
                return NotFound();
            }

            return View(paypal);
        }

        // GET: Paypals/Create
        [Authorize(Roles = "User")]
        public IActionResult Create()
        {
            ViewData["tiendaOnlineUserID"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Paypals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create([Bind("PaypalID,correoPaypal,psswrdPaypal,tiendaOnlineUserID")] Paypal paypal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paypal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["tiendaOnlineUserID"] = new SelectList(_context.Users, "Id", "Id", paypal.tiendaOnlineUserID);
            return View(paypal);
        }

        // GET: Paypals/Edit/5
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paypal = await _context.Paypal.FindAsync(id);
            if (paypal == null)
            {
                return NotFound();
            }
            
            return View(paypal);
        }

        // POST: Paypals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Edit(int id, [Bind("PaypalID,correoPaypal,psswrdPaypal,tiendaOnlineUserID")] Paypal paypal)
        {
            if (id != paypal.PaypalID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paypal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaypalExists(paypal.PaypalID))
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
            ViewData["tiendaOnlineUserID"] = new SelectList(_context.Users, "Id", "Id", paypal.tiendaOnlineUserID);
            return View(paypal);
        }

        // GET: Paypals/Delete/5
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paypal = await _context.Paypal
                .Include(p => p.tiendaOnlineUser)
                .FirstOrDefaultAsync(m => m.PaypalID == id);
            if (paypal == null)
            {
                return NotFound();
            }

            return View(paypal);
        }

        // POST: Paypals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var paypal = await _context.Paypal.FindAsync(id);
            _context.Paypal.Remove(paypal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaypalExists(int id)
        {
            return _context.Paypal.Any(e => e.PaypalID == id);
        }
    }
}
