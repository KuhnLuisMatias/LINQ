using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Windows.Threading;


namespace Tareas.Paralelismo
{
    internal class Program
    {
        private static object s_logLock = new object();
        private static BufferBlock<string> s_buffer = new BufferBlock<string>();
        static void Main(string[] args)
        {
            /*
                Info:
                    The Parallel class waits for the tasks it created, but it doesn’t wait for other background.
            
            int[] numeros = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 50 };

            ParallelFor();
            Console.WriteLine($"El valor encontrado en la posicion {SearchNumber(numeros,4)}");

            StopParallelForEarly();
            ParallelForWithInit();
            ParallelForWithInit_II();
            ParallelForEach();

            ParallelInvoke();
            TaskMethod(s_logLock);
            TasksUsingThreadPool();
            RunSynchronousTask();
            LongRunningTask();
            TaskWithResultDemo();
            ContinuationTasks();


            Task t1 = Task.Run(() => Producer());
            Task t2 = Task.Run(async () => await ConsumerAsync());
            Task.WaitAll(t1, t2);
            
            
            var target = SetupPipeline();
            target.Post("C:\\Users\\Hogar\\source\\repos\\CSharp7\\Tareas.Paralelismo");
            Console.ReadLine();
            
            DispatcherTimerInitialize();
             */


            RaceConditions();

            Console.ReadKey();
        }
        public static void Log_II(string prefix) => Console.WriteLine($"{prefix}\t, Tarea: {Task.CurrentId}, " + $"Hilo: {Thread.CurrentThread.ManagedThreadId}");
        public static void ParallelFor()
        {
            ParallelLoopResult result =
            Parallel.For(0, 10, new ParallelOptions { MaxDegreeOfParallelism = 2 }, i =>
            {
                Log($"Start {i}");
                Task.Delay(10);
                Log($"End {i}");
            });
            Console.WriteLine($"Is completed: {result.IsCompleted}");
        }
        public static int SearchNumber(int[] numbers, int target)
        {
            int foundIndex = -1;
            object lockObject = new object();
            Parallel.For(1, numbers.Length, (int i, ParallelLoopState pls) =>
            {
                Log($"recorriendo > {i}");
                if (numbers[i] == target)
                {
                    lock (lockObject)
                    {
                        if (foundIndex == -1)
                        {
                            foundIndex = i;
                        }
                    }
                }

            });
            return foundIndex;
        }
        public static void StopParallelForEarly()
        {
            ParallelLoopResult result =
            Parallel.For(0, 10, (int i, ParallelLoopState pls) =>
            {
                Log($"S {i}");
                if (i > 5)
                {
                    pls.Break();
                    Log($"break now... {i}");
                }
                Task.Delay(10).Wait();
                Log($"E {i}");
            });
            Console.WriteLine($"Is completed: {result.IsCompleted}");
            Console.WriteLine($"lowest break iteration: {result.LowestBreakIteration}");
        }
        public static void ParallelForWithInit()
        {
            /*  
                En este ejemplo, se está utilizando un bucle Parallel.For<long>() para iterar a través de un arreglo de enteros llamado data. 
                El método For<TLocal>() tiene cuatro parámetros: 

                > el primero es el índice inicial, 
                > el segundo es el índice final,
                > el tercero es una función de inicialización de una variable local, 
                > el cuarto es una función de iteración 
                > y el quinto es una función de agregación.

                La función de inicialización es una función que se llama una vez por hilo para inicializar una variable local. 
                En este caso, se está inicializando una variable local llamada subtotal en 0.
                La función de iteración se llama para cada iteración del bucle. 
                En este caso, se está sumando el valor actual del arreglo data a la variable local subtotal y devolviendo el valor de subtotal.
                La función de agregación se llama una vez por hilo para combinar los resultados de las variables locales. 
                En este caso, se está utilizando el método Interlocked.Add() para agregar el valor de subtotal a la variable total.
                Finalmente, se imprime el resultado total en la consola.
            */

            int[] data = Enumerable.Range(0, 100).ToArray();
            long total = 0;

            Parallel.For<long>(0, data.Length, () => 0, (i, loop, subtotal) =>
            {
                subtotal += data[i];
                return subtotal;
            },
            (x) => Interlocked.Add(ref total, x));

            Console.WriteLine("Total: " + total);
        }
        public static void ParallelForWithInit_II()
        {
            Parallel.For<string>(0, 10, () =>
            {
                // invoked once for each thread
                Log($"init thread");
                return $"t{Thread.CurrentThread.ManagedThreadId}";
            },
            (i, pls, str1) =>
            {
                // invoked for each member
                Log($"body i {i} str1 {str1}");
                Task.Delay(10).Wait();
                return $"i {i}";
            },
            (str1) =>
            {
                // final action on each thread
                Log($"finally {str1}");
            });
        }
        public static void ParallelForEach()
        {
            string[] data = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve" };
            ParallelLoopResult result =
            Parallel.ForEach<string>(data, (s, pls, l) =>
            {
                if (l > 5)
                {
                    pls.Break();
                    Console.WriteLine("Alto");
                }
                Console.WriteLine($"{s} {l}");
            });
        }
        public static void ParallelInvoke()
        {
            Parallel.Invoke(Foo, Bar);
        }
        public static void Foo() => Console.WriteLine("foo");
        public static void Bar() => Console.WriteLine("bar");
        public static void TaskMethod(object o)
        {
            Log(o?.ToString());
        }
        public static void Log(string title)
        {
            lock (s_logLock)
            {
                Console.WriteLine(title);
                Console.WriteLine($"Task id: {Task.CurrentId?.ToString() ?? "no task"}, " + $"thread: {Thread.CurrentThread.ManagedThreadId}");
                Console.WriteLine($"is pooled thread: " + $"{Thread.CurrentThread.IsThreadPoolThread}");
                Console.WriteLine($"is background thread: " + $"{Thread.CurrentThread.IsBackground}");
                Console.WriteLine();
            }
        }
        public static void TasksUsingThreadPool()
        {
            var tf = new TaskFactory();
            Task t1 = tf.StartNew(TaskMethod, "using a task factory");
            Task t2 = Task.Factory.StartNew(TaskMethod, "factory via a task");
            var t3 = new Task(TaskMethod, "using a task constructor and Start");
            t3.Start();
            Task t4 = Task.Run(() => TaskMethod("using the Run method"));
        }
        private static void RunSynchronousTask()
        {
            TaskMethod("just the main thread");
            var t1 = new Task(TaskMethod, "run sync");
            t1.RunSynchronously();
        }
        private static void LongRunningTask()
        {
            var t1 = new Task(TaskMethod, "long running",
            TaskCreationOptions.LongRunning);
            t1.Start();
        }
        public static (int Result, int Remainder) TaskWithResult(object division)
        {
            (int x, int y) = ((int x, int y))division;
            int result = x / y;
            int remainder = x % y;
            Console.WriteLine("task creates a result...");
            return (result, remainder);
        }
        public static void TaskWithResultDemo()
        {
            /*
                Este código muestra cómo se puede utilizar una tarea para devolver un resultado al completarse, 
                utilizando la clase Task<TResult> y un método con cualquier tipo de valor de retorno. 
                La propiedad Result de la tarea bloquea y espera hasta que se complete la tarea antes de devolver el resultado.
            */
            var t1 = new Task<(int Result, int Remainder)>(TaskWithResult, (8, 3));
            t1.Start();
            Console.WriteLine($"El resultado es la tupla: {t1.Result}");
            t1.Wait();
            Console.WriteLine($"result from task: {t1.Result.Result} " + $"{t1.Result.Remainder}");
        }
        private static void DoOnFirst()
        {
            Console.WriteLine($"1. Tarea Padre ID:{Task.CurrentId}");
            Task.Delay(3000).Wait();
        }
        private static void DoOnSecond(Task t)
        {
            Console.WriteLine($"\n2. Tarea Padre: {t.Id} finalizada.");
            Console.WriteLine($"3. Nueva Tarea ID:{Task.CurrentId}");
            Console.WriteLine("4. Limpiando..");
            Task.Delay(3000).Wait();
        }
        public static void ContinuationTasks()
        {
            Task t1 = new Task(DoOnFirst);
            Task t2 = t1.ContinueWith(DoOnSecond);
            Task t3 = t1.ContinueWith(DoOnSecond);
            Task t4 = t2.ContinueWith(DoOnSecond);
            Task t5 = t1.ContinueWith(DoOnError, TaskContinuationOptions.OnlyOnRanToCompletion);
            t1.Start();
        }
        private static void DoOnError(Task obj)
        {
            Console.WriteLine("Error pibe.");
        }

