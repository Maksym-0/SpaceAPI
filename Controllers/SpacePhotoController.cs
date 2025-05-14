using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SpaceAPI.Models;
using SpaceAPI.Clients;
using SpaceAPI.Database;
namespace SpaceAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SpacePhotoController : ControllerBase
    {
        private readonly ILogger<SpacePhotoController> logger;
        public SpacePhotoController(ILogger<SpacePhotoController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        [ActionName("GetPhoto")]
        public async Task<SpacePhoto> GetPhotoAsync()
        {
            SpaceClient sc = new SpaceClient();
            APODSpacePhoto APODPhoto = await sc.GetPhoto();
            SpacePhoto spacePhoto = new SpacePhoto
            {
                title = APODPhoto.title,
                explanation = APODPhoto.explanation,
                date = APODPhoto.date,
                url = APODPhoto.url
            };
            return spacePhoto;
        }

        [HttpGet]
        [ActionName("GetRandomPhoto")]
        public async Task<SpacePhoto> GetRandomPhotoAsync()
        {
            SpaceClient sc = new SpaceClient();
            APODSpacePhoto APODPhoto = await sc.GetRandomPhoto();
            SpacePhoto spacePhoto = new SpacePhoto
            {
                title = APODPhoto.title,
                explanation = APODPhoto.explanation,
                date = APODPhoto.date,
                url = APODPhoto.url
            };
            return spacePhoto;
        }
        [HttpGet]
        [ActionName("GetDatabasePhoto")]
        public async Task<List<SpacePhoto>> GetDatabasePhotoAsync()
        {
            DBPhoto dBPhoto = new DBPhoto();
            List<SpacePhoto> photos = new List<SpacePhoto>();
            photos = await dBPhoto.ReadPhotoAsync();
            return photos;
        }
        [HttpPost]
        [ActionName("PostPhotoByDate")]
        public async Task PostPhotoByDateAsync(string date)
        {
            DBPhoto dBPhoto = new DBPhoto();
            SpaceClient sc = new SpaceClient();
            APODSpacePhoto APODPhoto = await sc.GetPhotoByDate(date);
            SpacePhoto spacePhoto = new SpacePhoto
            {
                title = APODPhoto.title,
                explanation = APODPhoto.explanation,
                date = APODPhoto.date,
                url = APODPhoto.url
            };
            await dBPhoto.InsertPhotoAsync(spacePhoto);
        }
        [HttpPut]
        [ActionName("PutPhotoByDate")]
        public async Task PutPhotoByDate(string title, string explanation, string date)
        {
            DBPhoto dBPhoto = new DBPhoto();
            await dBPhoto.EditPhotoAsync(title, explanation, date);
        }
        [HttpDelete]
        [ActionName("DeletePhotoByDate")]
        public async Task DeletePhotoByDate(string date)
        {
            DBPhoto dBPhoto = new DBPhoto();
            await dBPhoto.DeletePhotoAsync(date);
        }
    }
}
