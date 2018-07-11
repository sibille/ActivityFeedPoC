using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using ActivityFeed.Switch.Services;
using ActivityFeed.Switch.Views;

using Windows.ApplicationModel.Activation;

namespace ActivityFeed.Switch.Activation
{
    // TODO WTS: Open package.appxmanifest and change the declaration for the scheme (from the default of 'wtsapp') to what you want for your app.
    // More details about this functionality can be found at https://github.com/Microsoft/WindowsTemplateStudio/blob/master/docs/features/uri-scheme.md
    // TODO WTS: Change the image in Assets/Logo.png to one for display if the OS asks the user which app to launch.
    internal class SchemeActivationHandler : ActivationHandler<ProtocolActivatedEventArgs>
    {
        // By default, this handler expects URIs of the format 'wtsapp:sample?secret={value}'
        protected override async Task HandleInternalAsync(ProtocolActivatedEventArgs args)
        {

            var uriPath = args.Uri.AbsolutePath;
            var parameters = GetParamsFromUri(args.Uri);

            switch (uriPath)
            {
                case "sample":
                    NavigationService.Navigate(typeof(SchemeActivationSamplePage), parameters);
                    break;
                default:
                    if (args.PreviousExecutionState != ApplicationExecutionState.Running)
                    {
                        // If the app isn't running and not navigating to a specific page based on the URI, navigate to the home page
                        NavigationService.Navigate(typeof(Views.MainPage));
                    }
                    break;
            }



            await Task.CompletedTask;
        }

        protected override bool CanHandleInternal(ProtocolActivatedEventArgs args)
        {
            // If your app has multiple handlers of ProtocolActivationEventArgs
            // use this method to determine which to use. (possibly checking args.Uri.Scheme)
            return true;
        }

        private static Dictionary<string, string> GetParamsFromUri(Uri uri)
        {
            var uriQuery = HttpUtility.ParseQueryString(uri.Query);
            var parameters = new Dictionary<string, string>();
            foreach (var paramKey in uriQuery.AllKeys)
            {
                parameters.Add(paramKey, uriQuery.Get(paramKey));
            }

            return parameters;
        }
    }
}
