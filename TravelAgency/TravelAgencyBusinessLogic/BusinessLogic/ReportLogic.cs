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
using TravelAgencyBusinessLogic.Enums;

namespace TravelAgencyBusinessLogic.BusinessLogic
{
    public class ReportLogic
    {
        private readonly ITourLogic tourLogic;
        private readonly IOrderLogic orderLogic;
        private readonly IGuideLogic guideLogic;
        private readonly IRequestLogic requestLogic;

        public ReportLogic(ITourLogic tourLogic, IOrderLogic orderLogic,
            IGuideLogic guideLogic, IRequestLogic requestLogic)
        {
            this.tourLogic = tourLogic;
            this.orderLogic = orderLogic;
            this.guideLogic = guideLogic;
            this.requestLogic = requestLogic;
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
                        GuideThemeName = pc.Value.Item1,
                        Count = pc.Value.Item2
                    };
                    list.Add(record);
                }
            }
            return list;
        }

        public List<ReportGuideViewModel> GetGuides(DateTime from, DateTime to)
        {
            var guides = guideLogic.Read(null);
            var requests = requestLogic.Read(null);
            var list = new List<ReportGuideViewModel>();
            foreach (var request in requests)
            {
                if (request.CreationDate >= from && request.CreationDate.Value.AddDays(-1) <= to)
                {
                    foreach (var requestGuide in request.Guides)
                    {
                        foreach (var guide in guides)
                        {
                            if (guide.GuideThemeName == requestGuide.Value.Item1)
                            {
                                var record = new ReportGuideViewModel
                                {
                                    GuideThemeName = requestGuide.Value.Item1,
                                    Count = requestGuide.Value.Item2,
                                    Status = StatusGuide(request.Status),
                                    CreationDate = request.CreationDate,
                                    CompletionDate = request.CompletionDate,
                                    Price = guide.Price * requestGuide.Value.Item2
                                };
                                list.Add(record);
                            }
                        }
                    }
                }
            }
            return list;
        }

        public string StatusGuide(RequestStatus requestStatus)
        {
            if (requestStatus == RequestStatus.Создана)
                return "Ждут отправки";
            if (requestStatus == RequestStatus.Выполняется)
                return "В пути";
            if (requestStatus == RequestStatus.Готова)
                return "Поставлено";
            if (requestStatus == RequestStatus.Обработана)
                return "Использовано";
            return "";
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
                    Title = "Список выполненных туров",
                    Orders = GetReportOrder(model),
                    TourGuides = GetTourGuides()
                });
            }
            catch (Exception)
            {
                throw;
            }
            SendMail("den.ohotnikov@gmail.com", model.FileName, "Список туров с гидами");
        }

        public void SaveOrdersToExcelFile(ReportBindingModel model)
        {
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список выполненных туров",
                Orders = GetReportOrder(model),
                TourGuides = GetTourGuides()
            });
            SendMail("den.ohotnikov@gmail.com", model.FileName, "Список туров с гидами");
        }

        public void SaveGuidesToPdfFile(ReportBindingModel model)
        {
            SaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Отчет по движению гидов",
                Guides = GetGuides(model.DateFrom, model.DateTo),
                DateTo = model.DateTo,
                DateFrom = model.DateFrom
            });
            SendMail("den.ohotnikov@gmail.com", model.FileName, "Отчет по движению гидов");
        }

        public void SendMail(string email, string fileName, string subject)
        {
            MailAddress from = new MailAddress("labwork15kafis@gmail.com", "Турфирма Иван Сусанин");
            MailAddress to = new MailAddress(email);
            MailMessage m = new MailMessage(from, to);
            m.Subject = subject;
            m.Attachments.Add(new Attachment(fileName));
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("labwork15kafis@gmail.com", "passlab15");
            smtp.EnableSsl = true;
            smtp.Send(m);
        }

        public void SendMailReport(string email, string fileName, string subject, string type)
        {
            MailAddress from = new MailAddress("labwork15kafis@gmail.com", "Турфирма Иван Сусанин");
            MailAddress to = new MailAddress(email);
            MailMessage m = new MailMessage(from, to);
            m.Subject = subject;
            m.Attachments.Add(new Attachment(fileName + "\\Order." + type));
            m.Attachments.Add(new Attachment(fileName + "\\Request." + type));
            m.Attachments.Add(new Attachment(fileName + "\\RequestGuide." + type));
            m.Attachments.Add(new Attachment(fileName + "\\Tour." + type));
            m.Attachments.Add(new Attachment(fileName + "\\TourGuide." + type));
            m.Attachments.Add(new Attachment(fileName + "\\Guide." + type));
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("labwork15kafis@gmail.com", "passlab15");
            smtp.EnableSsl = true;
            smtp.Send(m);
        }
    }
}