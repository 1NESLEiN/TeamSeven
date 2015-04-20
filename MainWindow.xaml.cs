﻿using System.Windows;
using DevelopmentProject.PeterGUI.Pages;
using Model;

namespace DevelopmentProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       /// <summary>
       /// Content visibility property from Contentvisibility class
       /// </summary>
        public ContentVisibility ContentVisibility { get; set; }
        /// <summary>
        /// Mainwindow constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            ContentVisibility = new ContentVisibility();
            NavigationDockPanel.DataContext = ContentVisibility;
            ManageSupportersButton.DataContext = ContentVisibility;

            Login login = new Login(ContentVisibility);
            MainWondowFrame.Content = login;
            Title = "Login";
        }
        /// <summary>
        /// Open AddDocumentationPage method
        /// </summary>
        private void AddDocumentation_Click(object sender, RoutedEventArgs e)
        {
            AddDocumentationPage addDocumentationPage = new AddDocumentationPage();
            MainWondowFrame.Content = addDocumentationPage;
            Title = "Add Documentation";
        }
        /// <summary>
        /// Open AddSupporterPage method
        /// </summary>
        private void AddSupporter_Click(object sender, RoutedEventArgs e)
        {
            AddSupporterPage addSupporterPage = new AddSupporterPage();
            MainWondowFrame.Content = addSupporterPage;
            Title = "Manage Supporters";
        }
        /// <summary>
        /// Open SearchDocumentationPage method
        /// </summary>
        private void SearchDocumentation_Click(object sender, RoutedEventArgs e)
        {
            SearchDocumentationPage searchDocumentationPage = new SearchDocumentationPage(ContentVisibility);
            MainWondowFrame.Content = searchDocumentationPage;
            Title = "Search Documentation";
        }
        /// <summary>
        /// logout method
        /// </summary>
        private void Logout_OnClick(object sender, RoutedEventArgs e)
        {
            //Opens up login page
            Login login = new Login(ContentVisibility);
            MainWondowFrame.Content = login;
            Title = "Login";

            //resets the login visibility property
            var contentVisibility = NavigationDockPanel.DataContext as ContentVisibility;
            var vis = contentVisibility != null && contentVisibility.LoginVisibility;

            var visibility = NavigationDockPanel.DataContext as ContentVisibility;
            if (visibility != null)
                visibility.LoginVisibility = !vis;

            //resets the user visibility property
            var contentVisibility2 = ManageSupportersButton.DataContext as ContentVisibility;
            var vis2 = contentVisibility2 != null && contentVisibility2.UserVisibility;

            var visibility2 = ManageSupportersButton.DataContext as ContentVisibility;
            if (visibility2 != null && visibility2.UserVisibility)
                visibility2.UserVisibility = !vis2;
        }
    }
}
