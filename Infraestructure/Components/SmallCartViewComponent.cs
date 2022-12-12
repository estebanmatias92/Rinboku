using Microsoft.AspNetCore.Mvc;
using Rinboku.Models;
using Rinboku.Models.ViewModels;

namespace Rinboku.Infraestructure.Components
{
    public class SmallCartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            SmallCartViewModel smallCartViewModel;

            if (cart == null || cart.Count == 0)
            {
                smallCartViewModel = null;
            }
            else
            {
                smallCartViewModel = new()
                {
                    NumberOfItems = cart.Sum(item => item.Quantity),
                    TotalAmount = cart.Sum(item => item.Quantity * item.Price)
                };
            }

            return View(smallCartViewModel);
        }
    }
}
