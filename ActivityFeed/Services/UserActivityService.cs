using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.UserActivities;
using Windows.UI;
using Windows.UI.Shell;
using ActivityFeed.Activation;

namespace ActivityFeed.Services
{
    // For more info about UserActivities in Timeline see
    // https://blogs.windows.com/buildingapps/2017/12/19/application-engagement-windows-timeline-user-activities/#q4oyyjCE45qW8MMl.97
    // For more info about UserActivities with AdaptativeCards see
    // https://docs.microsoft.com/adaptive-cards/get-started/windows
    public static partial class UserActivityService
    {
        private static UserActivitySession _currentUserActivitySession;

        public static async Task AddAsync(UserActivityData activityData, string description = null)
        {
            var activity = await activityData.ToUserActivity();
            activity.VisualElements.Description = description ?? string.Empty;
            activity.VisualElements.Content = null;
            await SaveAsync(activity);
        }

        public static async Task AddAsync(UserActivityData activityData, IAdaptiveCard adaptiveCard)
        {
            var activity = await activityData.ToUserActivity();
            activity.VisualElements.Content = adaptiveCard;
            await SaveAsync(activity);
        }

        private static async Task SaveAsync(UserActivity activity)
        {
            await activity.SaveAsync();

            //Dispose of any current UserActivitySession, and create a new one.
            _currentUserActivitySession?.Dispose();
            _currentUserActivitySession = activity.CreateSession();
        }
    }
}
