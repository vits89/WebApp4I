using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;
using AutoMapper;
using WebApp4I.AppCore.Entities;
using WebApp4I.AppCore.Interfaces;
using WebApp4I.WebApp.Infrastructure;
using WebApp4I.WebApp.Models;
using WebApp4I.WebApp.ViewModels;

namespace WebApp4I.WebApp.Controllers
{
    public class ImagesController : ApiController
    {
        private readonly IImageInfoRepository _imageInfoRepository;
        private readonly IImageFileProcessor _imageFileProcessor;
        private readonly IImageMetadataFileReader _imageMetadataFileReader;
        private readonly IMapper _mapper;

        public ImagesController(IImageInfoRepository imageInfoRepository, IImageFileProcessor imageFileProcessor,
            IImageMetadataFileReader imageMetadataFileReader, IMapper mapper)
        {
            _imageInfoRepository = imageInfoRepository;
            _imageFileProcessor = imageFileProcessor;
            _imageMetadataFileReader = imageMetadataFileReader;
            _mapper = mapper;
        }

        [HttpPost]
        [MultipartContent]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Save([ModelBinder]IEnumerable<PostedFileModel> files)
        {
            var imageInfo = new List<ImageInfo> { Capacity = files.Count() };

            foreach (var file in files)
            {
                var fileContent = file.Content.ToArray();

                var newFileName = await _imageFileProcessor.SaveAsync(fileContent, file.Name);
                var thumbnailFileName = await _imageFileProcessor.CreateThumbnailAsync(fileContent, file.Name);

                imageInfo.Add(new ImageInfo
                {
                    FileName = newFileName,
                    ThumbnailFileName = thumbnailFileName
                });
            }

            await _imageInfoRepository.AddAsync(imageInfo);

            return StatusCode(HttpStatusCode.NoContent);
        }

        [ResponseType(typeof(ImageInfoViewModel))]
        public async Task<IHttpActionResult> Get(int id)
        {
            var imageInfo = await _imageInfoRepository.GetAsync(id);

            if (imageInfo != null)
            {
                var viewModel = _mapper.Map<ImageInfoViewModel>(imageInfo);

                viewModel.Metadata = _imageMetadataFileReader.Read(imageInfo.FileName);

                return Ok(viewModel);
            }

            return NotFound();
        }

        public async Task<IEnumerable<ImageThumbnailInfoViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<ImageThumbnailInfoViewModel>>(await _imageInfoRepository.GetAllAsync());
        }

        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Update(int id, UpdateImageInfoViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var imageInfo = await _imageInfoRepository.GetAsync(id);

            if (imageInfo != null)
            {
                imageInfo.Description = viewModel.Description;

                await _imageInfoRepository.UpdateAsync(imageInfo);

                return Ok();
            }

            return NotFound();
        }
    }
}
