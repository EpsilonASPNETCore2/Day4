using EComm.MVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EComm.MVC.ViewModels
{
    public class CartViewModel
    {
        public ShoppingCart Cart { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [CreditCard]
        public string CreditCard { get; set; }
    }
}
