using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using rent.domain.Services.UploadImage;

namespace rent.infrastructure.Services.UploadImage
{
    public class UploadImage : IUploadImage
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName = string.Empty;

        public UploadImage(IAmazonS3 s3Client, string bucketName)
        {
            _s3Client = s3Client;
            _bucketName = bucketName;
        }
        public async Task Execute(MemoryStream memoryStream, string fileName, string contentType)
        {
            await EnsureBucketExistsAsync(_bucketName);

            var fileTransferUtility = new TransferUtility(_s3Client);

            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = memoryStream,
                Key = fileName,
                BucketName = _bucketName,
                ContentType = contentType,
                CannedACL = S3CannedACL.Private
            };

            await fileTransferUtility.UploadAsync(uploadRequest);
        }

        private async Task EnsureBucketExistsAsync(string bucketName)
        {
            var listBucketsResponse = await _s3Client.ListBucketsAsync();

            if (!listBucketsResponse.Buckets.Any(b => b.BucketName == bucketName))
            {
                var putBucketRequest = new PutBucketRequest
                {
                    BucketName = bucketName,
                    UseClientRegion = true
                };

                var response = await _s3Client.PutBucketAsync(putBucketRequest);
            }
        }
    }
}
