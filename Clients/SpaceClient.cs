using Newtonsoft.Json;
using SpaceAPI;
using SpaceAPI.Models;
namespace SpaceAPI.Clients
{
    public class SpaceClient
    {
        private HttpClient _client;
        private static string _adress;
        private static string _apikey;
        private static string _apihost;
        public SpaceClient()
        {
            _client = new HttpClient();
            _adress = Constants.Adress;
            _apikey = Constants.ApiKey;
            _apihost = Constants.ApiHost;
        }
        public async Task<APODSpacePhoto> GetPhoto()
        {
            HttpResponseMessage response = await _client.GetAsync($"{_adress}{_apihost}?api_key={_apikey}");
            string json = await response.Content.ReadAsStringAsync();
            APODSpacePhoto photo = JsonConvert.DeserializeObject<APODSpacePhoto>(json);
            
            return photo;
        }
        public async Task<APODSpacePhoto> GetRandomPhoto()
        {
            HttpResponseMessage response = await _client.GetAsync($"{_adress}{_apihost}?api_key={_apikey}&count=1");
            string json = await response.Content.ReadAsStringAsync();
            List<APODSpacePhoto> photos = JsonConvert.DeserializeObject<List<APODSpacePhoto>>(json);

            return photos[0];
        }
        public async Task<APODSpacePhoto> GetPhotoByDate(string date)
        {
            HttpResponseMessage response = await _client.GetAsync($"{_adress}{_apihost}?api_key={_apikey}&date={date}");
            string json = await response.Content.ReadAsStringAsync();
            APODSpacePhoto photo = JsonConvert.DeserializeObject<APODSpacePhoto>(json);

            return photo;
        }
    }
}
