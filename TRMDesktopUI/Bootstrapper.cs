using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TRMDesktopUI.Helpers;
using TRMDesktopUI.Library.Api;
using TRMDesktopUI.Library.Models;
using TRMDesktopUI.ViewModels;

namespace TRMDesktopUI
{
    public class Bootstrapper : BootstrapperBase
    {

        private SimpleContainer _container = new SimpleContainer();

        public Bootstrapper() // constructor
        {
            Initialize();

           ConventionManager.AddElementConvention<PasswordBox>(
           PasswordBoxHelper.BoundPasswordProperty,
           "Password",
           "PasswordChanged");
        }

        protected override void Configure()
        {
            _container.Instance(_container);

            // specific to caliburn

            _container
                .Singleton<IWindowManager, WindowManager>()     // Handling windows in and out
                .Singleton<IEventAggregator, EventAggregator>() // Pass event message throughout app instead of passing data trhough constructor and method; link everything
                .Singleton<IAPIHelper, APIHelper>()            // singleton = create one instance of the class for the life of app or scope of container ( same thing here ) same as a static class
                .Singleton<ILoggedInUserModel, LoggedInUserModel>();
                //.Singleton<LoggedInUserModel>();            //we could use that to create an instance of loggedInUserModel and reuse the same instance when needed but it is better to use an interface for the futur

            GetType().Assembly.GetTypes()
                .Where(Type => Type.IsClass)
                .Where(Type => Type.Name.EndsWith("ViewModel"))
                .ToList()
                .ForEach(viewModelType => _container.RegisterPerRequest(
                    viewModelType, viewModelType.ToString(), viewModelType));
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>(); // On startup launch ShellViewModel
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }
    }
}
