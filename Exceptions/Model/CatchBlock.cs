using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions.Model
{
    public static class CatchBlock
    {
        public static void Example()
        {
            while (true)
            {
                try
                {
                    string userInput;
                    Console.Write("Input a number between 0 and 5 " + "(or just hit return to exit)> ");
                    userInput = Console.ReadLine();
                    if (string.IsNullOrEmpty(userInput))
                    {
                        break;
                    }
                    int index = Convert.ToInt32(userInput);
                    if (index < 0 || index > 5)
                    {
                        throw new IndexOutOfRangeException($"You typed in {userInput}");
                    }
                    Console.WriteLine($"Your number was {index}");
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine("Exception: " + $"Number should be between 0 and 5. {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An exception was thrown. Message was: " + $"{ex.Message}");
                }
                finally
                {
                    Console.WriteLine("Thank you\n");
                }
            }
        }

        public static void ThrowWithErrorCode(int code)
        {
            throw new MyCustomException("Error in Foo") { ErrorCode = code };
        }

        public static void Execute()
        {
            try
            {
                ThrowWithErrorCode(405);
            }
            catch (MyCustomException ex) when (ex.ErrorCode == 405)
            {
                Console.WriteLine($"Exception caught with filter {ex.Message} " +
                $"and {ex.ErrorCode}");
            }
            catch (MyCustomException ex)
            {
                Console.WriteLine($"Exception caught {ex.Message} and {ex.ErrorCode}");
            }
        }

#line 100
        public static void HandleAll()
        {
            var methods = new Action[]
            {
                HandleAndThrowAgain,
                HandleAndThrowWithInnerException,
                HandleAndRethrow,
                HandleWithFilter
            };
            foreach (var m in methods)
            {
                try
                {
                    m(); // line 114
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine($"\tInner Exception{ex.Message}");
                        Console.WriteLine(ex.InnerException.StackTrace);
                    }
                    Console.WriteLine();
                }
            }
        }

#line 8000
        public static void ThrowAnException(string message)
        {
            throw new MyCustomException(message); // line 8002
        }

#line 4000
        public static void HandleAndThrowAgain()
        {
            try
            {
                ThrowAnException("test 1");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Log exception {ex.Message} and throw again");
                throw ex; // you shouldn't do that - line 4009
            }
        }

#line 3000
        public static void HandleAndThrowWithInnerException()
        {
            try
            {
                ThrowAnException("test 2"); // line 3004
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Log exception {ex.Message} and throw again");
                throw new AnotherCustomException("throw with inner exception", ex); // 3009
            }
        }

#line 2000
        public static void HandleAndRethrow()
        {
            try
            {
                ThrowAnException("test 3");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Log exception {ex.Message} and rethrow");
                throw; // line 2009
            }
        }

#line 1000
        public static void HandleWithFilter()
        {
            try
            {
                ThrowAnException("test 4"); // line 1004
            }
            catch (Exception ex) when (Filter(ex))
            {
                Console.WriteLine("block never invoked");
            }
        }
#line 1500
        public static bool Filter(Exception ex)
        {
            Console.WriteLine($"just log {ex.Message}");
            return false;
        }
    }
}
