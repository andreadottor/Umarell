namespace Dottor.Umarell.Server.Controllers
{
    using Dottor.Umarell.Server.Models;
    using Dottor.Umarell.Shared;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.StaticFiles;
    using Microsoft.EntityFrameworkCore;

    [Route("api/v1/[controller]")]
    [ApiController]
    public class BuildingSitesController : ControllerBase
    {
        private readonly string _filesFolder;
        private readonly UmarellContext _db;
        private readonly ILogger<BuildingSitesController> _logger;

        public BuildingSitesController(IConfiguration configuration, UmarellContext db, ILogger<BuildingSitesController> logger)
        {
            _filesFolder = configuration.GetValue<string>("FilesFolder");
            _db = db;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = new ApiResult<IEnumerable<BuildingSiteModel>>()
            {
                Result = true
            };

            try
            {
                var sites = await _db.BuildingSites.ToListAsync();
                var list = new List<BuildingSiteModel>();
                foreach (var item in sites)
                {
                    list.Add(new()
                    {
                        FileName = item.FileName,
                        Id = item.Id,
                        Latitude = item.Latitude,
                        Longitude = item.Longitude,
                        StartDate = item.StartDate,
                        Title = item.Title
                    });
                }
                result.Data = list;
                result.Result = true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on retrieve BuildingSite list", ex);
            }
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Insert(BuildingSiteInsertModel model)
        {
            var result = new ApiResult()
            {
                Result = false
            };
            try
            {
                BuildingSite buildingSite = new()
                {
                    Id = Guid.NewGuid(),
                    Title = model.Title,
                    FileName = model.FileName,
                    Latitude = model.Latitude,
                    Longitude = model.Longitude,
                    StartDate = model.StartDate
                };

                if (!string.IsNullOrWhiteSpace(model.FileName))
                {
                    var provider = new FileExtensionContentTypeProvider();
                    string contentType;
                    if (!provider.TryGetContentType(model.FileName, out contentType))
                    {
                        contentType = "application/octet-stream";
                    }
                    buildingSite.FileContentType = contentType;

                    var folderPath = Path.Combine(_filesFolder, buildingSite.Id.ToString());
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);

                    var path = Path.Combine(_filesFolder, buildingSite.Id.ToString(), model.FileName);
                    await System.IO.File.WriteAllBytesAsync(path, model.FileContent);
                    buildingSite.FilePath = path;
                }

                _db.BuildingSites.Add(buildingSite);
                await _db.SaveChangesAsync();

                result.Result = true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error on Insert BuildingSite", ex);
            }
            return Ok(result);
        }

        [HttpGet("Image/{buildingSiteId}")]
        public async Task<IActionResult> Image(Guid buildingSiteId)
        {
            if (await _db.BuildingSites.FindAsync(buildingSiteId) is BuildingSite buildingSite)
            {
                if (!string.IsNullOrWhiteSpace(buildingSite.FilePath))
                {
                    var image = System.IO.File.OpenRead(buildingSite.FilePath);
                    return File(image, buildingSite.FileContentType);
                }
            }

            return NotFound();
        }
    }
}
