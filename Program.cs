using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            int index = 0;
            string dateString = null;
            DateTime today = DateTime.Today;
            try
            {
                
                using (StreamReader sr = new StreamReader("C:\\TestFolder\\MarketingDataFile.txt"))
                {  
                    char[] buf = sr.ReadToEnd().Where(c => char.IsDigit(c)).ToArray();
                    sr.Close();
                    while (index + 8 < buf.Length)
                    {
                        char[] dateChars = buf.Skip(index).Take(8).ToArray();
                        dateString = new string(dateChars);
                                               
                        if (IsValidDateTime(dateString))
                        {
                          
                            index = index + 8;
                        }
                        else
                            index++;

                    }

                    Console.WriteLine("Program Ending");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The date could not be processed:" + dateString);
                Console.WriteLine(e.Message);
            }
        }

        static bool IsValidDateTime(string dateTime)
        {
            string[] formats = { "MMddyyyy" };
            DateTime parsedDateTime;
            bool isValid = DateTime.TryParseExact(dateTime, formats, new CultureInfo("en-US"),
                                           DateTimeStyles.None, out parsedDateTime);
            if(isValid && DateTime.Compare(parsedDateTime, DateTime.Today) <= 0)
            {
                Console.WriteLine(dateTime);
            }
            return isValid;
        }
    }
    }

