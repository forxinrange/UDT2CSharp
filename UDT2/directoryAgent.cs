using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Collections;
using System.Security.Policy;
using System.Windows;

namespace ComptuerDetails
{
    class directoryAgent
    {

        public DirectoryEntry ldapConnection;
        public DirectorySearcher directorySearcher;
        public static MainWindow WPF;

        public directoryAgent(MainWindow wpfWin)
        {
            WPF = wpfWin;
            //ldapConnection = new DirectoryEntry(domainName);
            //ldapConnection.Path = "LDAP://OU=UCLAN,DC=ntds,D=uclan,DC=ac,DC=uk";
            //ldapConnection.AuthenticationType = AuthenticationTypes.Secure;
            //directorySearcher = new DirectorySearcher(ldapConnection);
        }

        public Hashtable returnProperty(string username, string domain)
        {
            //WPF.updateLabel(WPF.statusLabel, "in the return property method");
            Hashtable userProps = new Hashtable();
            try
            {
                
                PrincipalContext ctx = new PrincipalContext(ContextType.Domain, domain);
                //WPF.updateLabel(WPF.statusLabel, "built the principle context");
                UserPrincipal userP = UserPrincipal.FindByIdentity(ctx, username);
                //WPF.updateLabel(WPF.statusLabel, "built the user principle");

                userProps.Add("HomeDirectory", userP.HomeDirectory.ToString());
                userProps.Add("DistinguishedName", userP.DistinguishedName.ToString());


                //WPF.updateLabel(WPF.statusLabel, "returning the hashtable");

                return userProps;
            }
            catch(Exception e)
            {
                userProps.Add("HomeDirectory", "notfound");
                userProps.Add("DistinguishedName", "notfound");
                return userProps;
            }
            
        }


    }
}
