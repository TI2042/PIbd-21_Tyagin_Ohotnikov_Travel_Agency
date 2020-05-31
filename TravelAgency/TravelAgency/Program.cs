using TravelAgencyBusinessLogic.BusinessLogic;
using TravelAgencyBusinessLogic.Interfaces;
using TravelAgencyDatabaseImplement.Implements;
using System;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace TravelAgency
{
    static class Program
    {
        public static bool Cheak;
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var login = new FormLogin();
            login.ShowDialog();
            if (Cheak)
            {
                Application.Run(container.Resolve<FormMain>());
            }
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IGuideLogic, GuideLogic>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IRequestLogic, RequestLogic>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<MainLogic>
                (new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ReportLogic>
                (new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOrderLogic, OrderLogic>
                (new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IHotelLogic, HotelLogic>
                (new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ITourLogic, TourLogic>
                (new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISupplierLogic, SupplierLogic>
                (new HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}