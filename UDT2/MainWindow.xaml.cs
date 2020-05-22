using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using LoadingIndicators;
using LoadingIndicators.WPF;
using System.Diagnostics;

namespace ComptuerDetails
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // Class for getting host details (i.e hostname)
        hostInfo hInfo;
        Thread chkThread;
        checkLib checkCore;

        public MainWindow()
        {
            InitializeComponent();
            if(Environment.GetCommandLineArgs().Contains("/s"))
            {
                App.Current.MainWindow.Hide();
            }
            killDuplicateProcess();
            initControls();
            populateDeviceInfo();
            checkStart();
            //MainPane.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        public void killDuplicateProcess()
        {
            Process thisApp = Process.GetCurrentProcess();
            Process[] dupe = (Process.GetProcessesByName(thisApp.ProcessName));
            if(dupe.Length > 1)
            {
                foreach(Process p in dupe)
                {
                    if(p.Id != thisApp.Id)
                    {
                        p.Kill();
                    }
                }
            }
        }

        public void initControls()
        {
            LI1.IsActive = false;
            LI2.IsActive = false;
            LI3.IsActive = false;
            phaseOneImg.Visibility = System.Windows.Visibility.Hidden;
            phaseTwoImg.Visibility = System.Windows.Visibility.Hidden;
            phaseThreeImg.Visibility = System.Windows.Visibility.Hidden;
            changeImageSource("pack://application:,,,/Resources/tick.png", phaseOneImg);
            changeImageSource("pack://application:,,,/Resources/tick.png", phaseTwoImg);
            changeImageSource("pack://application:,,,/Resources/tick.png", phaseThreeImg);
        }

        public void populateDeviceInfo()
        {
            hInfo = new hostInfo();

            // Populate computer name label
            cnValue.Content = hInfo.getMachineName();
            usrVal.Content = hInfo.getUsername();
            uuidVal.Content = hInfo.getUUID();
            officeVal.Content = hInfo.getOfficeVersion();
            osVal.Content = hInfo.getOsName();
            ipVal.Content = hInfo.getIp();
            macVal.Content = hInfo.getMacAddress();

        }

        public void updateLabel(Label targetLabel, string updateText)
        {
            this.Dispatcher.Invoke(() =>
            {
                targetLabel.Content = updateText;
            });
        }

        public void toggleLI(LoadingIndicator LI)
        {
            this.Dispatcher.Invoke(() =>
            {
                LI.IsActive = !LI.IsActive;
            });
        }

        public void toggleBtnVisible(Button targetBtn)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (targetBtn.Visibility == System.Windows.Visibility.Visible)
                {
                    targetBtn.Visibility = System.Windows.Visibility.Hidden;
                }
                else
                {
                    targetBtn.Visibility = System.Windows.Visibility.Visible;
                }
            });
        }

        public void toggleImgVisibilty(Image targetImg)
        {
            this.Dispatcher.Invoke(() =>
            {
                if(targetImg.Visibility == System.Windows.Visibility.Visible)
                {
                    targetImg.Visibility = System.Windows.Visibility.Hidden;
                }
                else
                {
                    targetImg.Visibility = System.Windows.Visibility.Visible;
                }
            });
        }

        public void changeImageSource(string uriPath, Image targetImg)
        {

            this.Dispatcher.Invoke(() =>
            {
                Uri uri = new Uri(uriPath);
                BitmapImage bitmap = new BitmapImage(uri);
                targetImg.Source = bitmap;
            });

        }

        public void critialError(int phase)
        {
            if(phase == 1)
            {
                toggleLI(LI1);
                changeImageSource("pack://application:,,,/Resources/cross.png", phaseOneImg);
                toggleImgVisibilty(phaseOneImg);

                changeImageSource("pack://application:,,,/Resources/cross.png", phaseTwoImg);
                toggleImgVisibilty(phaseTwoImg);

                changeImageSource("pack://application:,,,/Resources/cross.png", phaseThreeImg);
                toggleImgVisibilty(phaseThreeImg);
            }
            else if(phase == 2)
            {
                toggleLI(LI2);
                changeImageSource("pack://application:,,,/Resources/cross.png", phaseTwoImg);
                toggleImgVisibilty(phaseTwoImg);

                changeImageSource("pack://application:,,,/Resources/cross.png", phaseThreeImg);
                toggleImgVisibilty(phaseThreeImg);
            }
            else if(phase == 3)
            {
                toggleLI(LI3);
                changeImageSource("pack://application:,,,/Resources/cross.png", phaseThreeImg);
                toggleImgVisibilty(phaseThreeImg);
            }

            if(phase == 2)
            {
                Console.WriteLine("Shutdown option code here");
            }

            if (Environment.GetCommandLineArgs().Contains("/s"))
            {
                Environment.Exit(1);
            }

        }

        public void checkProc()
        {
            // Hide check button
            toggleBtnVisible(chkBtn);

            // Show loading indicator
            toggleLI(LI1);

            // Set status label
            updateLabel(statusLabel, "Running checks...");

            //  Phase one
            checkCore = new checkLib(this);
            if (checkCore.phaseOneProc() == true)
            {
                toggleLI(LI1);
                toggleImgVisibilty(phaseOneImg);
                toggleLI(LI2);
                if (checkCore.phaseTwoProc())
                {
                    toggleLI(LI2);
                    toggleImgVisibilty(phaseTwoImg);
                    toggleLI(LI3);
                    if (checkCore.phaseThreeProc())
                    {
                        toggleLI(LI3);
                        toggleImgVisibilty(phaseThreeImg);
                        if (Environment.GetCommandLineArgs().Contains("/s"))
                        {
                            
                            Environment.Exit(10);
                        }

                        updateLabel(statusLabel, "All services connected and ready for use.");
                    }
                    else
                    {
                        critialError(3);
                        //updateLabel(statusLabel, "Unable to map network drives.  Please contact LIS customer support.");
                    }
                }
                else
                {
                    critialError(2);
                    updateLabel(statusLabel, "Unable to connect to UCLan services.  Please restart your device or contact LIS customer support.");
                }
            }
            else
            {
                critialError(1);
                updateLabel(statusLabel, "Unable to connect to the internet. Please check your internet connection.");
            }

            // re-enable check button
            toggleBtnVisible(chkBtn);
        }

        public void checkStart() {

            chkThread = new Thread(new ThreadStart(checkProc));
            chkThread.Start();

        }

        // Launch help url
        private void helpBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://uclan.topdesk.net/tas/public/");
        }


        // Exit button | gracefully close all processes
        private void exiBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void chkBtn_Click(object sender, RoutedEventArgs e)
        {
            initControls();
            checkStart();
            populateDeviceInfo();
        }
    }
}