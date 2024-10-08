﻿namespace FileClient
{
    public class UploadFileRequest : IRequest
    {
        public string RequestType { get; set; }
        public string FileName { get; set; }
        public byte[] FileData { get; set; }

        public UploadFileRequest(string fileName, byte[] fileData)
        {
            RequestType = "UploadFile";
            FileName = fileName;
            FileData = fileData;
        }
    }
}
