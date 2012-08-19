using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;

namespace MDWcfServiceLibraryHoster
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("MDWCF Service Host");
            Uri baseAddress = new Uri("http://localhost:9079/MonopolyDeal/");
            Uri wsHttpAddress = new Uri("http://localhost:9080/MonopolyDeal/");
            Uri[] baseAddresses = new Uri[1];
            baseAddresses[0] = baseAddress;
            //ServiceHost shost = new ServiceHost(new MDWcfServiceLibrary.MonopolyDealService(), baseAddresses);
            using (ServiceHost host = new ServiceHost(typeof(MDWcfServiceLibrary.MonopolyDealService), baseAddresses))
            {
                host.AddServiceEndpoint(typeof(MDWcfServiceLibrary.IMonopolyDeal), new WSHttpBinding(), wsHttpAddress);

                    host.Open();
                Console.WriteLine("The service is ready at {0}", wsHttpAddress);
                Console.WriteLine("Press <Enter> to stop the service.");
                Console.ReadLine();

                // Close the ServiceHost.
                host.Close();
            }
        }
    }
}