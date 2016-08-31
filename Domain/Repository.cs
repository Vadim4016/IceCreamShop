using System.Collections.Generic;
using System.Linq;
using Domain.Abstract;
using Domain.Context;
using Domain.Model;

namespace Domain
{
    public class Repository : IRepository
    {
        private IceCreamContext Db { get; set; }

        public Repository()
        {
            Db = new IceCreamContext();
        }

        public IEnumerable<IceCream> GetAllIceCreams()
        {
            List<IceCream> iceCreams = Db.IceCreams.Include("Image").ToList();
            return iceCreams;
        }

        public IceCream GetIceCream(int id)
        {
            IceCream iceCream = Db.IceCreams.Include("Image").First(i => i.Id == id);
            return iceCream;
        }

        public void DeleteIceCream(int id)
        {
            IceCream iceCream = Db.IceCreams.Include("Image").First(i => i.Id == id);
            if (iceCream.Image != null)
            {
                Db.Images.Remove(iceCream.Image);
            }
            Db.IceCreams.Remove(iceCream);
            Db.SaveChanges();
        }

        public void AddIceCream(IceCream iceCream)
        {
            Db.IceCreams.Add(iceCream);
            Db.SaveChanges();
        }

        public void EditIceCream(IceCream iceCream)
        {
            var newIceCream = Db.IceCreams.Include("Image").First(i => i.Id == iceCream.Id);
            Db.IceCreams.Remove(newIceCream);
            Db.IceCreams.Add(iceCream);
            Db.SaveChanges();
        }

        public IEnumerable<IceCream> GetAllHitsIceCreams()
        {
            var allHitsIceCream = Db.IceCreams.Include("Image").Where(i => i.Hit);
            return allHitsIceCream;
        }
    }
}

