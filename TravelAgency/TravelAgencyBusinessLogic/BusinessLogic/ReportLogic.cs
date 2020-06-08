using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
                foreach (var gh in hotel.Guides)
                {
                    var record = new ReportHotelGuideViewModel
                    {
                        HotelName = hotel.HotelName,
                        GuideName = gh.Value.Item1,
                        Count = gh.Value.Item2
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

        public List<ReportOrdersViewModel> GetOrder(ReportBindingModel model)
        {
            return orderLogic.Read(new OrderBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            })
            .Select(x => new ReportOrdersViewModel
            {
                CreationDate = x.CreationDate,
                TourName = x.TourName,
                Count = x.Count,
                Amount = x.Sum,
                Status = x.Status
            })
            .ToList();
        }

        public List<ReportOrdersViewModel> GetReportOrder(ReportBindingModel model)
        {
            var tours = orderLogic.Read(null);
            var list = new List<ReportOrdersViewModel>();
            foreach (var tour in tours)
            {
                var record = new ReportOrdersViewModel
                {
                    TourName = tour.TourName,
                    Amount = tour.Sum,
                    Count = tour.Count,
                    CreationDate = tour.CreationDate,
                    Status = tour.Status
                };
                list.Add(record);
            }
            return list;
        }

        public void SaveOrdersToWordFile(ReportBindingModel model)
        {
            try
            {
                SaveToWord.CreateDoc(new WordInfo
                {
                    FileName = model.FileName,
                    Title = "Список приготовленных блюд",
                    Orders = GetReportOrder(model),
                    TourGuides = GetTourGuides()
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SaveOrdersToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список приготовленных блюд",
                Orders = GetReportOrder(model),
                TourGuides = GetTourGuides()
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

        public void SendMail(string email, string fileName, string subject)
        {
            MailAddress from = new MailAddress("den.ohotnikov@gmail.com", "Турфирма Иван Сусанин");
            MailAddress to = new MailAddress(email);
            MailMessage m = new MailMessage(from, to);
            m.Subject = subject;
            m.Attachments.Add(new Attachment(fileName));
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("den.ohotnikov@gmail.com", "1");
            smtp.EnableSsl = true;
            smtp.Send(m);
        }
    }
}