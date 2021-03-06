﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using tiendaOnline.Areas.Identity.Data;
using tiendaOnline.Data;
using tiendaOnline.Models;

namespace tiendaOnline.Controllers
{
    public class DireccionesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public readonly UserManager<tiendaOnlineUser> _userManager;

        public DireccionesController(ApplicationDbContext context, UserManager<tiendaOnlineUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Direcciones
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Index()
        {
            //Filtro de direcciones, filtro por id de usuario
            var direcciones = from dir in (_context.Direccion.Include(d => d.Municipio).Include(d => d.detalleVendedor).Include(d => d.tiendaOnlineUser)) select dir;
            var user = await _userManager.GetUserAsync(User);
            direcciones = direcciones.Where(d => d.tiendaOnlineUserID.Contains(user.Id) && d.detalleVendedor== null);

            return View(await direcciones.AsNoTracking().ToListAsync());
            //return View(await applicationDbContext.ToListAsync());
        }

        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> IndexVendedor()
        {            
            //Filtro de direcciones, filtro por id de usuario
            var direcciones = from dir in (_context.Direccion.Include(d => d.Municipio).Include(d => d.detalleVendedor).Include(d => d.tiendaOnlineUser)) select dir;
            var user = await _userManager.GetUserAsync(User);
            var vendedor = _context.DetalleVendedor.Single(vndr=> vndr.tiendaOnlineUserID== user.Id);
            direcciones = direcciones.Where(d => d.tiendaOnlineUserID.Contains(user.Id) && d.detalleVendedorID==vendedor.DetalleVendedorID);

            return View(await direcciones.AsNoTracking().ToListAsync());
            //return View(await applicationDbContext.ToListAsync());
        }

        // GET: Direcciones/Details/5
        [Authorize(Roles = "User, Seller")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direccion = await _context.Direccion
                .Include(d => d.Municipio)
                .Include(d => d.detalleVendedor)
                .Include(d => d.tiendaOnlineUser)
                .FirstOrDefaultAsync(m => m.DireccionID == id);
            if (direccion == null)
            {
                return NotFound();
            }

            return View(direccion);
        }

        //Metodo Json para llenar el select de municipios en la vista
       public async Task<JsonResult> SubCate(int idcat)
        {
            List<Municipio> subcats =await this._context.Municipio.Where(c => c.DepartamentoID == idcat).ToListAsync();
        //    subcats.Insert(0, new Municipio { MunicipioID = 0, nombreMunicipio = "Seleccione un municipio" });
 
            return new JsonResult(subcats);
        }

        // GET: Direcciones/Create
        [Authorize(Roles = "User")]
        public IActionResult Create()
        {
           
          ViewData["DepartamentoID"] = new SelectList(_context.Departamento, "DepartamentoID", "nombreDepartamento");
             
            return View();
        }

        // POST: Direcciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create([Bind("DireccionID,direccionDetallada,codigoPostal,MunicipioID,tiendaOnlineUserID,detalleVendedorID")] Direccion direccion)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);               
                direccion.tiendaOnlineUserID = user.Id;                
                _context.Add(direccion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartamentoID"] = new SelectList(_context.Departamento, "DepartamentoID", "nombreDepartamento");           
            
            return View(direccion);
        }

        //Direccion para Vendedor
        [Authorize(Roles = "Seller")]
        public IActionResult CreateParaVendedor()
        {
            ViewData["DepartamentoID"] = new SelectList(_context.Departamento, "DepartamentoID", "nombreDepartamento");

            ViewData["MunicipioID"] = new SelectList(_context.Municipio, "MunicipioID", "nombreMunicipio");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> CreateParaVendedor([Bind("DireccionID,direccionDetallada,codigoPostal,MunicipioID,tiendaOnlineUserID,detalleVendedorID")] Direccion direccion)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var vendedor = _context.DetalleVendedor.Single(d => d.tiendaOnlineUserID == user.Id);
                direccion.tiendaOnlineUserID = user.Id;
                direccion.detalleVendedorID = vendedor.DetalleVendedorID;
                _context.Add(direccion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(IndexVendedor));
            }
            ViewData["MunicipioID"] = new SelectList(_context.Municipio, "MunicipioID", "nombreMunicipio", direccion.MunicipioID);
            
            return View(direccion);
        }
        // GET: Direcciones/Edit/5
        [Authorize(Roles = "User, Seller")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direccion = await _context.Direccion.FindAsync(id);
            if (direccion == null)
            {
                return NotFound();
            }
            ViewData["MunicipioID"] = new SelectList(_context.Municipio, "MunicipioID", "nombreMunicipio", direccion.MunicipioID);            
            return View(direccion);
        }

        // POST: Direcciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User, Seller")]
        public async Task<IActionResult> Edit(int id, [Bind("DireccionID,direccionDetallada,codigoPostal,MunicipioID,tiendaOnlineUserID,detalleVendedorID")] Direccion direccion)
        {
            if (id != direccion.DireccionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(direccion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DireccionExists(direccion.DireccionID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                if (direccion.detalleVendedorID == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                else return RedirectToAction(nameof(IndexVendedor));
            }
            ViewData["MunicipioID"] = new SelectList(_context.Municipio, "MunicipioID", "nombreMunicipio", direccion.MunicipioID);            
            return View(direccion);
        }

        // GET: Direcciones/Delete/5
        [Authorize(Roles = "User, Seller")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var direccion = await _context.Direccion
                .Include(d => d.Municipio)
                .Include(d => d.detalleVendedor)
                .Include(d => d.tiendaOnlineUser)
                .FirstOrDefaultAsync(m => m.DireccionID == id);
            if (direccion == null)
            {
                return NotFound();
            }

            return View(direccion);
        }

        // POST: Direcciones/Delete/5
        [Authorize(Roles = "User, Seller")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var direccion = await _context.Direccion.FindAsync(id);
            _context.Direccion.Remove(direccion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DireccionExists(int id)
        {
            return _context.Direccion.Any(e => e.DireccionID == id);
        }
    }
}
