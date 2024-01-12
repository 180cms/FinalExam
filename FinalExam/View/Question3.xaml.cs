using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Newtonsoft.Json;

namespace FinalExam.View
{
    public partial class Question3 : ContentPage
    {
        public class PostRecord
        {
            [JsonProperty("id")]
            public int Id { get; set; }

            [JsonProperty("userId")] // Adjusted property name to match the JSON data
            public string UserId { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("body")]
            public string Body { get; set; }
        }

        private const string ApiUrl = "https://jsonplaceholder.typicode.com/posts";
        private ObservableCollection<PostRecord> posts;

        public Question3()
        {
            InitializeComponent();
            posts = new ObservableCollection<PostRecord>();
            postsCollectionView.ItemsSource = posts;

            // Call the method to fetch and display data
            Task.Run(LoadData); // Run asynchronously on a background thread
        }

        private async Task LoadData()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    // Make a GET request to the API
                    var response = await httpClient.GetStringAsync(ApiUrl);

                    // Deserialize the JSON response into a collection of PostRecord objects
                    var result = JsonConvert.DeserializeObject<ObservableCollection<PostRecord>>(response);

                    // Update the ObservableCollection with the retrieved data
                    foreach (var post in result)
                    {
                        posts.Add(post);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        private async void SaveData(string json)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    // Make a PUT request to update the API with the new data
                    await httpClient.PutAsync(ApiUrl, new StringContent(json, System.Text.Encoding.UTF8, "application/json"));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        private void OnSaveClicked(object sender, EventArgs e)
        {
            try
            {
                // Create a new PostRecord with data from the Entry fields
                var newPost = new PostRecord
                {
                    Title = titleEntry.Text,
                    Body = bodyEntry.Text
                };

                // Add the new post to the ObservableCollection
                posts.Add(newPost);

                // Serialize the ObservableCollection to JSON
                var json = JsonConvert.SerializeObject(posts);

                // Save the updated data to the API
                SaveData(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is PostRecord post)
            {
                var result = await DisplayAlert("Delete Confirmation", $"Are you sure you want to delete?", "Yes", "No");

                if (result)
                {
                    posts.Remove(post);
                }
            }
        }
    }
}
