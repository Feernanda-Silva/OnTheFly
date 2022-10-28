using Newtonsoft.Json;
using Passenger_API.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Passenger_API.Service
{
    public class AddressService
    {
        public AddressService() { }
        public async Task<Address> GetAdress(string cep)
        {
            Address address;
            using (HttpClient _adressClient = new HttpClient())
            {
                HttpResponseMessage response = await _adressClient.GetAsync("https://viacep.com.br/ws/" + cep + "/json/");
                var addressJson = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                    return address = JsonConvert.DeserializeObject<Address>(addressJson);
                else
                    return null;
            }
        }
    }
}
