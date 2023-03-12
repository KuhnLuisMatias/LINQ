using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archivos
{
    internal class Program
    {
        /*  The File class defines static method, whereas the FileInfo class offers instance methods.   */

        public static string archivo = "Ejemplo.txt";
        public static string binFile = "data.bin";
        public static string rutaArchivo = Path.Combine(GetDocumentsFolder(), archivo);
        public static string rutaArchivoBin = Path.Combine(GetDocumentsFolder(), binFile);
        public static string directory = @"C:\Desktop\TestFiles";
        public static string zipFile = @"C:\Desktop\TestFiles.zip";
        static void Main(string[] args)
        {
            /*
            
            ObtenerPath();
            
            ObtenerInformacionDisco();
            ObtenerInformacionExistenciaArchivo();
            Console.WriteLine(GetDocumentsFolder());
            FileInformation();
            ChangeFileProperties();
            ReadingAFileLineByLine(rutaArchivo);
            WriteAFile();
            agregarLineasAlArchivo();
            DeleteDuplicateFiles(GetDocumentsFolder(), false);
            
            ReadFileUsingFileStream(rutaArchivo);
            ReadFileUsingFileStream_II(rutaArchivo);
            WriteTextFile();
            ReadFileUsingReader(rutaArchivo);

            string[] cadena = { "Hola","Como ","estas?" };
            WriteFileUsingWriter(rutaArchivo,cadena);
            WriteFileUsingBinaryWriter(rutaArchivoBin);
            ReadFileUsingBinaryReader(rutaArchivoBin);

             */
            crearArchivos();
            Console.ReadLine();
        }

        public static string GetDocumentsFolder() => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static void ObtenerInformacionDisco()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                if (drive.IsReady)
                {
                    Console.WriteLine($"Drive name: {drive.Name}");
                    Console.WriteLine($"Format: {drive.DriveFormat}");
                    Console.WriteLine($"Type: {drive.DriveType}");
                    Console.WriteLine($"Root directory: {drive.RootDirectory}");
                    Console.WriteLine($"Volume label: {drive.VolumeLabel}");
                    Console.WriteLine($"Free space: {drive.TotalFreeSpace}");
                    Console.WriteLine($"Available space: {drive.AvailableFreeSpace}");
                    Console.WriteLine($"Total size: {drive.TotalSize}");
                    Console.WriteLine();
                }
            }
        }
        public static void ObtenerPath()
        {
            //Ver otros ejemplos: VolumeSeparatorChar, DirectorySeparatorChar, AltDirectorySeparatorChar,PathSeparator

            Console.WriteLine(Path.Combine(@"D:\Projects", "ReadMe.txt"));

            string path = @"C:\Archivos de programa\Archivo.txt";
            string[] parts = path.Split(Path.VolumeSeparatorChar);
            string drive = parts[0]; // drive = "C"
            string directory = parts[1]; // directory = "Archivos de programa\Archivo.txt"

            Console.WriteLine(drive + " " + directory);
        }
        public static void crearArchivos()
        {
            const string nombreArchivo = "Ejemplo.txt";

            string rutaArchivo = Path.Combine(GetDocumentsFolder(), nombreArchivo);
            File.WriteAllText(rutaArchivo, "HOLA BARBARAAAA");

            //Copiar
            string rutaArchivoCopia = Path.Combine(GetDocumentsFolder(), "EjemploCopia.txt");
            var file = new FileInfo(rutaArchivo);
            file.CopyTo(rutaArchivoCopia);
        }
        public static void ObtenerInformacionExistenciaArchivo()
        {
            //Obtener informacion si existe el archivo
            var myFolder = new DirectoryInfo(GetDocumentsFolder());
            var nombreArchivo = Path.Combine(myFolder.FullName, archivo);
            var test = new FileInfo(nombreArchivo);
            Console.WriteLine(test.Exists);
        }
        public static void FileInformation(string fileName)
        {
            /*
            If you instead used the
            File class, the access would be slower because every access would mean a check to determine whether the
            user is allowed to get this information. With the FileInfo class, the check happens only when calling the
            constructor.
            */
            var file = new FileInfo(fileName);
            Console.WriteLine($"Name: {file.Name}");
            Console.WriteLine($"Directory: {file.DirectoryName}");
            Console.WriteLine($"Read only: {file.IsReadOnly}");
            Console.WriteLine($"Extension: {file.Extension}");
            Console.WriteLine($"Length: {file.Length}");
            Console.WriteLine($"Creation time: {file.CreationTime:F}");
            Console.WriteLine($"Access time: {file.LastAccessTime:F}");
            Console.WriteLine($"File attributes: {file.Attributes}");
        }
        public static void ChangeFileProperties()
        {
            string fileName = Path.Combine(GetDocumentsFolder(), archivo);
            var file = new FileInfo(fileName);
            if (!file.Exists)
            {
                Console.WriteLine($"Create the file {archivo} before calling this method");
                Console.WriteLine("You can do this by invoking this program with the -c argument");
                return;
            }
            Console.WriteLine($"creation time: {file.CreationTime:F}");
            file.CreationTime = new DateTime(2025, 12, 24, 15, 0, 0);
            Console.WriteLine($"creation time: {file.CreationTime:F}");
        }
        public static void ReadingAFileLineByLine(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            //IEnumerable<string> lines = File.ReadLines(fileName);
            int i = 1;
            foreach (var line in lines)
            {
                Console.WriteLine($"{i++}. {line}");
            }
        }
        public static void WriteAFile()
        {
            string[] movies =
            {
                "Snow White And The Seven Dwarfs",
                "Gone With The Wind",
                "Casablanca",
                "The Bridge On The River Kwai",
                "Some Like It Hot"
            };
            File.WriteAllLines(rutaArchivo, movies);
        }
        public static void agregarLineasAlArchivo()
        {
            string[] moreMovies =
            {
            "Psycho",
            "Easy Rider",
            "Star Wars",
            "The Matrix"
            };
            File.AppendAllLines(rutaArchivo, moreMovies);
        }
        public static void DeleteDuplicateFiles(string directory, bool checkOnly)
        {
            IEnumerable<string> fileNames = Directory.EnumerateFiles(directory, "*", SearchOption.TopDirectoryOnly);
            string previousFileName = string.Empty;
            foreach (string fileName in fileNames)
            {
                string previousName = Path.GetFileNameWithoutExtension(previousFileName);
                if (!string.IsNullOrEmpty(previousFileName) && previousName.EndsWith("copia") && fileName.StartsWith(previousFileName.Substring(0, previousFileName.LastIndexOf(" - copia"))))
                {
                    var copiedFile = new FileInfo(previousFileName);
                    var originalFile = new FileInfo(fileName);

                    if (copiedFile.Length == originalFile.Length)
                    {
                        Console.WriteLine($"delete {copiedFile.FullName}");
                        if (!checkOnly)
                        {
                            copiedFile.Delete();
                        }
                    }
                }
                previousFileName = fileName;
            }
        }

        #region FileStream
        /*La diferencia en la cantidad de memoria ocupada por diferentes tipos de codificaciones depende del conjunto de caracteres que se utilice y de la longitud del texto. 
         * Por ejemplo, en un texto en inglés, la diferencia entre UTF-8 y UTF-16 sería mínima, ya que ambos utilizan 1 byte para representar cada caracter en inglés. 
         * Sin embargo, en un texto en chino, UTF-8 utilizaría 3 bytes por caracter, mientras que UTF-16 utilizaría 2 bytes por caracter. 
         * Por lo tanto, en general, UTF-8 ocupa menos espacio que UTF-16 o UTF-32 para textos con un gran número de caracteres no latinos, 
         * pero puede ocupar más espacio que estos últimos para textos con un gran número de caracteres latinos.*/
        public static void ReadFileUsingFileStream(string fileName)
        {
            const int bufferSize = 4096;
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                ShowStreamInformation(stream);
                Encoding encoding = GetEncoding(stream);
            }
        }
        public static void ShowStreamInformation(Stream stream)
        {
            Console.WriteLine($"stream can read: {stream.CanRead}, can write: {stream.CanWrite}, can seek: {stream.CanSeek}, " + $"can timeout: {stream.CanTimeout}");
            Console.WriteLine($"length: {stream.Length}, position: {stream.Position}");
            if (stream.CanTimeout)
            {
                Console.WriteLine($"read timeout: {stream.ReadTimeout} write timeout: {stream.WriteTimeout} ");
            }
        }
        public static Encoding GetEncoding(Stream stream)
        {
            if (!stream.CanSeek) throw new ArgumentException("require a stream that can seek");
            Encoding encoding = Encoding.ASCII;
            byte[] bom = new byte[5];
            int nRead = stream.Read(bom, offset: 0, count: 5);
            if (bom[0] == 0xff && bom[1] == 0xfe && bom[2] == 0 && bom[3] == 0)
            {
                Console.WriteLine("UTF-32");
                stream.Seek(4, SeekOrigin.Begin);
                return Encoding.UTF32;
            }
            else if (bom[0] == 0xff && bom[1] == 0xfe)
            {
                Console.WriteLine("UTF-16, little endian");
                stream.Seek(2, SeekOrigin.Begin);
                return Encoding.Unicode;
            }
            else if (bom[0] == 0xfe && bom[1] == 0xff)
            {
                Console.WriteLine("UTF-16, big endian");
                stream.Seek(2, SeekOrigin.Begin);
                return Encoding.BigEndianUnicode;
            }
            else if (bom[0] == 0xef && bom[1] == 0xbb && bom[2] == 0xbf)
            {
                Console.WriteLine("UTF-8");
                stream.Seek(3, SeekOrigin.Begin);
                return Encoding.UTF8;
            }
            stream.Seek(0, SeekOrigin.Begin);
            return encoding;
        }

        public static void ReadFileUsingFileStream_II(string fileName)
        {
            const int BUFFERSIZE = 256;
            using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                ShowStreamInformation(stream);
                Encoding encoding = GetEncoding(stream);
                byte[] buffer = new byte[BUFFERSIZE];
                bool completed = false;
                do
                {
                    int nread = stream.Read(buffer, 0, BUFFERSIZE);
                    if (nread == 0) completed = true;
                    if (nread < BUFFERSIZE)
                    {
                        Array.Clear(buffer, nread, BUFFERSIZE - nread);
                    }
                    string s = encoding.GetString(buffer, 0, nread);
                    Console.WriteLine($"read {nread} bytes");
                    Console.WriteLine(s);
                } while (!completed);
            }
        }
        public static void WriteTextFile()
        {
            string tempTextFileName = Path.ChangeExtension(Path.GetTempFileName(),"txt");

            using (FileStream stream = File.OpenWrite(tempTextFileName))
            {
                stream.WriteByte(0xef);
                /*
                 * stream.WriteByte(0xef); significa que se escribe un byte específico en el stream. 
                 * En este caso, el byte escrito es 0xef. Este byte especifico es el Byte Order Mark (BOM) que indica que el archivo está codificado en UTF-8. 
                 * Al escribir este byte al inicio del archivo, se está indicando a las aplicaciones que utilicen el archivo que está codificado en UTF-8.
                 */
        stream.WriteByte(0xbb);
                /* stream.WriteByte(0xbb); escribe un byte con el valor hexadecimal 0xbb en el stream.
                 * Este valor específico se utiliza para indicar que el archivo está codificado en UTF-8,
                 * ya que los primeros tres bytes(0xef, 0xbb, 0xbf) son el "byte order mark" o marca de orden de bytes para UTF-8.
                 * Esto indica al sistema o a un programa que utilice el archivo que se está utilizando la codificación UTF-8 para representar los caracteres en el archivo de texto.*/
                
                stream.WriteByte(0xbf);
                /*stream.WriteByte(0xef);, stream.WriteByte(0xbb);, y stream.WriteByte(0xbf); 
                 * son tres llamadas consecutivas a la función WriteByte del objeto stream que escriben los valores hexadecimales 0xef, 0xbb, y 0xbf 
                 * respectivamente en el archivo. Estos valores forman parte del indicador de orden de bytes (Byte Order Mark, BOM) de UTF-8, 
                 * que es una marca de orden de bytes utilizada para indicar la codificación de un archivo de texto. 
                 * La presencia de estos valores en el inicio del archivo indica que el archivo está codificado en UTF-8.
                 * */

                byte[] preamble = Encoding.UTF8.GetPreamble();
                stream.Write(preamble, 0, preamble.Length);
                /*
                Los tres llamados a stream.WriteByte escriben los bytes correspondientes al identificador de orden de bytes (BOM, por sus siglas en inglés) para UTF-8 en el archivo. 
                El BOM es un conjunto de bytes que se utiliza para indicar la codificación de caracteres utilizada en un archivo de texto. En el caso de UTF-8, 
                el BOM se compone de los tres bytes 0xef, 0xbb y 0xbf.
                Agregar o no estos bytes al inicio del archivo es opcional, ya que UTF-8 no necesita BOM para ser leído correctamente, 
                pero algunos programas y herramientas pueden utilizarlo para detectar automáticamente la codificación de un archivo.
                La llamada byte[] preamble = Encoding.UTF8.GetPreamble();
                stream.Write(preamble, 0, preamble.Length); escribe el BOM de UTF-8 en el archivo, que es el mismo resultado de los 3 llamados anteriores.
                */
                string hello = "Hello, World!";
                byte[] buffer = Encoding.UTF8.GetBytes(hello);
                stream.Write(buffer, 0, buffer.Length);
                /*
                 * La línea "string hello = "Hello, World!";" establece una variable llamada "hello" y le asigna el valor "Hello, World!" 
                 * como una cadena de caracteres. La línea siguiente, "byte[] buffer = Encoding.UTF8.GetBytes(hello);", 
                 * utiliza el método GetBytes de la clase Encoding, específicamente la propiedad UTF8, para codificar la cadena "hello" en un arreglo de bytes. 
                 * Este arreglo de bytes representa la cadena "hello" en formato UTF-8, 
                 * que es una codificación de caracteres ampliamente utilizada que permite representar un rango más amplio de caracteres unicode. 
                 * La variable "buffer" ahora contiene la codificación en bytes de la cadena "hello".
                 
                 stream.Write(buffer, 0, buffer.Length) escribe los bytes del buffer en el archivo. 

                 * El primer parámetro es el buffer de bytes que se va a escribir en el archivo, 
                 * el segundo parámetro es el índice del primer byte del buffer que se va a escribir, 
                 * y el tercer parámetro es la cantidad de bytes que se van a escribir. En este caso, 
                 * se escriben todos los bytes del buffer (buffer.Length).*/
                Console.WriteLine($"file {stream.Name} written");
            }
        }

        public static void CopyUsingStreams(string inputFile, string outputFile)
        {
            /*
             * Este código abre dos archivos de forma segura: uno para lectura y otro para escritura. 
             * Utiliza un buffer de tamaño 4096 bytes para leer los datos del archivo de entrada y escribirlos en el archivo de salida. 
             * El ciclo while se ejecuta hasta que se haya completado la lectura del archivo de entrada, es decir, cuando se hayan leído 0 bytes. 
             * Cada vez que se llama a inputStream.Read, se guardan los datos leídos en el buffer, y luego se escriben en el archivo de salida mediante outputStream.Write. 
             * El número de bytes que se leen y escriben es igual al número de bytes que quedan en el buffer, es decir nRead.
             * */
            const int BUFFERSIZE = 4096;
            using (var inputStream = File.OpenRead(inputFile))
            using (var outputStream = File.OpenWrite(outputFile))
            {
                byte[] buffer = new byte[BUFFERSIZE];
                bool completed = false;
                do
                {
                    int nRead = inputStream.Read(buffer, 0, BUFFERSIZE);
                    if (nRead == 0) completed = true;
                    outputStream.Write(buffer, 0, nRead);
                } while (!completed);
            }
            /*El buffer se borra automáticamente cuando el bloque "using" alcanza su fin de vida y el recolector de basura de .NET se encarga de liberar la memoria asignada a ese buffer. 
             * Sin embargo, en este caso específico, el buffer se vuelve a reutilizar en cada iteración del bucle, 
             * por lo que no es necesario crear un nuevo buffer en cada iteración y la memoria se utiliza de manera eficiente.*/
        }
        public static void CopyUsingStreams2(string inputFile, string outputFile)
        {
            using (var inputStream = File.OpenRead(inputFile))
            using (var outputStream = File.OpenWrite(outputFile))
            {
                inputStream.CopyTo(outputStream);
            }
        }

        public static void ReadFileUsingReader(string fileName)
        {
            var stream = new FileStream(fileName, FileMode.Open, FileAccess.Read,FileShare.Read);
            using (var reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    Console.WriteLine(line);
                }
            }
        }
        public static void WriteFileUsingWriter(string fileName, string[] lines)
        {
            var outputStream = File.OpenWrite(fileName);
            using (var writer = new StreamWriter(outputStream))
            {
                byte[] preamble = Encoding.UTF8.GetPreamble();
                outputStream.Write(preamble, 0, preamble.Length);
                writer.Write(lines);
            }
        }
        public static void WriteFileUsingBinaryWriter(string binFile)
        {
            var outputStream = File.Create(binFile);
            using (var writer = new BinaryWriter(outputStream))
            {
                double d = 47.47;
                int i = 42;
                long l = 987654321;
                string s = "sample";
                writer.Write(d);
                writer.Write(i);
                writer.Write(l);
                writer.Write(s);
            }
        }
        public static void ReadFileUsingBinaryReader(string binFile)
        {
            var inputStream = File.Open(binFile, FileMode.Open);
            using (var reader = new BinaryReader(inputStream))
            {
                double d = reader.ReadDouble();
                int i = reader.ReadInt32();
                long l = reader.ReadInt64();
                string s = reader.ReadString();
                Console.WriteLine($"d: {d}, i: {i}, l: {l}, s: {s}");
            }
        }

        #endregion
    }
}
