using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.HelperModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModels;

namespace TravelAgencyBusinessLogic.BusinessLogic
{
    public class SupplierReportLogic
    {
        private readonly IRequestLogic requestLogic;
        public SupplierReportLogic(IRequestLogic requestLogic)
        {
            this.requestLogic = requestLogic;
        }

        public Dictionary<int, (string, int, bool)> GetRequestGuides(int requestId)
        {
            var requestGuides = requestLogic.Read(new RequestBindingModel
            {
                Id = requestId
            })?[0].Guides;
            return requestGuides;
        }

        public void SaveNeedGuideToWordFile(WordInfo wordInfo, string email)
        {
            string title = "Список требуемых гидов по заявке №" + wordInfo.RequestId;
            wordInfo.Title = title;
            wordInfo.FileName = wordInfo.FileName;
            wordInfo.Date = DateTime.Now;
            wordInfo.RequestGuides = GetRequestGuides(wordInfo.RequestId);
            SupplierSaveToWord.CreateDoc(wordInfo);
            SendMail(email, wordInfo.FileName, title);
        }

        public void SaveNeedGuideToExcelFile(ExcelInfo excelInfo, string email)
        {
            string title = "Список требуемых гидов по заявке №" + excelInfo.RequestId;
            excelInfo.Title = title;
            excelInfo.FileName = excelInfo.FileName;
            excelInfo.DateComplete = DateTime.Now;
            excelInfo.RequestGuides = GetRequestGuides(excelInfo.RequestId);
            SupplierSaveToExcel.CreateDoc(excelInfo);
            SendMail(email, excelInfo.FileName, title);
        }

        public void SendMail(string email, string fileName, string subject)
        {
            MailAddress from = new MailAddress("tis3.1415@gmail.com", "Турфирма Иван Сусанин");
            MailAddress to = new MailAddress(email);
            MailMessage m = new MailMessage(from, to);
            m.Subject = subject;
            m.Attachments.Add(new Attachment(fileName));
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("tis3.1415@gmail.com", "****");
            smtp.EnableSsl = true;
            smtp.Send(m);
        }
    }
}
