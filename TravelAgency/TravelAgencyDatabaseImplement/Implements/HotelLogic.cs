using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModels;
using TravelAgencyDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RestaurantDatabaseImplement.Implements
{
    public class HotelLogic : IHotelLogic
    {
        public List<HotelViewModel> GetList()
        {
            using (var context = new TravelAgencyDatabase())
            {
                return context.Hotels
                .ToList()
               .Select(rec => new HotelViewModel
               {
                   Id = rec.Id,
                   HotelName = rec.HotelName,
                   HotelGuides = context.HotelGuides,
               .Include(recFF => recFF.Food)
               .Where(recFF => recFF.HotelId == rec.Id).
               Select(x => new GuideViewModel
               {
                   Id = x.Id,
                   HotelId = x.HotelId,
                   GuideId = x.GuideId,
                   GuideName = context.Guides.FirstOrDefault(y => y.Id == x.GuideId).GuideName,
                   Count = x.Count,
                   IsReserved = x.IsReserved
               })
               .ToList()
               })
            .ToList();
            }
        }
        public HotelViewModel GetElement(int id)
        {
            using (var context = new TravelAgencyDatabase())
            {
                var elem = context.Hotels.FirstOrDefault(x => x.Id == id);
                if (elem == null)
                {
                    throw new Exception("Элемент не найден");
                }
                else
                {
                    return new HotelViewModel
                    {
                        Id = id,
                        HotelName = elem.HotelName,
                        HotelGuides = context.HotelGuides,
                        Capacity = elem.Capacity
                .Include(recSF => recSF.Food)
                .Where(recSF => recSF.FridgeId == elem.Id)
                        .Select(x => new HotelViewModel
                        {
                            Id = x.Id,
                            HotelId = x.HotelID,
                            GuideId = x.GuideId,
                            GuideName = context.Guides.FirstOrDefault(y => y.Id == x.GuideId).GuideName,
                            Count = x.Count,
                            IsReserved = x.IsReserved
                        }).ToList()
                    };
                }
            }
        }
        public void AddElement(HotelBindingModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                var elem = context.Hotels.FirstOrDefault(x => x.HotelName == model.HotelName);
                if (elem != null)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
                var hotel = new Hotel();
                context.Hotels.Add(hotel);
                hotel.FridgeName = model.FridgeName;
                context.SaveChanges();
            }
        }
        public void UpdElement(HotelBindingModel
            model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                var elem = context.Hotels.FirstOrDefault(x => x.HotelName == model.HotelName && x.Id != model.Id);
                if (elem != null)
                {
                    throw new Exception("Уже есть склад с таким названием");
                }
                var elemToUpdate = context.Hotels.FirstOrDefault(x => x.Id == model.Id);
                if (elemToUpdate != null)
                {
                    elemToUpdate.HotelName = model.HotelName;
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        public void DelElement(int id)
        {
            using (var context = new TravelAgencyDatabase())
            {
                var elem = context.Hotels.FirstOrDefault(x => x.Id == id);
                if (elem != null)
                {
                    context.Hotels.Remove(elem);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }
        public void FillFridge(HotelGuideViewModel model)
        {
            using (var context = new TravelAgencyDatabase())
            {
                var item = context.HotelGuides.FirstOrDefault(x => x.GuideId == model.GuideId
    && x.HotelId == model.HotelId);

                if (item != null)
                {
                    item.Count += model.Count;
                }
                else
                {
                    var elem = new HotelGuide();
                    context.HotelGuides.Add(elem);
                    elem.HotelId = model.HotelId;
                    elem.GuideId = model.GuideId;
                    elem.Count = model.Count;
                    elem.IsReserved = model.IsReserved;
                }
                context.SaveChanges();
            }
        }
        public void RemoveFromHotel(TourViewModel order)
        {
            using (var context = new RestaurantDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        var DishFoods = context..Where(x => x. == order.).ToList();
                        var FridgeFoods = context.HotelGuides.ToList();
                        foreach (var food in DishFoods)
                        {
                            var foodCount = food.Count * order.Count;
                            foreach (var sb in FridgeFoods)
                            {
                                if (sb.GuideId == food.GuideId && sb.Count >= foodCount)
                                {
                                    sb.Count -= foodCount;
                                    foodCount = 0;
                                    context.SaveChanges();
                                    break;
                                }
                                else if (sb.GuideId == food.GuideId && sb.Count < foodCount)
                                {
                                    foodCount -= sb.Count;
                                    sb.Count = 0;
                                    context.SaveChanges();
                                }
                            }
                            if (foodCount > 0)
                                throw new Exception("Недостаточно гидов на складе");
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}