using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.Enums;
using TravelAgencyBusinessLogic.HelperModels;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyBusinessLogic.ViewModels;

namespace TravelAgencyBusinessLogic.BusinessLogic
{
    public class SupplierReportLogic
    {
        private readonly IRequestLogic requestLogic;
        private readonly IGuideLogic guideLogic;
        public SupplierReportLogic(IRequestLogic requestLogic, IGuideLogic guideLogic)
        {
            this.requestLogic = requestLogic;
            this.guideLogic = guideLogic;
        }

        public Dictionary<int, (string, int, bool)> GetRequestGuides(int requestId)
        {
            var requestGuides = requestLogic.Read(new RequestBindingModel
            {
                Id = requestId
            })?[0].Guides;
            return requestGuides;
        }

        public List<ReportGuideViewModel> GetGuides(RequestBindingModel model)
        {
            var guides = guideLogic.Read(null);
            var requests = requestLogic.Read(model);
            var list = new List<ReportGuideViewModel>();
            foreach (var request in requests)
            {
                foreach (var requestGuide in request.Guides)
                {
                    foreach (var guide in guides)
                    {
                        if (guide.GuideThemeName == requestGuide.Value.Item1)
                        {
                            var record = new ReportGuideViewModel
                            {
                                RequestId = request.Id,
                                SupplierFIO = request.SupplierFIO,
                                GuideThemeName = requestGuide.Value.Item1,
                                Count = requestGuide.Value.Item2,
                                Status = StatusGuide(request.Status),
                                Date = request.Date,
                                Price = guide.Price,
                                Sum = request.Sum
                            };
                            list.Add(record);
                        }
                    }
                }
            }
            return list;
        }

        public void SaveNeedGuideToWordFile(WordInfo wordInfo, string email)
        {
            string title = "Список требуемых гидов по заявке №" + wordInfo.RequestId;
            wordInfo.Title = title;
            wordInfo.FileName = wordInfo.FileName;
            wordInfo.RequestGuides = GetRequestGuides(wordInfo.RequestId);
            SupplierSaveToWord.CreateDoc(wordInfo);
            SendMail(email, wordInfo.FileName, title);
        }

        public void SaveNeedGuideToExcelFile(ExcelInfo excelInfo, string email)
        {
            string title = "Список требуемых гидов по заявке №" + excelInfo.RequestId;
            excelInfo.Title = title;
            excelInfo.FileName = excelInfo.FileName;
            excelInfo.RequestGuides = GetRequestGuides(excelInfo.RequestId);
            SupplierSaveToExcel.CreateDoc(excelInfo);
            SendMail(email, excelInfo.FileName, title);
        }

        public void SaveGuidesToPdfFile(string fileName, RequestBindingModel model, string email)
        {
            string title = "Список гидов в период с " + model.DateFrom.ToString() + " по " + model.DateTo.ToString();
            SupplierSaveToPdf.CreateDoc(new PdfInfo
            {
                FileName = fileName,
                Title = title,
                Guides = GetGuides(model)
            });
            SendMail(email, fileName, title);
        }

        public string StatusGuide(RequestStatus requestStatus)
        {
            if (requestStatus == RequestStatus.Создана)
                return "Ждет отправки";
            if (requestStatus == RequestStatus.Выполняется)
                return "В пути";
            if (requestStatus == RequestStatus.Готова)
                return "Поставлен";
            if (requestStatus == RequestStatus.Обработана)
                return "Отработал";
            return "";
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

        public void SendMailBackup(string email, string fileName, string subject, string type)
        {
            MailAddress from = new MailAddress("labwork15kafis@gmail.com", "Турфирма Иван Сусанин");
            MailAddress to = new MailAddress(email);
            MailMessage m = new MailMessage(from, to);
            m.Subject = subject;
            m.Attachments.Add(new Attachment(fileName + "\\Request." + type));
            m.Attachments.Add(new Attachment(fileName + "\\RequestGuide." + type));
            m.Attachments.Add(new Attachment(fileName + "\\Hotel." + type));
            m.Attachments.Add(new Attachment(fileName + "\\HotelGuide." + type));
            m.Attachments.Add(new Attachment(fileName + "\\Supplier." + type));
            m.Attachments.Add(new Attachment(fileName + "\\Guide." + type));
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("labwork15kafis@gmail.com", "passlab15");
            smtp.EnableSsl = true;
            smtp.Send(m);
        }
    }
}
