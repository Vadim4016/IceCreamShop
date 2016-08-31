using System.Collections.Generic;
using System.Linq;

namespace Domain.Model
{
    // Класс представляет корзину покупателя
    public class Cart
    {
        private List<CartLine> CartLines = new List<CartLine>();
        public List<CartLine> Items { get { return CartLines; }}

        public void AddItem(IceCream iceCream, int quantity)
        {
            CartLine line = CartLines.Where(i => i.IceCream.Id == iceCream.Id).FirstOrDefault();

            if (line == null)
            {
                CartLines.Add(new CartLine {IceCream = iceCream, Quantity = quantity});
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(IceCream iceCream)
        {
            CartLines.RemoveAll(i => i.IceCream.Id == iceCream.Id);
        }

        public decimal ComputeTotalValue()
        {
            return CartLines.Sum(i => i.Quantity*i.IceCream.Price);
        }

        public void Clear()
        {
            CartLines.Clear();
        }
    }


    // Класс представляющий список покупок
    public class CartLine
    {
        public IceCream IceCream { get; set; }
        public int Quantity { get; set; }
    }
}
