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
            Uri baseAddress = new Uri("http://localhost:8080/hello");

            using (ServiceHost host = new ServiceHost(typeof(MDWcfServiceLibrary.MonopolyDealService)))
            {
                host.Open();

                Console.WriteLine("The service is ready at {0}", baseAddress);
                Console.WriteLine("Press <Enter> to stop the service.");
                Console.ReadLine();

                // Close the ServiceHost.
                host.Close();
            }
        }
    }
}