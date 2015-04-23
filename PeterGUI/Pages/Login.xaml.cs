﻿using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Control;
using Model;

namespace DevelopmentProject.PeterGUI.Pages
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        #region instance members for LoginPage
        /// <summary>
        /// Handler to get instance
        /// </summary>
        private readonly Handler _handler;
        /// <summary>
        /// Datatable for a specific login
        /// </summary>
        private DataTable _login;

        private Window mainWindow;
        #endregion

        /// <summary>
        /// Constructor for the LoginPage class
        /// </summary>
        /// <param name="contentVisibility">Sets the contentvisibility for loginPage</param>
        public Login(ContentVisibility contentVisibility)
        {
            InitializeComponent();
            _handler = Handler.GetInstance();

            DataContext = contentVisibility;

            mainWindow = Application.Current.MainWindow;
        }
        /// <summary>
        /// Login method for users
        /// </summary>
        private void Login_OnClick(object sender, RoutedEventArgs e)
        {
            if (TextBoxSupporterName.Text == "" || TextBoxSupporterPass.Password == "")
            {
                MessageBox.Show("You need to type both your username and password");
            }
            else
            {
                try
                {
                    //Checks if the login method from the handler can succeed with the parameters
                    _login = _handler.Login(TextBoxSupporterName.Text, TextBoxSupporterPass.Password);

                    //Checks the amount of rows returned
                    if (_login.Rows.Count != 0)
                    {
                        //Gets the access id from the returned row
                        var accessid = _login.Rows[0].Field<int>(4);

                        //reveals admin components if user is admin
                        if (accessid == 1)
                        {
                            var contentVisibility2 = DataContext as ContentVisibility;
                            var vis2 = contentVisibility2 != null && contentVisibility2.UserVisibility;

                            var visibility2 = DataContext as ContentVisibility;
                            if (visibility2 != null && visibility2.UserVisibility != true)
                                visibility2.UserVisibility = !vis2;
                        }
                        //reveals login components
                        var contentVisibility = DataContext as ContentVisibility;
                        var vis = contentVisibility != null && contentVisibility.LoginVisibility;

                        var visibility = DataContext as ContentVisibility;
                        if (visibility != null)
                            visibility.LoginVisibility = !vis;

                        //Navigates to searchDocumentationPage
                        if (NavigationService != null)
                        {
                            NavigationService.Navigate(new SearchDocumentationPage(contentVisibility));
                            mainWindow.Title = "Search Documentation";

                        }
                    }
                    else
                    {
                        MessageBox.Show("Your Username or Password were incorrect");
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}
