using System;

namespace VismaDevTask.Handlers
{
    static class ErrorHandler
    {
        public static void Handle(Action func)
        {
            try
            {
                func();
            }
            catch (Exception e)
            {
                Console.WriteLine("Uh oh, it seems that something happened with the process. Here is the message:");
                Console.WriteLine(e.Message);
            }
        }
    }
}
