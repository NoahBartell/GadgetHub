using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Entities;

namespace GadgetHub.Domain.Concrete
{
    public class EFGadgetRepository : IGadgetRepository
    {
        private EFDbContext context = new EFDbContext();

        public IEnumerable<Gadget> Gadgets
        {
            get { return context.Gadget; }
        }

        void IGadgetRepository.SaveGadget(Gadget product)
        {
            if (product.ProductId == 0)
            {
                context.Gadget.Add(product);
            }
            else
            {
                Gadget dbEntry = context.Gadget.Find(product.ProductId);
                if (dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                    dbEntry.ImageData = product.ImageData;
                    dbEntry.ImageMimeType = product.ImageMimeType;
                }
            }
            context.SaveChanges();
        }

        public Gadget DeleteGadget(int productID)
        {
            Gadget dbEntry = context.Gadget.Find(productID);
            if (dbEntry != null)
            {
                context.Gadget.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
