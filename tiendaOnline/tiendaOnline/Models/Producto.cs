﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace tiendaOnline.Models
{
    public class Producto
    {
       public int ProductoID { get; set; }
        //it may be nombre
        [Display(Name = "Nombre del Producto")]
        [Required(ErrorMessage ="Ingresar el nombre del Producto"), MinLength(5, ErrorMessage ="Ingresar minimo 5 caracteres"), StringLength(50, ErrorMessage ="El nombre del producto solo admite como máximo 50 caracteres")]
        public string NombreProducto { get; set; }

        [Display(Name = "Precio Unitario")]
        [Required(ErrorMessage ="Ingresar el Precio del producto"), Range(0.01, 1000.00, ErrorMessage ="El producto no puede valer más de $1000.00")]
        public double PrecioUnitario { get; set; }

        [Display(Name = "Cantidad de Productos")]
        [Required(ErrorMessage ="Ingresar Existencia"), Range(1,100,ErrorMessage ="Debe ingresar menos de 100 productos a la venta")]
        public int Existencia { get; set; }

        public string Codigo { get; set; }

        [Display(Name = "Imagen")]
        [Required(ErrorMessage ="Debe introducir una imagen")]
        public string Imagen { get; set; }

        //Relacion con DetalleProducto
        public DetalleProducto DetalleProducto { get; set; }
        //Relacion con TipoDeDescuento

        public int existenciaSinActivacion { get; set; }
        public Boolean estadoProducto { get; set; }
       
        [Required(ErrorMessage ="Debe seleccionar una subcategoria para su producto")]
        public int SubcategoriaID { get; set; }
        public Subcategoria Subcategoria { get; set; }
        //Relacion con Vendedor
        [Display(Name = "Vendedor")]
        public int? detalleVendedorID { get; set; }
        public DetalleVendedor detalleVendedor { get; set; }


    }
}

