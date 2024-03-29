﻿using Amazon;
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
        public static long _loginUserid { get; set; }

        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.APSouth1;
       
        public static string MoveFileToS3Server(EnumImageType imageType,long UploadId,string tempImagePath)
        {
            string finalPath = string.Empty;
            try
            {
                tempImagePath = CommonConstants.FileTempPath + tempImagePath;
                if (File.Exists(tempImagePath))
                {
                    string movingImagePath =string.Format( GetFileUploadDetails(imageType),UploadId).Replace("/Content", "Content");
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
                case EnumImageType.BrandLogo:
                    uploadPath = CommonConstants.BrandLogo;
                    break;
                case EnumImageType.ProductLogo:
                    uploadPath = CommonConstants.ProductLogo;
                    break;
                case EnumImageType.ServiceLogo:
                    uploadPath = CommonConstants.ServiceLogo;
                    break;
                case EnumImageType.BannerEng:
                    uploadPath = CommonConstants.BannerEng;
                    break;
                case EnumImageType.BannerArb:
                    uploadPath = CommonConstants.BannerArb;
                    break;
                case EnumImageType.GalleryImage:
                    uploadPath = CommonConstants.GalleryImage;
                    break;
                case EnumImageType.GalleryFile:
                    uploadPath = CommonConstants.GalleryFile;
                    break;
                case EnumImageType.OfferImage:
                    uploadPath = CommonConstants.OfferImages;
                    break;
                case EnumImageType.TeamPicture:
                    uploadPath = CommonConstants.CompanyTeam;
                    break;
                case EnumImageType.AwardFile:
                    uploadPath = CommonConstants.AwardFile;
                    break;
                case EnumImageType.NewsImage:
                    uploadPath = CommonConstants.NewsImage;
                    break; 
                case EnumImageType.EventImage:
                    uploadPath = CommonConstants.EventImage;
                    break;
                case EnumImageType.FreeListingLogo:
                    uploadPath = CommonConstants.FreeListingImage;
                    break;
                default:
                    uploadPath = string.Empty;
                    break;

            }
            return uploadPath;
        }
    }
}

