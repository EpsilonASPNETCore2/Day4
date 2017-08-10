using EComm.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EComm.MVC.Components
{
    public class ProductCountViewComponent
    {
        private ECommData ECommData { get; }
        public ProductCountViewComponent(ECommData ecommData)
        {
            ECommData = ecommData;
        }

        public string Invoke()
        {
            return $"{ECommData.GetProducts().Count()} Products";
        }
    }

    public class PartialViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("Detail");
        }
    }
}
