using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ActivityFeed.Helpers;

namespace ActivityFeed.ViewModels
{
    public class SchemeActivationSampleViewModel : Observable
    {
        public ObservableCollection<string> Parameters { get; } = new ObservableCollection<string>();

        public SchemeActivationSampleViewModel()
        {
        }

        public void Initialize(Dictionary<string, string> parameters)
        {
            foreach (var param in parameters)
            {
                if (param.Key == "ticks" && long.TryParse(param.Value, out long ticks))
                {
                    var dateTime = new DateTime(ticks);
                    Parameters.Add($"{param.Key}: {dateTime.ToString()}");
                }
                else
                {
                    Parameters.Add($"{param.Key}: {param.Value}");
                }
            }
        }
    }
}
