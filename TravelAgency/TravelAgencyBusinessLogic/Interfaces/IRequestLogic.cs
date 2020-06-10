using System;
using System.Collections.Generic;
using System.Text;
using TravelAgencyBusinessLogic.BindingModels;
using TravelAgencyBusinessLogic.ViewModels;

namespace TravelAgencyBusinessLogic.Interfaces
{
    public interface IRequestLogic
    {
        List<RequestViewModel> Read(RequestBindingModel model);
        void CreateOrUpdate(RequestBindingModel model);
        void Delete(RequestBindingModel model);
        void Reserve(ReserveGuideBindingModel model);
        void SaveJsonRequest(string folderName);
        void SaveJsonRequestGuide(string folderName);
        void SaveXmlRequest(string folderName);
        void SaveXmlRequestGuide(string folderName);
    }
}
