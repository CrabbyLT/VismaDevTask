using System;

namespace VismaDevTask
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Visma book library :) How can I help you?");
            Console.WriteLine("To list all the commands, enter 'help'");

            var input = Console.ReadLine();

            if (input.ToLower().Equals("help"))
            {
                Console.WriteLine("Not implemented! Closing..");
                Environment.Exit(0);
            }
            else Environment.Exit(0);
        }
    }
}
