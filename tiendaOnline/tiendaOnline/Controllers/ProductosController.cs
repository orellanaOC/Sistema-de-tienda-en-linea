﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using tiendaOnline.Areas.Identity.Data;
using tiendaOnline.Data;
using tiendaOnline.Models;

namespace tiendaOnline.Controllers
{
    public class ProductosController : Controller
    {
        private readonly UserManager<tiendaOnlineUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment he;


        public ProductosController(IHostingEnvironment e, ApplicationDbContext context, UserManager<tiendaOnlineUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            he = e;
        }

        // GET: Productos       
        public async Task<IActionResult> Index(string searchString, double precioMin, double precioMax, string vendedor, string marca)
        {
            var applicationDbContext = _context.Producto.Include(p => p.Subcategoria).Include(p => p.detalleVendedor);
            //Cuadro de busqueda

            ViewData["CurrentFilter"] = searchString;
            ViewData["PrecioMinFilter"] = precioMin;
            ViewData["PrecioMaxFilter"] = precioMax;
            ViewData["SellerFilter"] = vendedor;
            ViewData["BrandFilter"] = marca;
            var productos = from p in _context.Producto select p; //recorre todos los items en producto

            if (!String.IsNullOrEmpty(searchString))
            {
                //agregar metadata si se modifican los atributos
                productos = productos.Where(p => p.NombreProducto.Contains(searchString) ||
                p.Subcategoria.nombreSubcategoria.Contains(searchString) ||
                p.Subcategoria.Categoria.nombre_categoria.Contains(searchString) ||
                p.DetalleProducto.Marca.Contains(searchString)
                );

            }
            if (!String.IsNullOrEmpty(searchString)&& precioMax.Equals(0))
            {
                //agregar metadata si se modifican los atributos
                productos = productos.Where(p => p.NombreProducto.Contains(searchString) ||
                p.Subcategoria.nombreSubcategoria.Contains(searchString) ||
                p.Subcategoria.Categoria.nombre_categoria.Contains(searchString) ||
                p.DetalleProducto.Marca.Contains(searchString)
                );
                               
            }
            if (!String.IsNullOrEmpty(searchString) && precioMax.Equals(0) && !String.IsNullOrEmpty(vendedor))
            {
                //agregar metadata si se modifican los atributos
                productos = productos.Where(p => p.detalleVendedor.nombreComercial.Contains(vendedor) && 
                (p.NombreProducto.Contains(searchString) ||
                p.Subcategoria.nombreSubcategoria.Contains(searchString) ||
                p.Subcategoria.Categoria.nombre_categoria.Contains(searchString) ||
                p.DetalleProducto.Marca.Contains(searchString))
                );

            }
            if (!String.IsNullOrEmpty(marca) && !String.IsNullOrEmpty(searchString) && precioMax.Equals(0) && !String.IsNullOrEmpty(vendedor))
            {
                //agregar metadata si se modifican los atributos
                productos = productos.Where(p => p.DetalleProducto.Marca.Contains(marca) &&
                p.detalleVendedor.nombreComercial.Contains(vendedor) &&
                (p.NombreProducto.Contains(searchString) ||
                p.Subcategoria.nombreSubcategoria.Contains(searchString) ||
                p.Subcategoria.Categoria.nombre_categoria.Contains(searchString) ||
                p.DetalleProducto.Marca.Contains(searchString))
                );

            }
            if (!String.IsNullOrEmpty(marca) && !String.IsNullOrEmpty(searchString) && precioMax.Equals(0))
            {
                //agregar metadata si se modifican los atributos
                productos = productos.Where(p => p.DetalleProducto.Marca.Contains(marca) &&
                (p.NombreProducto.Contains(searchString) ||
                p.Subcategoria.nombreSubcategoria.Contains(searchString) ||
                p.Subcategoria.Categoria.nombre_categoria.Contains(searchString) ||
                p.DetalleProducto.Marca.Contains(searchString))
                );

            }

            //esperando en diosito que el filtrado sirva 
            if (!String.IsNullOrEmpty(precioMin.ToString()) && !precioMax.Equals(0) && !String.IsNullOrEmpty(searchString))
            {
                productos = productos.Where(p => (p.PrecioUnitario <= precioMax) && (p.PrecioUnitario >= precioMin) &&
                (
                p.NombreProducto.Contains(searchString) ||
                p.Subcategoria.nombreSubcategoria.Contains(searchString) ||
                p.Subcategoria.Categoria.nombre_categoria.Contains(searchString) ||
                p.DetalleProducto.Marca.Contains(searchString)
                )
                );
            }
            if (!String.IsNullOrEmpty(vendedor) && !String.IsNullOrEmpty(precioMin.ToString()) && !String.IsNullOrEmpty(precioMax.ToString()) && !String.IsNullOrEmpty(searchString))
            {
                productos = productos.Where(p => (p.PrecioUnitario <= precioMax) && (p.PrecioUnitario >= precioMin) &&
                p.detalleVendedor.nombreComercial.Contains(vendedor) && (
                p.NombreProducto.Contains(searchString) ||
                p.Subcategoria.nombreSubcategoria.Contains(searchString) ||
                p.Subcategoria.Categoria.nombre_categoria.Contains(searchString) ||
                p.DetalleProducto.Marca.Contains(searchString))
                );
            }
            if (!String.IsNullOrEmpty(marca) && !String.IsNullOrEmpty(vendedor) && !String.IsNullOrEmpty(precioMin.ToString()) && !String.IsNullOrEmpty(precioMax.ToString()) && !String.IsNullOrEmpty(searchString))
            {
                productos = productos.Where(p => (p.PrecioUnitario <= precioMax) && (p.PrecioUnitario >= precioMin) &&
                p.DetalleProducto.Marca.Contains(marca) &&
                p.detalleVendedor.nombreComercial.Contains(vendedor)&& (
                p.NombreProducto.Contains(searchString) ||
                p.Subcategoria.nombreSubcategoria.Contains(searchString) ||
                p.Subcategoria.Categoria.nombre_categoria.Contains(searchString) ||
                p.DetalleProducto.Marca.Contains(searchString))
                );
            }

            return View(await productos.AsNoTracking().ToListAsync());
            //return View(await applicationDbContext.ToListAsync());
        }

