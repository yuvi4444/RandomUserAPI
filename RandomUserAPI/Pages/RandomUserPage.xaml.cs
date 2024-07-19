using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RandomUserAPI.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RandomUserPage : ContentPage
    {
        public RandomUserPage()
        {
            InitializeComponent();
            RandomUser();
        }

        public async void RandomUser()
        {
            string APIurl = "https://randomuser.me/api/";
            using HttpClient Client = new HttpClient();

            try   // to catch exceptions if the API call fails
            {
                var RandomUserData = await Client.GetFromJsonAsync<UserDataResponse>(APIurl);   // calling the API
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (RandomUserData != null)
                    {
                        var user = RandomUserData.Results[0];
                        UserGender.Text = user.Gender;
                        UserEmail.Text = user.Email;
                        NumberOfResults.Text = Convert.ToString(RandomUserData.Info.Results);
                        UserName.Text = user.Name.First + " " + user.Name.Last;
                        UsernameTag.Text = user.Login.Username;
                        UserAge.Text = Convert.ToString(user.Dob.Age);
                    }
                });
            }
            catch (HttpRequestException Exception)  // displays the error message
            {
                await DisplayAlert("Error", $"Exception: {Exception}", "OK");
            }
            
        }

        public class UserDataResponse
        {
            public List<User> Results { get; set; }
            public Info Info {  get; set; }
        }

        public class User
        {
            public string Gender { get; set; }
            public string Email { get; set; }
            public Name Name { get; set; }
            public string Country { get; set; }
            public Login Login { get; set; }
            public Dob Dob { get; set; }

        }

        public class Name
        {
            public string First { get; set; }
            public string Last { get; set; }
        } 
        public class Login 
        { 
            public string Username { get; set; }
        }

        public class Dob
        {
            public int Age { get; set; }
        }

        public class Info
        {
            public int Results { get; set; }
        }
    }
}