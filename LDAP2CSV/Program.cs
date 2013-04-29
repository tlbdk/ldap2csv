using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LDAPUtils;
using System.DirectoryServices.Protocols;
using System.Collections;

// Links: http://www.codeproject.com/Articles/34468/Talk-to-Sun-One-LDAP-with-NET-DirectoryServices

namespace LDAP2CSV
{
    class Program
    {
        static void Main(string[] args)
        {
            var uri = new Uri("LDAP://localhost:50000/CN=Users,CN=Users,CN=external,CN=production,DC=apmoller,DC=net");
            var conn = LDAPAccount.LdapConnectBind(uri, "CN=admin,CN=Users,CN=Users,CN=external,CN=production,DC=apmoller,DC=net", "admin");

            var bindDN = uri.LocalPath.Substring(1);

            var searchreq = new SearchRequest(bindDN, "(objectClass=*)", SearchScope.OneLevel);

            var results = (SearchResponse)conn.SendRequest(searchreq);
            Console.WriteLine("Found {0} entries", results.Entries.Count);

            foreach (SearchResultEntry item in results.Entries)
            {
                Console.WriteLine(item.DistinguishedName);
                foreach (DictionaryEntry entry in item.Attributes)
                {
                    var attibute = (DirectoryAttribute)entry.Value;
                    foreach (DirectoryAttribute attibutevalue in attibute) {
                        Console.WriteLine("{0}: {1}", entry.Key, attibutevalue);
                    }
                        
                }
            }

            Console.ReadKey();
        }
    }
}
 