        public static async Task ConsumerAsync()
        {
            while (true)
            {
                string data = await s_buffer.ReceiveAsync();
                Console.WriteLine($"user input: {data}");
            }
        }
        public static void Producer()
        {
            bool exit = false;
            while (!exit)
            {
                string input = Console.ReadLine();
                if (string.Compare(input, "exit", ignoreCase: true) == 0)
                {
                    exit = true;
                }
                else
                {
                    s_buffer.Post(input);
                }
            }
        }

        #region Pipeline
        /*
            Este código contiene 4 métodos que forman una pipeline de procesamiento de archivos. 
            El método principal es SetupPipeline(), el cual se encarga de configurar y enlazar los diferentes bloques de transformación y acción.
        */

        public static IEnumerable<string> GetFileNames(string path)
        {
            /*
            El primer método, GetFileNames(), 
            toma una ruta de directorio como entrada y utiliza el método EnumerateFiles() de la clase Directory
            para recorrer todos los archivos con extensión ".cs" en ese directorio y devuelve una colección de nombres de archivos como salida.
            */
            foreach (var fileName in Directory.EnumerateFiles(path, "*.cs"))
            {
                yield return fileName;
            }
        }

        public static IEnumerable<string> LoadLines(IEnumerable<string> fileNames)
        {
            /*
            El siguiente método, LoadLines(), toma una colección de nombres de archivos como entrada y utiliza un bucle para abrir cada archivo, 
            leer línea por línea con un StreamReader y devolver cada línea como salida.
            */
            foreach (var fileName in fileNames)
            {
                using (FileStream stream = File.OpenRead(fileName))
                {
                    var reader = new StreamReader(stream);
                    string line = null;
                    while ((line = reader.ReadLine()) != null)
                    {
                        //WriteLine($"LoadLines {line}");
                        yield return line;
                    }
                }
            }
        }

