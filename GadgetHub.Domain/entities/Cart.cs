using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GadgetHub.Domain.Abstract;

namespace GadgetHub.Domain.Entities
{
    public class CartLine
    {
        public Gadget Gadget { get; set; }
        public int Quantity { get; set; }
    }

    public class Cart
    {
        public List<CartLine> lineCollections = new List<CartLine>();

        public IEnumerable<CartLine> Lines
        {
            get { return lineCollections; }
        }

        // add gadget to cart
        public void AddItem(Gadget myGadget, int quantity)
        {
            CartLine line = lineCollections
                .Where(p => p.Gadget.ProductId == myGadget.ProductId)
                .FirstOrDefault();
            if (line == null)
            {
                lineCollections.Add(new CartLine
                {
                    Gadget = myGadget,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Gadget myGadget)
        {
            lineCollections.RemoveAll(l => l.Gadget.ProductId == myGadget.ProductId);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollections.Sum(e => e.Gadget.Price * e.Quantity);
        }

        public void Clear()
        {
            lineCollections.Clear();
        }
    }
}
