using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadFile
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine($"Input the file name");
                var fileName = Console.ReadLine();
                Console.WriteLine($"Which number per line be catched?(Default:5)");
                var num = Console.ReadLine();
                if (string.IsNullOrEmpty(num) || !int.TryParse(num, out int perNumber)) {
                    perNumber = 5;
                }
                while (!fileName.Equals("Q"))
                {
                    var file = new FileInfo(fileName);
                    if (file.Exists)
                    {
                        List<string> lines = new List<string>();
                        //int lineNumber = 0;
                        int index = 0;

                        FileStream fileStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);
                        using (StreamReader sr = new StreamReader(fileStream))
                        {
                            string header = sr.ReadLine();
                            while (!sr.EndOfStream)
                            {
                                index += 1;
                                var item = sr.ReadLine();
                                #region debug
                                if ((index % 1000) == 0)
                                {
                                    Console.WriteLine($"Read line no: {index}");
                                }
                                #endregion
                                
                                if ((index % 100) == perNumber)
                                {
                                    lines.Add(item);
                                    if (lines.Count > 10000)
                                    {
                                        lines.RemoveAt(0);
                                    }
                                }
                                //lineNumber++;
                            }
                            lines.Insert(0, header);
                        }
                        Console.WriteLine($"Input the new file name");
                        var newfileName = Console.ReadLine();
                        while (string.IsNullOrEmpty(newfileName) || newfileName == fileName) {
                            Console.WriteLine($"The new file name can not be equal to source file or be Empty");
                            newfileName = Console.ReadLine();
                        }

                        if (new FileInfo(newfileName).Exists)
                        {
                            Console.WriteLine($"The new file does exists, press 'Y' to delete it ");
                            var isDelete = Console.ReadLine().Trim();
                            while (!"Y".Equals(isDelete)) {
                                Console.WriteLine($"You must press 'Y' or have no any option.....");
                                isDelete = Console.ReadLine().Trim();
                            }


                            System.IO.File.Delete(new FileInfo(newfileName).FullName);
                        }

                        using (StreamWriter sr = new StreamWriter(new FileInfo(newfileName).FullName))
                        {
                            Console.WriteLine($"It`s writing the file now. Line conut:{lines.Count()} .");

                            foreach (var line in lines)
                            {
                                sr.WriteLine(line);
                            }
                        }                        
                    }
                    else
                    {
                        Console.WriteLine($"Error:: File {fileName} does not exists, input the file name");
                    }                    
                    Console.WriteLine($"Press Q for exit");
                    fileName = Console.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw ex;
            }

        }
    }
}

