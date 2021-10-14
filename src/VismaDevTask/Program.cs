using System;
using VismaDevTask.Handlers;

namespace VismaDevTask
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Visma book library :) How can I help you?");
            Console.WriteLine("To list all the commands, enter 'help'");


            while (true)
            {
                var input = Console.ReadLine();
                if (input == "exit")
                {
                    Console.WriteLine("Exitting the program. Have a nice day :)");
                    Environment.Exit(0);
                }
                else
                {
                    ErrorHandler.Handle(() => InputHandler.Handle(input));
                }
            }
        }
    }
}
