using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ActivityFeed.Activation;
using ActivityFeed.Views;
using AdaptiveCards;
using Windows.UI;
using Windows.UI.Shell;

namespace ActivityFeed.Services
{
    public static partial class UserActivityService
    {
        public static async Task AddSampleUserActivity()
        {
            var displayText = $"Work on {nameof(SchemeActivationSamplePage)}";
            var description = $"UserActivity description, this activity was created at {DateTime.Now.ToShortTimeString()}";
            var imageUrl = "http://adaptivecards.io/content/cats/2.png";
            var adaptiveCard = CreateAdaptiveCardSample(displayText, description, imageUrl);

            var activityData = new UserActivityData
            {
                ActivityId = nameof(SchemeActivationSamplePage),
                ActivationData = CreateActivationDataSample(),
                DisplayText = displayText,
                BackgroundColor = Colors.DarkRed
            };

            await UserActivityService.AddAsync(activityData, adaptiveCard);
        }

        private static SchemeActivationData CreateActivationDataSample()
        {
            return new SchemeActivationData
            (
                pageType: typeof(SchemeActivationSamplePage),
                parameters: new Dictionary<string, string>()
                {
                    { "paramName1", "paramValue1" },
                    { "ticks", DateTime.Now.Ticks.ToString() }
                }
            );
        }

        // TODO WTS: Change this to configure your own adaptive card
        // For more info about adaptive cards see http://adaptivecards.io/
        private static IAdaptiveCard CreateAdaptiveCardSample(string displayText, string description, string imageUrl)
        {
            var adaptiveCard = new AdaptiveCard();
            var columns = new AdaptiveColumnSet();
            var firstColumn = new AdaptiveColumn() { Width = "auto" };
            var secondColumn = new AdaptiveColumn() { Width = "*" };

            firstColumn.Items.Add(new AdaptiveImage()
            {
                Url = new Uri(imageUrl),
                Size = AdaptiveImageSize.Medium
            });

            secondColumn.Items.Add(new AdaptiveTextBlock()
            {
                Text = displayText,
                Weight = AdaptiveTextWeight.Bolder,
                Size = AdaptiveTextSize.Large
            });

            secondColumn.Items.Add(new AdaptiveTextBlock()
            {
                Text = description,
                Size = AdaptiveTextSize.Medium,
                Weight = AdaptiveTextWeight.Lighter,
                Wrap = true
            });

            columns.Columns.Add(firstColumn);
            columns.Columns.Add(secondColumn);
            adaptiveCard.Body.Add(columns);

            return AdaptiveCardBuilder.CreateAdaptiveCardFromJson(adaptiveCard.ToJson());
        }
    }
}
