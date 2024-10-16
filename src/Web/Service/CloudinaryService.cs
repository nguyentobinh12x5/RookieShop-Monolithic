using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace Web.Service
{
    public class CloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IConfiguration config)
        {
            var account = new Account(
                config["Cloudinary:CloudName"],
                config["Cloudinary:ApiKey"],
                config["Cloudinary:ApiSecret"]
            );
            _cloudinary = new Cloudinary(account);
        }

        public async Task<List<ImageUploadResult>> UploadListImagesAsync(List<Stream> imageStreams, List<string> fileNames)
        {
            var uploadResults = new List<ImageUploadResult>();

            for (int i = 0; i < imageStreams.Count; i++)
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(fileNames[i], imageStreams[i]),
                    PublicId = $"uploads/{fileNames[i]}",
                    Overwrite = true,
                    UseFilename = true
                };

                var result = await _cloudinary.UploadAsync(uploadParams);
                uploadResults.Add(result);
            }

            return uploadResults;
        }

        public async Task<ImageUploadResult> UploadImageAsync(Stream imageStream, string fileName)
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(fileName, imageStream),
                PublicId = $"uploads/categories/{fileName}",
                Overwrite = true,
                UseFilename = true
            };

            var result = await _cloudinary.UploadAsync(uploadParams);
            return result;
        }

        public async Task<bool> DeleteImageAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);
            return result.Result == "ok";
        }

    }
}
