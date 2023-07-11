using Amazon;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3;
using CommonService.Constants;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using CommonService.Enums;

namespace CommonService.Helpers
{
    public static class Helper
    {
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.APSouth1;
       
        public static string MoveFileToS3Server(EnumImageType imageType,int UploadId,string tempImagePath)
        {
            string finalPath = string.Empty;
            try
            {
                if (File.Exists(tempImagePath))
                {
                    string movingImagePath =string.Format( GetFileUploadDetails(imageType),UploadId);
                    string fileName = Path.GetFileName(tempImagePath);
                    finalPath = string.Concat(movingImagePath, fileName);
                    UploadToS3(tempImagePath, movingImagePath, fileName);
                   
                }
            }
            catch (Exception ex)
            { }

            return finalPath;
        }
        private static void UploadToS3(string filepath, string folder, string filename)
        {
            string bucketName = AccessAppSettings.config.GetSection("S3Details:BucketName").Value;
            string S3Key = AccessAppSettings.config.GetSection("S3Details:S3Key").Value;
            string S3Secret = AccessAppSettings.config.GetSection("S3Details:S3Secret").Value;
            var s3Client = new AmazonS3Client(S3Key, S3Secret, bucketRegion);

            var fileTransferUtility = new TransferUtility(s3Client);
            try
            {
                PutObjectRequest request = new PutObjectRequest()
                {
                    BucketName = bucketName,
                    Key = folder // <-- in S3 key represents a path  
                };

                s3Client.PutObjectAsync(request);
                 
                string path = folder + filename;
                var fileTransferUtilityRequest = new TransferUtilityUploadRequest
                {
                    BucketName = bucketName + "/" + folder,
                    FilePath = filepath,
                    StorageClass = S3StorageClass.StandardInfrequentAccess,
                    PartSize = 6291456, // 6 MB.  
                    Key = filename,
                    CannedACL = S3CannedACL.PublicRead
                }; 
                fileTransferUtility.Upload(fileTransferUtilityRequest);
                fileTransferUtility.Dispose();

            }

            catch (Exception ex)
            {
            }
            finally
            {
                s3Client.Dispose();
            }

        }

        private static string GetFileUploadDetails(EnumImageType uploadType)
        {
            string uploadPath = string.Empty;
            switch (uploadType)
            {
                case EnumImageType.CompanyLogo:
                    uploadPath = CommonConstants.CompanyLogo;
                    break;
                default:
                    uploadPath = string.Empty;
                    break;

            }
            return uploadPath;
        }
    }
}

