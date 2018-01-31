using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CopyFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            int fileCount = 0;
            if (args == null || args.Count() < 2) {
                Console.Write("請設定要複製的來源路徑和目標路徑");
                Console.ReadKey();
            }
            string sourcePath = args[0];
            string targetPath = args[1];
            Console.Write($@"來源路徑：{sourcePath}");
            Console.Write($@"目標路徑：{targetPath}");
            if (string.IsNullOrEmpty(targetPath) || string.IsNullOrEmpty(sourcePath))
            {
                Console.Write("ERROR => 請設定要複製的來源路徑和目標路徑");
                Console.ReadKey();
                return;
            }
            if (!Directory.Exists(sourcePath) || !Directory.Exists(targetPath))
            {
                Console.Write("ERROR => 來源路徑或目標路徑不存在");
                Console.ReadKey();
                return;
            }
            try
            {
                string[] files = Directory.GetFiles(sourcePath, "*", SearchOption.AllDirectories);
                if (files == null || files.Count() <= 0)
                {
                    Console.Write("ERROR => 來源路徑檔案為空");
                    Console.ReadKey();
                    return;
                }
                Directory.CreateDirectory($@"{targetPath}\Backup_{string.Format(@"{0:yyyyMMdd}", DateTime.Now)}");
                targetPath = $@"{targetPath}\Backup_{string.Format(@"{0:yyyyMMdd}", DateTime.Now)}";
                foreach (string file in files)
                {
                    string fileName = System.IO.Path.GetFileName(file);
                    string destFile = file.Replace(sourcePath, targetPath);
                    string DirName = System.IO.Path.GetDirectoryName(destFile);
                    if (!Directory.Exists(DirName))
                    {
                        Directory.CreateDirectory(DirName);
                    }
                    File.Copy(file, destFile, true);
                    Console.WriteLine($@"複製檔案=>{destFile}");
                    fileCount++;
                }
            }
            catch (Exception ex)
            {
                Console.Write($@"Copy files ERROR => {ex.Message}");
                Console.ReadKey();
                return;
            }
            Console.Write($@"複製檔案完成，總共：{fileCount.ToString()}筆檔案");
            Console.ReadKey();
        }
    }
}
