using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.UserActivities;
using Windows.UI;

namespace ActivityFeed.Switch.Services
{
    public class UserActivityData
    {
        public string ActivityId { get; private set; }

        public Uri ActivationUri { get; private set; }

        public string DisplayText { get; private set; }

        public string Description { get; private set; }

        public Color BackgroundColor { get; private set; }

        public UserActivityData(string activityId, Uri activationUri, string displayText, Color backgroundColor = default(Color), string description = null)
        {
            ActivityId = activityId;
            ActivationUri = activationUri;
            DisplayText = displayText;
            BackgroundColor = backgroundColor;
            Description = description ?? string.Empty;
        }

        public async Task<UserActivity> ToUserActivity()
        {
            if (string.IsNullOrEmpty(ActivityId)) throw new ArgumentNullException(nameof(ActivityId));
            if (ActivationUri == null) throw new ArgumentNullException(nameof(ActivationUri));
            if (string.IsNullOrEmpty(DisplayText)) throw new ArgumentNullException(nameof(DisplayText));

            var channel = UserActivityChannel.GetDefault();

            var activity = await channel.GetOrCreateUserActivityAsync(ActivityId);
            activity.ActivationUri = ActivationUri;
            activity.VisualElements.DisplayText = DisplayText;
            activity.VisualElements.BackgroundColor = BackgroundColor;
            activity.VisualElements.Description = Description;

            return activity;
        }
    }
}
