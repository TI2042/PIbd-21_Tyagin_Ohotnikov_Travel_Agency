using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.HelperModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModels;

namespace TravelAgencyBusinessLogic.BusinessLogic
{
    public class ReportLogic
    {
        private readonly ITourLogic tourLogic;
        private readonly IOrderLogic orderLogic;
        private readonly IHotelLogic hotelLogic;

        public ReportLogic(ITourLogic tourLogic, IOrderLogic orderLogic, IHotelLogic hotelLogic)
        {
            this.tourLogic = tourLogic;
            this.orderLogic = orderLogic;
            this.hotelLogic = hotelLogic;
        }

        public List<ReportTourGuideViewModel> GetTourGuides()
        {
            var tours = tourLogic.Read(null);
            var list = new List<ReportTourGuideViewModel>();
            foreach (var tour in tours)
            {
                foreach (var pc in tour.TourGuides)
                {
                    var record = new ReportTourGuideViewModel
                    {
                        TourName = tour.TourName,
                        GuideName = pc.Value.Item1,
                        Count = pc.Value.Item2
                    };
                    list.Add(record);
                }
            }
            return list;
        }

        public List<ReportHotelGuideViewModel> GetHotelGuides()
        {
            var hotels = hotelLogic.Read(null);
            var list = new List<ReportHotelGuideViewModel>();
            foreach (var hotel in hotels)
            {
                foreach (var ff in hotel.Guides)
                {
                    var record = new ReportHotelGuideViewModel
                    {
                        HotelName = hotel.HotelName,
                        GuideName = ff.Value.Item1,
                        Count = ff.Value.Item2
                    };
                    list.Add(record);
                }
            }
            return list;
        }

        public List<IGrouping<DateTime, OrderViewModel>> GetOrders(ReportBindingModel model)
        {
            var list = orderLogic
            .Read(new OrderBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            })
            .GroupBy(rec => rec.CreationDate.Date)
            .OrderBy(recG => recG.Key)
            .ToList();
            return list;
        }

        public void SaveToursToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список гидов",
                Tours = tourLogic.Read(null),
                Hotels = null
            });
        }

        public void SaveOrdersToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                Orders = GetOrders(model),
                Hotels = null
            });
        }

        public void SaveTourGuidesToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список туров с гидами",
                TourGuides = GetTourGuides(),
                HotelGuides = null
            });
        }

        public void SaveHotelsToWordFile(ReportBindingModel model)
        {
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список отелей",
                Tours = null,
                Hotels = hotelLogic.Read(null)
            });
        }

        public void SaveHotelGuidesToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список гидов в отелях",
                Orders = null,
                Hotels = hotelLogic.Read(null)
            });
        }

        public void SaveGuidesToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список гидов",
                TourGuides = null,
                HotelGuides = GetHotelGuides()
            });
        }
    }
}
