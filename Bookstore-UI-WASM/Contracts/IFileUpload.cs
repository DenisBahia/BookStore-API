﻿using BlazorInputFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore_UI_WASM.Contracts
{
   public interface IFileUpload
    {
        public void UploadFile(IFileListEntry file, MemoryStream msFile, string picName);
        public void RemoveFile(string picName);
    }
}
