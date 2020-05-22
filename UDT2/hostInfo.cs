using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows.Documents;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Collections;

namespace ComptuerDetails
{
    class hostInfo
    {

        // wmi objects
        public ManagementObjectSearcher wmiSearcher;
        public ManagementScope mScope;
        public ObjectQuery wmiQuery;

        // Hive object
        public RegistryKey HKLM;

        // Reg key object
        public RegistryKey regKey;

        public string getUUID()
        {
            string cName = "localhost";
            mScope = new ManagementScope(String.Format("\\\\{0}\\root\\CIMV2", cName), null);
            mScope.Connect();
            wmiQuery = new ObjectQuery("SELECT UUID FROM Win32_ComputerSystemProduct");
            wmiSearcher = new ManagementObjectSearcher(mScope, wmiQuery);

            foreach(ManagementObject wmiObj in wmiSearcher.Get())
            {
                return wmiObj["UUID"].ToString();
            }

            return "no UUID found";
        }

        public string getMacAddress()
        {
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if ((nic.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet) && nic.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in nic.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            string processedMac = Regex.Replace(nic.GetPhysicalAddress().ToString(), ".{2}", "$0:");
                            return processedMac.Remove(processedMac.Length - 1, 1);
                        }
                    }
                }
            }
            return "no MAC found";
        }

        public string getIp()
        {
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if((nic.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet) && nic.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in nic.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            return ip.Address.ToString();
                        }
                    }
                }
            }
            return "No IP found";
        }

        public string getDomainUsername()
        {
            return System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString();
        }

        public string getUsername()
        {
            return Environment.UserName.ToString();
        }

        public string getMachineName()
        {
            return System.Environment.MachineName.ToString();
            
        }

        public string getMachineRegKeyValue(string path, string valueName)
        {
            try
            {
                HKLM = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                regKey = HKLM.OpenSubKey(path);
                Console.WriteLine(regKey);
                if (regKey != null)
                {
                    object rVal = regKey.GetValue(valueName);
                    if (rVal != null)
                    {
                        return rVal.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return "default";
        }

        public string getOsName()
        {
            string productName = getMachineRegKeyValue("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", "ProductName");
            string releaseID = getMachineRegKeyValue("SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", "ReleaseId");
            return (productName + " " + releaseID);
        }

        public string getOfficeVersion()
        {
            HKLM = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            regKey = HKLM.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall");
            foreach(string key in regKey.GetSubKeyNames())
            {
                if (key.Contains("O365ProPlusRetail"))
                {

                    string keyConcat = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\" + key;
                    Console.WriteLine(keyConcat);
                    return getMachineRegKeyValue(keyConcat, "DisplayVersion");
                }
            }

            return "Office not installed.";
        }
    }
}