        public static IEnumerable<string> GetWords(IEnumerable<string> lines)
        {
            /*
            El tercer método, GetWords(), toma una colección de líneas como entrada y utiliza el método Split()
            para dividir cada línea en palabras. Luego utiliza un bucle interno para devolver cada palabra válida(no vacía o nula) como salida.
            */
            foreach (var line in lines)
            {
                string[] words = line.Split(' ', ';', '(', ')', '{', '}', '.', ',');
                foreach (var word in words)
                {
                    if (!string.IsNullOrEmpty(word))
                        yield return word;
                }
            }
        }
        public static ITargetBlock<string> SetupPipeline()
        {
            /*
            El último método, SetupPipeline(), crea 4 bloques: TransformBlock y ActionBlock y los enlaza. 
            El primer TransformBlock llamado "fileNamesForPath" utiliza el método GetFileNames(), 
            el segundo TransformBlock llamado "lines" utiliza el método LoadLines(), 
            el tercer TransformBlock llamado "words" utiliza el método GetWords() y 
            el último ActionBlock llamado "display" se encarga de mostrar en consola.

            Cada bloque se encarga de una tarea específica y se enlaza al siguiente bloque para que la salida del primer bloque 
            sea la entrada del siguiente y así sucesivamente. El método SetupPipeline() devuelve el primer bloque, "fileNamesForPath", 
            para que pueda ser utilizado para iniciar la pipeline.
            */
            var fileNamesForPath = new TransformBlock<string, IEnumerable<string>>(path => GetFileNames(path));
            var lines = new TransformBlock<IEnumerable<string>, IEnumerable<string>>(fileNames => LoadLines(fileNames));
            var words = new TransformBlock<IEnumerable<string>, IEnumerable<string>>(lines2 => GetWords(lines2));
            var display = new ActionBlock<IEnumerable<string>>(coll =>
            {
                foreach (var s in coll)
                {
                    Console.WriteLine(s);
                }
            });
            fileNamesForPath.LinkTo(lines);
            lines.LinkTo(words);
            words.LinkTo(display);
            return fileNamesForPath;
        }

