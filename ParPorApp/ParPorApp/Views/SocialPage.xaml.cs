﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BottomBar.XamarinForms;
using ParPorApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParPorApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SocialPage : BottomBarPage
    {
        public SocialPage()
        {

            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            //            if (Device.OS == TargetPlatform.Android)
            //                NavigationPage.SetTitleIcon(this, "ic_face.png");
            // Setting Color of selected Text and Icon
            BarTextColor = Color.FromHex("#43b05c");
            FixedMode = true;

            //Dark theme for bottom navigation bar
            //BarTheme = BottomBarPage.BarThemeTypes.DarkWithAlpha;

            //Children.Add(new HomePage() { Title = "Upcoming", Icon = "ic_home.png" });
            //Children.Add(new SchedulePage() { Title = "Events", Icon = "ic_event_black.png" });
            //Children.Add(new AccountGroupsPage() { Title = "Groups", Icon = "ic_group.png" });
            //Children.Add(new JoinTeamPage() { Title = Title = "Join", Icon = "ic_message.png" });
            //Children.Add(new UserProfilePage() { Title = "Profile", Icon = "ic_face.png" });


            //NavigationPage.SetHasNavigationBar(this, false);
        }


    }
}