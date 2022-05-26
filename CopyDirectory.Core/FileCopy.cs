using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyDirectory.Core
{
    public class FileCopy
    {

        public class Resquest
        {
            public string sourcePath { get; set; }
            public string destinationPath { get; set; }
        }


        public class Responce
        {
            public string message { get; set; }
            public bool status { get; set; }
        }

    }
}
