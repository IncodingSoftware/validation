using System;

[assembly: WebActivator.PreApplicationStartMethod(
    typeof(Validator.UI.App_Start.IncodingStart), "PreStart")]

namespace Validator.UI.App_Start {
    
    using Validator.UI.Controllers;

    public static class IncodingStart {
        public static void PreStart() {
            Bootstrapper.Start();
            new DispatcherController(); // init routes
        }
    }
}