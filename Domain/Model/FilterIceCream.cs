using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Abstract;

namespace Domain.Model
{
    public class FilterIceCream : IFilter
    {
        public List<IceCream> FilterByOderCost(List<IceCream> collection, string oderParam)
        {
            if (collection != null)
            {
                if (oderParam != null)
                {
                    switch (oderParam.ToLower())
                    {
                        case "up":
                            return collection.OrderBy(m => m.Price).ToList();
                        case "down":
                            return collection.OrderByDescending(m => m.Price).ToList();
                        default:
                            return collection;
                    }
                }
            }
            
            return collection;
        }

        public List<IceCream> FilterByCost(List<IceCream> collection, int cost)
        {
            return collection.Where(m => m.Price == cost).ToList();
        }

        public List<IceCream> FilterByFat(List<IceCream> collection, int fat)
        {
            return collection.Where(m => m.Fat == fat).ToList();
        }

        public List<IceCream> FilterByFat(List<IceCream> collection, int downRange, int upRange)
        {
            if (downRange < upRange && upRange != 0)
            {
                return collection.Where(m => m.Fat >= downRange).Where(m => m.Fat <= upRange).ToList();
            }

            return collection;
        }

        public List<IceCream> FilterByFillers(List<IceCream> collection, Filler fillers)
        {
            return collection.Where(m => m.Filler.Fruit == fillers.Fruit)
                .Where(m => m.Filler.Jams == fillers.Jams)
                .Where(m => m.Filler.SugarPowder == fillers.SugarPowder)
                .Where(m => m.Filler.Syrups == fillers.Syrups)
                .Where(m => m.Filler.Сhocolate == fillers.Сhocolate).ToList();
        }
    }
}