        //Visualizar productos propios de cada vendedor
        [Authorize(Roles ="Seller")]
        public async Task<IActionResult> IndexVendedor(string searchString)
        {
            var user = await _userManager.GetUserAsync(User);
            var vendedor = _context.DetalleVendedor.Single(d => d.tiendaOnlineUser == user);
            var applicationDbContext = _context.Producto.Include(p => p.Subcategoria).Include(p => p.detalleVendedor);
            //Cuadro de busqueda
            var productos = from p in _context.Producto.Where(p => p.detalleVendedorID == vendedor.DetalleVendedorID) select p; //recorre todos los items en producto

          
            return View(await productos.AsNoTracking().ToListAsync());
            //return View(await applicationDbContext.ToListAsync());
        }
        //Visualizar todos los productos para gestion del administrador
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> indexAdministrador(string code)
        {
            //var productos = _context.Producto;

            var productos = from p in _context.Producto.Include(p => p.Subcategoria).Include(p => p.detalleVendedor) select p;
            ViewData["CodeFilter"] = code;
            if (!String.IsNullOrEmpty(code))
            {
                productos = productos.Where(p => p.Codigo.Contains(code));

            }            
            return View("listarProductos", await productos.ToListAsync());
        }

        // GET: Productos/Details/5
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto
                .Include(p => p.Subcategoria)
                .Include(p => p.detalleVendedor)
                .FirstOrDefaultAsync(m => m.ProductoID == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        //Metodo Json para llenar el select de subcategorias en la vista
        public async Task<JsonResult> SubCate(int idcat)
        {
            List<Subcategoria> subcats = await this._context.Subcategoria.Where(c => c.CategoriaID == idcat).ToListAsync();
            ViewData["SUB"] = new SelectList(subcats, "SubcategoriaID", "nombreSubcategoria");
            return new JsonResult(subcats);
        }

        // GET: Productos/Create
        [Authorize(Roles = "Seller")]
        public IActionResult Create()
        {
            ViewData["CategoriaID"] = new SelectList(_context.Categoria, "CategoriaID", "nombre_categoria");

            return View();
        }
        //Guarda la imagen
        public IActionResult Agrega(IFormFile Imagen)
        {
            if (Imagen != null)
            {
                var fileName = Path.Combine(he.WebRootPath, "images/productos", Path.GetFileName(Imagen.FileName));

                Imagen.CopyTo(new FileStream(fileName, FileMode.Create));
                ViewData["FileLocation"] = "/images/productos/" + Path.GetFileName(Imagen.FileName);
            }
            return View();
        }
        // POST: Productos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> Create([Bind("ProductoID,NombreProducto,PrecioUnitario,Existencia,Codigo,Imagen,SubcategoriaID,detalleVendedorID, estadoProducto,existenciaSinActivacion")] Producto producto, IFormFile Imagen)
        {
            if (ModelState.IsValid)
            {

                Agrega(Imagen);
                producto.Imagen = Imagen.FileName;

                //generador random de codigo
                var chars = Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", 10);
                var randomStr = new string(chars.SelectMany(str => str)
                                                .OrderBy(c => Guid.NewGuid())
                                                .Take(10).ToArray());
                //setzy codiguito wapo
                producto.Codigo = randomStr;

                
                //Asignando el producto al vendedor que ha iniciado sesion
                var user = await _userManager.GetUserAsync(User);
                var vendedor = _context.DetalleVendedor.Single(d => d.tiendaOnlineUser == user);
                producto.detalleVendedorID = vendedor.DetalleVendedorID;
                producto.existenciaSinActivacion = producto.Existencia;
                producto.Existencia = 0;
                producto.estadoProducto = false;                    
                _context.Add(producto);
                //crea un descuento para cada producto
                var descuento = new Descuento()
                {
                    EstaActivo = false,
                    NombreDelDescuento = "",
                    MontoDeDescuento = 0,
                    TipoDeDescuento = true,
                    PrecioConDesc = 0,
                    ProductoID = producto.ProductoID
                };
                _context.Add(descuento);

                await _context.SaveChangesAsync();
                return RedirectToAction("Create", "DetalleProductos");

            }
            ViewData["CategoriaID"] = new SelectList(_context.Categoria, "CategoriaID", "nombre_categoria");
            
            return View(producto);
        }

        // GET: Productos/Edit/5
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["CategoriaID"] = new SelectList(_context.Categoria, "CategoriaID", "nombre_categoria");

            if (id == null)
            {
                return NotFound();
            }
         

            var producto = await _context.Producto.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            ViewData["SubcategoriaID"] = new SelectList(_context.Subcategoria, "SubcategoriaID", "nombreSubcategoria", producto.SubcategoriaID);
            
            return View(producto);
        }

