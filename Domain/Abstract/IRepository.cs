using System.Collections.Generic;
using Domain.Model;

namespace Domain.Abstract
{
    public interface IRepository
    {
        IEnumerable<IceCream> GetAllIceCreams();
        IceCream GetIceCream(int id);
        void DeleteIceCream(int id);
        void AddIceCream(IceCream iceCream);
        void EditIceCream(IceCream iceCream);
        IEnumerable<IceCream> GetAllHitsIceCreams();
    }
}
