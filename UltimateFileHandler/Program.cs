using System;
using System.IO;
using System.Linq;

namespace UltimateFileHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            DirSearch(Environment.CurrentDirectory);

            Console.ReadKey();
        }

        private static void DirSearch(string dir_search)
        {
            try
            {
                var pattern = "*.pdf";

                foreach (var dir in Directory.GetDirectories(dir_search))
                {
                    var found = from file in
                                    Directory.GetFiles(dir, pattern)
                                select CountPages(file);

                    Console.WriteLine($"{dir}: pages: {found.Sum()}");

                    DirSearch(dir);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static int CountPages(string path)
        {
            var file = File.ReadAllBytes(path);

            var reader = new iTextSharp.text.pdf.PdfReader(file);

            return reader.NumberOfPages;
        }
    }
}
