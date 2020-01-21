using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using WebApp4I.Infrastructure;
using WebApp4I.Models;
using WebApp4I.ViewModels;

namespace WebApp4I.Controllers
{
    public class ImagesController : ApiController
    {
        private readonly AppSettings _appSettings;
        private readonly IImageInfoRepository _imageInfoRepository;
        private readonly IImageFileProcessor _imageFileProcessor;
        private readonly IImageMetadataFileReader _imageMetadataFileReader;
        private readonly IMapper _mapper;

        public ImagesController(AppSettings appSettings, IImageInfoRepository imageInfoRepository,
            IImageFileProcessor imageFileProcessor, IImageMetadataFileReader imageMetadataFileReader, IMapper mapper)
        {
            _appSettings = appSettings;
            _imageInfoRepository = imageInfoRepository;
            _imageFileProcessor = imageFileProcessor;
            _imageMetadataFileReader = imageMetadataFileReader;
            _mapper = mapper;
        }

        [HttpPost]
        [MultipartContent]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Save()
        {
            var streamProvider = new MultipartMemoryStreamProvider();

            await Request.Content.ReadAsMultipartAsync(streamProvider);

            var imageInfo = new List<ImageInfo> { Capacity = streamProvider.Contents.Count };

            foreach (var content in streamProvider.Contents)
            {
                var fileName = content.Headers.ContentDisposition.FileName.Trim('\"');

                var fileContent = await content.ReadAsByteArrayAsync();

                var newFileName = await _imageFileProcessor.SaveAsync(fileContent, fileName);
                var thumbnailFileName = await _imageFileProcessor.CreateThumbnailAsync(fileContent, fileName);

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

        public async Task<IEnumerable<ImageInfoViewModel>> GetAll()
        {
            return _mapper.Map<IEnumerable<ImageInfoViewModel>>(await _imageInfoRepository.GetAllAsync());
        }

        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Update(int id, UpdateImageInfoViewModel viewModel)
        {
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
