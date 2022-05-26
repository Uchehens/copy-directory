using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CopyDirectory.Infrastructure
{
    public class Process: IProcess
    {

        public event EventHandler<string> FileInCopy;
        public  async Task<Core.FileCopy.Responce> Execute(Core.FileCopy.Resquest _params)
        {

            try
            {

                //Get all directory in source folder
                var sourceDir = new DirectoryInfo(_params.sourcePath);
                //Create destination direcotr
                if (!Directory.Exists(_params.destinationPath))
                {
                    Directory.CreateDirectory(_params.destinationPath);
                }
                //get all file in directory
                FileInfo[] files = sourceDir.GetFiles();
                foreach(var f in files)
                {
                    f.CopyTo(Path.Combine(_params.destinationPath, f.Name),true);
                    OnFileInCopy(f.Name);
                }

                //get all directory
                DirectoryInfo[] dirs = sourceDir.GetDirectories();
                foreach(DirectoryInfo di in dirs)
                {
                    string d = Path.Combine(_params.destinationPath, di.Name);
                    // copy the new diretory
                    await Execute(new Core.FileCopy.Resquest
                    {
                        destinationPath = d,
                        sourcePath = di.FullName
                    });
                }
                return new Core.FileCopy.Responce
                {
                    message = "Successfull",
                    status = true
                };
            }
            catch (Exception Ex)
            {
                //log exception 
                //
                return new Core.FileCopy.Responce
                {
                    message = Ex.InnerException.ToString(),
                    status = false
                };
            }

        }

        protected virtual void OnFileInCopy(string filename)
        {
            FileInCopy?.Invoke(this, filename);
        }



        public bool IsPermissionExist(string fullPath)
        {
            try
            {
                //test if user has permission to  write on the destination 
                string tempFile = Path.Combine(fullPath, "testfile.txt");
                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(fullPath);
                }
                File.Create(tempFile).Close();
                File.Delete(tempFile);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }


        public bool IsValidPath(string fullPath)
        {
            //check if path exist....
            return Directory.Exists(fullPath);
        }




    }
}