        // POST: Productos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> Edit(int id, [Bind("ProductoID,NombreProducto,PrecioUnitario,Existencia,Codigo,Imagen,SubcategoriaID,detalleVendedorID,existenciaSinActivacion,estadoProducto")] Producto producto)
        {
            if (id != producto.ProductoID)
            {
                return NotFound();
            }            
            if (ModelState.IsValid && producto.Existencia<= producto.existenciaSinActivacion)
            {
                try
                {
                    
                    if (producto.estadoProducto==true)
                    {
                        if(producto.Existencia!= producto.existenciaSinActivacion)
                        {
                            producto.estadoProducto = false;
                        }
                    }                    

                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                    
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.ProductoID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }                    
                }

                return RedirectToAction("IndexVendedor", "Productos");
            }
            ViewData["SubcategoriaID"] = new SelectList(_context.Subcategoria, "SubcategoriaID", "nombreSubcategoria", producto.SubcategoriaID);
            return View(producto);
        }

        // GET: Productos/Delete/5
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Producto
                .Include(p => p.Subcategoria)
                .Include(p => p.detalleVendedor)
                .FirstOrDefaultAsync(m => m.ProductoID == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }


        //Activar el producto Admin
        [Authorize(Roles = "Admin")]
        public async Task<RedirectToActionResult> ActivarProducto(int id)
        {
            var productoSeleccionado = _context.Producto.SingleOrDefault(p => p.ProductoID == id);
            productoSeleccionado.estadoProducto = true;
            productoSeleccionado.Existencia = productoSeleccionado.existenciaSinActivacion;            
            _context.SaveChanges();
            return RedirectToAction("IndexAdministrador");
        }

        //Desactivar el producto Admin
        [Authorize(Roles = "Admin")]
        public async Task<RedirectToActionResult> DesactivarProducto(int id)
        {
            var productoSeleccionado = _context.Producto.SingleOrDefault(p => p.ProductoID == id);
            productoSeleccionado.estadoProducto = false;
            productoSeleccionado.existenciaSinActivacion = productoSeleccionado.Existencia;
            productoSeleccionado.Existencia = 0;  
            _context.SaveChanges();
            return RedirectToAction("IndexAdministrador");
        }


        // POST: Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Seller")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Producto.FindAsync(id);
            _context.Producto.Remove(producto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return _context.Producto.Any(e => e.ProductoID == id);
        }
    }
}