using DianServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace fone_soap_app.Services
{

    public interface IDian
    {
        Task<WcfDianCustomerServicesClient> GetInstanceAsync();
        Task<DianResponse> GetStatus(string trackId);
    }


    public class Dian : IDian
    {

        public readonly string serviceUrl = "https://vpfe-hab.dian.gov.co/WcfDianCustomerServices.svc?wsdl";
        public readonly EndpointAddress endpointAddress;
        public readonly BasicHttpBinding basicHttpBinding;

        public Dian()
        {
            endpointAddress = new EndpointAddress(serviceUrl);

            basicHttpBinding =
                new BasicHttpBinding(endpointAddress.Uri.Scheme.ToLower() == "http" ?
                            BasicHttpSecurityMode.None : BasicHttpSecurityMode.Transport);

            //Please set the time accordingly, this is only for demo
            basicHttpBinding.OpenTimeout = TimeSpan.MaxValue;
            basicHttpBinding.CloseTimeout = TimeSpan.MaxValue;
            basicHttpBinding.ReceiveTimeout = TimeSpan.MaxValue;
            basicHttpBinding.SendTimeout = TimeSpan.MaxValue;
        }

        public async Task<WcfDianCustomerServicesClient> GetInstanceAsync()
        {
            return await Task.Run(() => new WcfDianCustomerServicesClient(basicHttpBinding, endpointAddress));
        }


        public async Task<DianResponse> GetStatus(string  trackId)
        {
            var client = await GetInstanceAsync();
            var response = await client.GetStatusAsync(trackId);
            return response;
        }
    }
}
