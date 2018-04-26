﻿using System;
using System.Globalization;
using System.Threading.Tasks;
using DurianCode.PlacesSearchBar;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParPorApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddEventPage : ContentPage
    {
        private static readonly string ApiKey = Device.OS == TargetPlatform.iOS
            ?
            "AIzaSyA3nX3DIcGkRyA4-LnNC_BYH9DedK3GYTs"
            :
            TargetPlatform.Android != TargetPlatform.Other
                ? "AIzaSyA3nX3DIcGkRyA4-LnNC_BYH9DedK3GYTs"
                :
                TargetPlatform.WinPhone != TargetPlatform.Other
                    ? "AIzaSyA3nX3DIcGkRyA4-LnNC_BYH9DedK3GYTs"
                    : "...etc...";

        public AddEventPage()
        {
            InitializeComponent();
            search_bar.ApiKey = ApiKey;
            search_bar.Type = PlaceType.Address;
            //search_bar.Components = new Components("country:us"); // Restrict results to USA
            search_bar.PlacesRetrieved += Search_Bar_PlacesRetrieved;
            search_bar.TextChanged += Search_Bar_TextChanged;
            search_bar.MinimumSearchText = 2;
            results_list.ItemSelected += Results_List_ItemSelected;
        }

        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            //string sourceDateText = e.NewDate.Month.ToString(CultureInfo.InvariantCulture) + "/" + e.NewDate.Day.ToString(CultureInfo.InvariantCulture) + "/" + e.NewDate.Year.ToString(CultureInfo.InvariantCulture) + " " + StartTime.Time;
            var date = EventDate.Text = e.NewDate.Month + "/" + e.NewDate.Day + "/" + e.NewDate.Year;
            EventDate.Text = date;
            var pickertime = EventTime.Time;
            var dt = Convert.ToDateTime(pickertime.ToString());
            var time = dt.ToString("h:mm tt");
            Time.Text = time;
        }

        private async Task ReturnEventPage_Clicked(object sender, EventArgs e)
        {
            await Task.Delay(3000);
            await Navigation.PushAsync(new EventsPage());
            //CrossLocalNotifications.Current.Show("New Event!", "A new content has posted.", 1, UtcNow.AddSeconds(8));
        }

        private void Search_Bar_PlacesRetrieved(object sender, AutoCompleteResult result)
        {
            results_list.ItemsSource = result.AutoCompletePlaces;
            spinner.IsRunning = false;
            spinner.IsVisible = false;

            if (result.AutoCompletePlaces != null && result.AutoCompletePlaces.Count > 0)
                results_list.IsVisible = true;
        }

        private void Search_Bar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                results_list.IsVisible = false;
                spinner.IsVisible = true;
                spinner.IsRunning = true;
            }
            else
            {
                results_list.IsVisible = true;
                spinner.IsRunning = false;
                spinner.IsVisible = false;
            }
        }

        private async void Results_List_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            var prediction = (AutoCompletePrediction) e.SelectedItem;
            results_list.SelectedItem = null;

            var place = await Places.GetPlace(prediction.Place_ID, ApiKey);
            if (place != null)
            {
                selectedLocation.Text = prediction.Description;
                locPlaceId.Text = prediction.Place_ID;
                locLatitude.Text = place.Latitude.ToString(CultureInfo.InvariantCulture);
                locLongitude.Text = place.Longitude.ToString(CultureInfo.InvariantCulture);
                Task.WaitAll();
            }
            
            results_list.IsVisible = false;
        }
    }
}