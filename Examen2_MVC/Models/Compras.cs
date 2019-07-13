using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Examen2_MVC.Models
{
    public partial class Compras:producto
    {
        public int cantidad { get; set; }
        public double total { get { return cantidad * (double)precioventa; } }

    }
}