﻿using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApp4I.Models
{
    public interface IImageInfoRepository
    {
        Task AddAsync(ImageInfo imageInfo);
        Task AddAsync(IEnumerable<ImageInfo> imageInfo);
        Task<ImageInfo> GetAsync(int id);
        IEnumerable<ImageInfo> GetAll();
        Task<bool> UpdateAsync(ImageInfo imageInfo);
    }
}
