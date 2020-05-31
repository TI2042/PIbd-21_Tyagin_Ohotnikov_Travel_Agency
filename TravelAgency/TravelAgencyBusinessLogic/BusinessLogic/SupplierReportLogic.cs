using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using TravelAgencyBusinessLogic.HelperModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModels;

namespace TravelAgencyBusinessLogic.BusinessLogic
{
    public class SupplierReportLogic
    {
        private readonly IGuideLogic guideLogic;
        private readonly IRequestLogic requestLogic;
        private readonly IHotelLogic hotelLogic;

        public SupplierReportLogic(IGuideLogic guideLogic, IRequestLogic requestLogic, IHotelLogic hotelLogic)
        {
            this.guideLogic = guideLogic;
            this.requestLogic = requestLogic;
            this.hotelLogic = hotelLogic;

        }
        public List<ReportHotelGuideViewModel> GetGuides()
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

        public void SaveNeedGuideToWordFile(string fileName, RequestViewModel request, string email)
        {
            string title = "Список требуемых продуктов";
            SaveToWord.CreateDoc(new WordInfo
            {
                FileName = fileName,
                Title = title,
                //Guides = GetGuides()
            });
            SendMail(email, fileName, title);
        }
        public void SaveTravelToursToExcelFile(string fileName, RequestViewModel request, string email)
        {
            string title = "Список требуемых гидов";
            SaveToExcel.CreateDoc(new ExcelInfo
            {
                FileName = fileName,
                Title = title,
                //Guides = GetGuides()
            });
            SendMail(email, fileName, title);
        }
        public void SendMail(string email, string fileName, string subject)
        {
            MailAddress from = new MailAddress("tis3.1415@gmail.com", "Турфирма Иван Сусанин");
            MailAddress to = new MailAddress(email);
            MailMessage m = new MailMessage(from, to);
            m.Subject = subject;
            m.Attachments.Add(new Attachment(fileName));
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("tis3.1415@gmail.com", "");
            smtp.EnableSsl = true;
            smtp.Send(m);
        }
    }
}
