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
using tiendaOnline.Data.Interfaces;
using tiendaOnline.Models;
using tiendaOnline.ViewModels;

namespace tiendaOnline.Controllers
{
    public class OrdenesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IOrden _orden;
        private readonly Carrito _carrito;
        private readonly UserManager<tiendaOnlineUser> _userManager;

        public OrdenesController(ApplicationDbContext context, IOrden orden, Carrito carrito, UserManager<tiendaOnlineUser> userManager)
        {
            _context = context;
            _orden = orden;
            _carrito = carrito;
            _userManager = userManager;

        }

        // GET: Ordenes
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Index()
        {
            string userId = _userManager.GetUserId(User);
            var applicationDbContext = _context.Orden.Include(o => o.cupon).Include(o => o.metodoEnvio).Include(o => o.tiendaOnlineUser).Where(o=> o.tiendaOnlineUserID==userId);
            return View("Index", await applicationDbContext.ToListAsync());
        }
        // GET: Ordenes
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> IndexAdministrador()
        {
            var applicationDbContext = _context.Orden.Include(o => o.cupon).Include(o => o.metodoEnvio).Include(o => o.tiendaOnlineUser);
            return View("IndexAdministrador", await applicationDbContext.ToListAsync());
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> OrdenesTrue()
        {
            var applicationDbContext = _context.Orden.Include(o => o.cupon).Include(o => o.metodoEnvio).Include(o => o.tiendaOnlineUser).Where(o=> o.estadoDeOrden==true);
            return View("IndexAdministrador", await applicationDbContext.ToListAsync());
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> OrdenesFalse()
        {
            var applicationDbContext = _context.Orden.Include(o => o.cupon).Include(o => o.metodoEnvio).Include(o => o.tiendaOnlineUser).Where(o => o.estadoDeOrden == false);
            return View("IndexAdministrador", await applicationDbContext.ToListAsync());
        }

        // GET: Ordenes/Details/5
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            /*var orden = await _context.Orden
                .Include(o => o.cupon)
                .Include(o => o.metodoEnvio)
                .Include(o => o.tiendaOnlineUser)
                .Include(o=> o.direccion)
                .Include(o=>o.tarjeta)
                .FirstOrDefaultAsync(m => m.OrdenID == id);             

            return View(orden);*/
            var lineas = from l in _context.LineaDeOrden.Include(l => l.orden).Include(l=>l.Producto).Include(l=> l.Producto.detalleVendedor).Where(l => l.OrdenID == id) select l;

            return View(await lineas.AsNoTracking().ToListAsync());
            
        }

        [Authorize(Roles ="Seller")]
        public async Task<IActionResult> DetailsVendedor()
        {
            string userId = _userManager.GetUserId(User);
            var vendedor = _context.DetalleVendedor.Single(v => v.tiendaOnlineUserID == userId);
            var lineasOrden = _context.LineaDeOrden.Include(l => l.orden).Include(l=>l.orden.tiendaOnlineUser).Include(l => l.Producto).Include(l => l.Producto.detalleVendedor).Where(l =>l.Producto.detalleVendedorID== vendedor.DetalleVendedorID);
            return View(await lineasOrden.ToListAsync());
        }

        // GET: Ordenes/Create
        [Authorize(Roles = "User, admin")]
        public IActionResult Create()
        {
            ViewData["direccionID"] = new SelectList(_context.Direccion, "DireccionID", "DireccionID");
            ViewData["tarjetaID"] = new SelectList(_context.Tarjeta, "TarjetaID", "TarjetaID");
            ViewData["paypalID"] = new SelectList(_context.Paypal, "paypalID", "paypalID");
            ViewData["SucursalID"] = new SelectList(_context.Sucursal, "SucursalID", "SucursalID");
            ViewData["cuponID"] = new SelectList(_context.Cupon, "CuponID", "CuponID");
            ViewData["metodoEnvioID"] = new SelectList(_context.MetodoEnvio, "MetodoEnvioID", "nombreMetodoEnvio");
            ViewData["tiendaOnlineUserID"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }
        public IActionResult Thanks()
        {
            return View();
        }
        // POST: Ordenes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> Create([Bind("OrdenID,fechaOrden,total,estadoDeOrden,tiendaOnlineUserID,metodoEnvioID,cuponID,tarjetaID, direccionID, sucursalID")] Orden orden)
        {
            var items = _carrito.GetprodCarrito();
            _carrito.prodCarrito = items;

            // validar existencia

            var userId = _userManager.GetUserId(User);
            var carritoid = _context.Carrito.SingleOrDefault(c => c.tiendaOnlineUserID == userId);
            var prodCarrito = _context.ProdCarrito
            .Where(carrito => carrito.CarritoID== carritoid.CarritoID && carrito.IsSelected == true);
            var existenciaValida = true;

            //aqui revisa si hay stock disponible
            foreach (var producto in prodCarrito)
            {
                //el stock disponible
                var existencia = _context.Producto.SingleOrDefault(c => c.ProductoID == producto.productoID).Existencia;
                //si cantidad seleccionada <= stock disponible
                if (producto.cantidadProducto > existencia)
                {
                    existenciaValida = false;
                    return RedirectToAction("Index", "Home");

                }
               

            }

            if ((orden.sucursalID == null && (orden.direccionID == 0 || orden.metodoEnvioID == 0)) )
            {

                ViewData["direccionID"] = new SelectList(_context.Direccion, "DireccionID", "DireccionID", orden.direccionID);
                ViewData["tarjetaID"] = new SelectList(_context.Tarjeta, "TarjetaID", "TarjetaID", orden.tarjetaID);
                ViewData["cuponID"] = new SelectList(_context.Cupon, "CuponID", "CuponID", orden.cuponID);
                ViewData["metodoEnvioID"] = new SelectList(_context.MetodoEnvio, "MetodoEnvioID", "nombreMetodoEnvio", orden.metodoEnvioID);
                ViewData["tiendaOnlineUserID"] = new SelectList(_context.Users, "Id", "Id", orden.tiendaOnlineUserID);
                
                return View(orden);
            }
            if (ModelState.IsValid )
            {
                // _context.Add(orden);
               if (orden.direccionID==0)
                {
                    orden.direccionID = null;
                }
               if(orden.metodoEnvioID==0)
                {
                    orden.metodoEnvioID = null;
                }
               if ( orden.cuponID==0)
                {
                    orden.cuponID = null;
                }
               if (orden.tarjetaID==0)
                {
                    orden.tarjetaID = null;
                }
                var user = await _userManager.GetUserAsync(User);
                orden.tiendaOnlineUserID = user.Id;
                _orden.CrearOrden(orden);
                _carrito.GetprodCarrito();
                _carrito.VaciarCarrito();
                await _context.SaveChangesAsync();
                return Redirect("Thanks");

            }
            ViewData["direccionID"] = new SelectList(_context.Direccion, "DireccionID", "DireccionID", orden.direccionID);
            ViewData["tarjetaID"] = new SelectList(_context.Tarjeta, "TarjetaID", "TarjetaID", orden.tarjetaID);
            ViewData["cuponID"] = new SelectList(_context.Cupon, "CuponID", "CuponID", orden.cuponID);
            ViewData["metodoEnvioID"] = new SelectList(_context.MetodoEnvio, "MetodoEnvioID", "nombreMetodoEnvio", orden.metodoEnvioID);
            ViewData["tiendaOnlineUserID"] = new SelectList(_context.Users, "Id", "Id", orden.tiendaOnlineUserID);
           
            return View(orden);
        }



        // GET: Ordenes/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orden = await _context.Orden.FindAsync(id);
            if (orden == null)
            {
                return NotFound();
            }
            ViewData["cuponID"] = new SelectList(_context.Cupon, "CuponID", "CuponID", orden.cuponID);
            ViewData["metodoEnvioID"] = new SelectList(_context.MetodoEnvio, "MetodoEnvioID", "nombreMetodoEnvio", orden.metodoEnvioID);
            ViewData["tiendaOnlineUserID"] = new SelectList(_context.Users, "Id", "Id", orden.tiendaOnlineUserID);
            return View(orden);
        }

        // POST: Ordenes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("OrdenID,fechaOrden,total,estadoDeOrden,tiendaOnlineUserID,metodoEnvioID,cuponID,tarjetaID,direccionID")] Orden orden)
        {
            if (id != orden.OrdenID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orden);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdenExists(orden.OrdenID))
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
            ViewData["cuponID"] = new SelectList(_context.Cupon, "CuponID", "CuponID", orden.cuponID);
            ViewData["metodoEnvioID"] = new SelectList(_context.MetodoEnvio, "MetodoEnvioID", "nombreMetodoEnvio", orden.metodoEnvioID);
            ViewData["tiendaOnlineUserID"] = new SelectList(_context.Users, "Id", "Id", orden.tiendaOnlineUserID);
            return View(orden);
        }

        // GET: Ordenes/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orden = await _context.Orden
                .Include(o => o.cupon)
                .Include(o => o.metodoEnvio)
                .Include(o => o.tiendaOnlineUser)
                .FirstOrDefaultAsync(m => m.OrdenID == id);
            if (orden == null)
            {
                return NotFound();
            }

            return View(orden);
        }

      

        // POST: Ordenes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orden = await _context.Orden.FindAsync(id);
            _context.Orden.Remove(orden);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //Para Marcar la orden como finalizada
        public async Task<RedirectToActionResult> FinalizarOrden(int id)
        {
            var ordenSeleccionada = _context.Orden.FirstOrDefault(d => d.OrdenID == id);

            ordenSeleccionada.estadoDeOrden = false;
            _context.SaveChanges();
            return RedirectToAction("OrdenesTrue");

        }

        private bool OrdenExists(int id)
        {
            return _context.Orden.Any(e => e.OrdenID == id);
        }
    }
}
