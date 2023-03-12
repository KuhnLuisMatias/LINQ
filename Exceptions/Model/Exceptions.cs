using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions.Model
{
    public static class ColdCallFileReaderLoop
    {
        public static void Execute(string filename)
        {
            var peopleToRing = new ColdCallFileReader();
            try
            {
                peopleToRing.Open(filename);
                for (int i = 0; i < peopleToRing.NPeopleToRing; i++)
                {
                    peopleToRing.ProcessNextPerson();
                }
                Console.WriteLine("All callers processed correctly");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"The file {filename} does not exist");
            }
            catch (ColdCallFileFormatException ex)
            {
                Console.WriteLine($"The file {filename} appears to have been corrupted");
                Console.WriteLine($"Details of problem are: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception was: {ex.InnerException.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred:\n{ex.Message}");
            }
            finally
            {
                peopleToRing.Dispose();
            }
        }
    }
}

