﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tesseracts.DMS.Common
{
    public interface IDocumentLogic
    {
        bool SaveFile(List<FileData> fileUploadData);

        FileData DownloadFile(string fileId);
    }
}
