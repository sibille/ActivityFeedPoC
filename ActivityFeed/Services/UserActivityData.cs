using ActivityFeed.Activation;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.UserActivities;
using Windows.UI;

namespace ActivityFeed.Services
{
    public class UserActivityData
    {
        public string ActivityId { get; set; }

        public SchemeActivationData ActivationData { get; set; }

        public string DisplayText { get; set; }

        public Color BackgroundColor { get; set; } = default(Color);

        public async Task<UserActivity> ToUserActivity()
        {
            if (string.IsNullOrEmpty(ActivityId)) throw new ArgumentNullException(nameof(ActivityId));
            if (ActivationData == null) throw new ArgumentNullException(nameof(ActivationData));
            if (string.IsNullOrEmpty(DisplayText)) throw new ArgumentNullException(nameof(DisplayText));

            var channel = UserActivityChannel.GetDefault();

            var activity = await channel.GetOrCreateUserActivityAsync(ActivityId);
            activity.ActivationUri = ActivationData.Uri;
            activity.VisualElements.DisplayText = DisplayText;
            activity.VisualElements.BackgroundColor = BackgroundColor;

            return activity;
        }
    }
}
