using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CopyDirectory
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                var f = new StartFileCopyService(new Infrastructure.Process());
                string source = string.Empty;
                string destinatation = string.Empty;
                Console.WriteLine("Welcome to file copy programme");
                Console.WriteLine("Please enter the source directory:_");
                source = Console.ReadLine();
                while (!f.IsPathValid(source))
                {
                    Console.WriteLine("Invalid source director, Please try again:_");
                    source = Console.ReadLine();

                }
                Console.WriteLine("Good...");
                Console.WriteLine("Please enter the destination directory:_");
                destinatation = Console.ReadLine();
                while (!f.IsPermissionExist(destinatation))
                {
                    Console.WriteLine("No permission to write directory, Please provide another location:_");
                    destinatation = Console.ReadLine();
                }

                Console.WriteLine("Starting....");
                f.Start(new Core.FileCopy.Resquest
                {
                    destinationPath = destinatation,
                    sourcePath = source
                });

                Console.WriteLine("Finished....Press any key to exit");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                //log to console or external file nlog or log4net
                Console.WriteLine(ex);
                Console.ReadLine();
            }

        }



        public class StartFileCopyService
        {
            private Infrastructure.IProcess _cpService;

            public StartFileCopyService(Infrastructure.IProcess cp)
            {
                _cpService = cp;
                _cpService.FileInCopy += _cpService_FileInCopy;
            }

            private void _cpService_FileInCopy(object sender, string e)
            {

                string name = e;
                Console.WriteLine(string.Format("Copying {0}", name));
                //throw new NotImplementedException();
            }

            public void Start(Core.FileCopy.Resquest input)
            {
                //if (_cpService.IsPermissionExist(input.destinationPath))
                //{
                //    if (_cpService.IsValidPath(input.sourcePath))
                //    {
                //        _cpService.Execute(input);
                //    }
                //}

                _cpService.Execute(input);

            }

            public bool IsPathValid(string f)
            {
                return _cpService.IsValidPath(f);
            }

            public bool IsPermissionExist(string f)
            {
                return _cpService.IsPermissionExist(f);
            }
        }


    }
}
