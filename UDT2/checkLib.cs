using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Net.NetworkInformation;
using System.Diagnostics;
using DotRas;
using System.IO;
using Utilities.Network;
using System.Collections;

namespace ComptuerDetails
{
    class checkLib
    {
        public bool phaseOne;
        public bool phaseTwo;
        public bool phaseThree;
        public Hashtable driveHash = new Hashtable();
        public Hashtable adObject = new Hashtable();
        public static MainWindow WPF;
        

        public checkLib(MainWindow wpf)
        {
            WPF = wpf;
            phaseOne = false;
            phaseTwo = false;
            phaseThree = false;
        }

        public bool CheckWebStream(string url)
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead(url))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }

        }

        public bool CheckPing(string target)
        {

            try
            {
                Ping pinger = new Ping();
                PingReply pingResult = pinger.Send(target);
                if (pingResult.Status.ToString() == "Success")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception e)
            {
                return false;
            }

        }

        public int mapType(string distinguihsedName)
        {
            string[] splitName = distinguihsedName.Split(new string[] { "," }, StringSplitOptions.None);
            if(splitName.Contains("OU=FACULTIES") || splitName.Contains("OU=XB")){
                return 2;
            }
            else if(splitName.Contains("OU=SERVICES") || splitName.Contains("OU=EXT") || splitName.Contains("OU=XA"))
            {
                return 1;
            }

            // anything else return 0
            return 0;
        }

        public void setDriveHash(string nPath, int mapType)
        {
            // Always add the N drive to the path
            driveHash.Add("N:", nPath);

            if(mapType != 0)
            {
                driveHash.Add("U:", @"\\LSA-003\Share");
                driveHash.Add("W:", @"\\LSA-201\Share");
                driveHash.Add("Q:", @"\\ntds.uclan.ac.uk\Apps");
            }

            if (mapType == 1)
            {
                driveHash.Add("S:", @"\\LSA-001\Share");
                driveHash.Add("T:", @"\\LSA-002\Share");
            }
            else if(mapType == 2)
            {
                driveHash.Add("T:", @"\\LSA-001\Share");
                driveHash.Add("S:", @"\\LSA-002\Share");
            }
            
        }

        public bool mapDrive(string driveL, string uncPath, int attempts)
        {
            for(int a = 0;a < attempts; a++)
            {
                int dStat = Utilities.Network.NetworkDrive.MapNetworkDrive(uncPath, driveL, null, null);
                if (Directory.Exists(driveL))
                {
                    return true;
                }
                else
                {
                    if(a == attempts - 1)
                    {
                        WPF.updateLabel(WPF.statusLabel, dStat.ToString());
                        return false;
                    }
                }
            }
            return false;

        }

        public void dialVPN()
        {
            try
            {
                RasDialer dialer = new RasDialer();
                dialer.EntryName = "UCLan Network";
                dialer.PhoneBookPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Microsoft\Network\Connections\Pbk\rasphone.pbk");
                RasHandle connection = dialer.Dial();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }


        public bool ethernetUp()
        {
            foreach(NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if(nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet && nic.OperationalStatus == OperationalStatus.Up)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public bool nicUp(string nicName)
        {
            foreach(NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if(nic.Name.ToString() == nicName && nic.OperationalStatus == OperationalStatus.Up)
                {
                    return true;
                }
            }
            return false;
        }


        // MAIN CHECK METHODS
        public bool phaseOneProc()
        {
            WPF.updateLabel(WPF.statusLabel, "Checking internet...");
            if (CheckWebStream("http://www.google.co.uk") || CheckWebStream("http://uclan.ac.uk"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool phaseTwoProc()
        {
            WPF.updateLabel(WPF.statusLabel, "Checking UCLan services...");
            if (ethernetUp() && CheckPing("ntds.uclan.ac.uk"))
            {
                // SKIP VPN checks as on ethernet and able to ping domain
                Console.WriteLine("SKIP VPN checks, domain reachable via Ethernet");
            }
            else
            {
                if(!nicUp("UCLan Network") || !CheckPing("ntds.uclan.ac.uk"))
                {
                    for(int i = 0; i <= 2; i++)
                    {
                        dialVPN();
                        Console.WriteLine("Called proc attempt {0}", i);
                        if(nicUp("UCLan Network"))
                        {
                            break;
                        }

                        if(i == 2)
                        {
                            return false;
                        }
                    }
                }
            }

            //System.Threading.Thread.Sleep(500);
            return true;
        }

        public bool phaseThreeProc()
        {
            WPF.updateLabel(WPF.statusLabel, "Mapping network drives...");
            // Map drives!

            // AD here
            directoryAgent adSearcher = new directoryAgent(WPF);
            string[] adProperties = new string[] { "HomeDirectory", "DistinguishedName" };
            adObject = adSearcher.returnProperty(System.Environment.UserName.ToString(), "NTDS");
            
            try
            {
                setDriveHash(adObject["HomeDirectory"].ToString(), mapType(adObject["DistinguishedName"].ToString()));

                foreach (DictionaryEntry entry in driveHash)
                {
                    if (!mapDrive(entry.Key.ToString(), entry.Value.ToString(), 3))
                    {
                        WPF.updateLabel(WPF.statusLabel, ("Failed to mape drive "+entry.Key.ToString() + ": - Please contact the customer support team on LISCustomerSupport@uclan.ac.uk or press the Help button above."));
                        return false;
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            

            return true;
            
        }
    }
}
