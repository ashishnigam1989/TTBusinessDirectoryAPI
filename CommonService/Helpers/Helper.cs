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
using Amazon.Runtime;

namespace CommonService.Helpers
{
    public static class Helper
    {
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.APSouth1;
       
        public static string MoveFileToS3Server(EnumImageType imageType,long UploadId,string tempImagePath)
        {
            string finalPath = string.Empty;
            try
            {
                tempImagePath = CommonConstants.FileTempPath + tempImagePath;
                if (File.Exists(tempImagePath))
                {
                    string movingImagePath =string.Format( GetFileUploadDetails(imageType),UploadId);
                    string fileName = Path.GetFileName(tempImagePath);
                    finalPath = string.Concat(movingImagePath, fileName);
                    UploadToS3(tempImagePath, movingImagePath, fileName);
                   
                }
            }
            catch
            {
                throw;
            }

            return finalPath;
        }
        private static void UploadToS3(string filepath, string folder, string filename)
        {
            string bucketName = AccessAppSettings.config.GetSection("S3Details:BucketName").Value;
            string S3Key = AccessAppSettings.config.GetSection("S3Details:S3Key").Value;
            string S3Secret = AccessAppSettings.config.GetSection("S3Details:S3Secret").Value;
            
            try
            {
                BasicAWSCredentials credentials = new BasicAWSCredentials(S3Key, S3Secret);
                AmazonS3Client s3Client = new AmazonS3Client(credentials,bucketRegion);
                TransferUtility fileTransferUtility = new TransferUtility(s3Client);
                fileTransferUtility.Upload(filepath, bucketName, (folder+filename));
                s3Client.Dispose();
            }

            catch
            {
                throw;
            } 

        }

        public static string GetFileUploadDetails(EnumImageType uploadType)
        {
            string uploadPath = string.Empty;
            switch (uploadType)
            {
                case EnumImageType.CompanyLogo:
                    uploadPath = CommonConstants.CompanyLogo;
                    break;
                case EnumImageType.CategoryIcon:
                    uploadPath = CommonConstants.CategoryIcon;
                    break;
                default:
                    uploadPath = string.Empty;
                    break;

            }
            return uploadPath;
        }
    }
}

