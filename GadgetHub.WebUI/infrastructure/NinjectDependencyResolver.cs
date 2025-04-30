using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Moq;
using Ninject;
using GadgetHub.Domain.Entities;
using GadgetHub.Domain.Abstract;
using GadgetHub.Domain.Concrete;
using System.Configuration;
using GadgetHub.WebUI.Infrastructure.Abstract;
using GadgetHub.WebUI.Infrastructure.Concrete;

namespace GadgetHub.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel myKernal;

        public NinjectDependencyResolver(IKernel kernalParam)
        {
            myKernal = kernalParam;
            AddBindings();
        }

        public object GetService(Type myServiceType)
        {
            return myKernal.GetService(myServiceType);
        }

        public IEnumerable<object> GetServices(Type myServiceType)
        {
            return myKernal.GetAll(myServiceType);
        }

        private void AddBindings() 
        { 
            //Mock<IGadgetRepository> myMock = new Mock<IGadgetRepository>();
            //myMock.Setup(m => m.Gadgets).Returns(new List<Gadget>
            //{
            //    new Gadget{Name="Laptop", Price= 800, Category = "Computers", Brand = "Dell" },
            //    new Gadget{Name="Subwoofer", Price= 399, Category = "Audio", Brand = "JBL" },
            //    new Gadget{Name="Keyboard", Price= 95, Brand = "Logitech", Category = "Peripheral" },
            //});
            //myKernal.Bind<IGadgetRepository>().ToConstant(myMock.Object);
            myKernal.Bind<IGadgetRepository>().To<EFGadgetRepository>();

            myKernal.Bind<IAuthProvider>().To<FormsAuthProvider>();


            EmailSettings emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };
            myKernal.Bind<IOrderProcessor>().To<EmailOrderProcessor>()
                .WithConstructorArgument("settings", emailSettings);
        }
    }
}