using System;
using System.IO;

namespace BMPManip
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Out.WriteLine("Program do dekodowania pliku .bmp");

            ImageManip image = new ImageManip(_imageLocation: @"destination_of_file_to_manipualte");
            Console.WriteLine();
            
            image.bmpfh.Wyswietl();
            image.bmpih.Wyswietl();
            Console.ReadKey();
        }
    }

}