        #endregion

        #region DispatcherTime

        public static void DispatcherTimerInitialize()
        {
            DispatcherTimer _timer = new DispatcherTimer();
            _timer.Tick += Timer_Tick;
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Start();

            Console.ReadLine();
        }

        public static void Timer_Tick(object sender, object e)
        {
            Console.WriteLine("Tick!");
        }

        #endregion

        #region Condicion De Carrera
        /*
         * .Este código contiene varias clases y un método que ilustran un problema conocido como "condición de carrera" en el desarrollo de software.

        La clase StateObject tiene una variable de estado _state con un valor inicial de 5 y un objeto de sincronización sync. 
        El método ChangeState se utiliza para cambiar el valor de _state y comprobar si se ha producido una condición de carrera.

        La clase SampleTask tiene un método llamado RaceCondition que toma un objeto como parámetro. 
        Utiliza la instrucción Trace.Assert para asegurar que el objeto es del tipo StateObject. 
        Luego, se crea un bucle infinito donde se llama al método ChangeState de StateObject pasando el número de iteración actual.

        El método RaceConditions crea una instancia de StateObject y ejecuta dos tareas utilizando Task.Run, 
        cada una de las cuales ejecuta el método RaceCondition en SampleTask pasando la instancia de StateObject.

        El problema que se está tratando de ilustrar aquí es que cuando dos tareas acceden a la misma instancia de StateObject al mismo tiempo
        y llaman al método ChangeState sin un mecanismo de sincronización adecuado, es posible que se produzca una "condición de carrera". 
        Esto significa que el valor de _state puede cambiar de forma impredecible y no se puede garantizar que se cumpla la lógica esperada 
        en el método ChangeState.

        Para solucionar esto, se puede utilizar la instrucción lock para bloquear el objeto de sincronización sync 
        durante la ejecución del método ChangeState. 
        De esta manera, solo una tarea puede acceder a StateObject a la vez, 
        lo que garantiza que el valor de _state no cambie de forma impredecible.
        */
        public class StateObject
        {
            private int _state = 5;
            private object sync = new object();
            public void ChangeState(int loop)
            {
                //  lock (sync)
                //{
                if (_state == 5)
                {
                    _state++;
                    if (_state != 6)
                    {
                        Console.WriteLine($"Race condition occurred after {loop} loops");
                        Trace.Fail("race condition");
                    }
                }
                _state = 5;
                //    }
            }
        }

        public class SampleTask
        {
            public void RaceCondition(object o)
            {
                Trace.Assert(o is StateObject, "o must be of type StateObject");
                StateObject state = o as StateObject;
                int i = 0;
                while (true)
                {
                    //lock (state) // no race condition with this lock
                    //{
                    state.ChangeState(i++);
                    //}
                }
            }
        }
        public static void RaceConditions()
        {
            var state = new StateObject();
            for (int i = 0; i < 2; i++)
            {
                Task.Run(() => new SampleTask().RaceCondition(state));
            }
        }
        #endregion
    }
}
