using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CopyDirectory.Infrastructure
{
    public interface IProcess
    {

        event EventHandler<string> FileInCopy;
        Task<Core.FileCopy.Responce> Execute(Core.FileCopy.Resquest _params);
        bool IsPermissionExist(string fullPath);
        bool IsValidPath(string fullPath);


    }
}
