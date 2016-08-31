using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model;

namespace Domain.Abstract
{
    public interface IFilter
    {
        List<IceCream> FilterByOderCost(List<IceCream> collection, string oderParam);
        List<IceCream> FilterByCost(List<IceCream> collection, int cost);
        List<IceCream> FilterByFat(List<IceCream> collection, int fat);
        List<IceCream> FilterByFat(List<IceCream> collection, int downRange, int upRange);
        List<IceCream> FilterByFillers(List<IceCream> collection, Filler fillers);
    }
}
