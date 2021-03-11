using System;
using System.Drawing;

namespace src
{    class Program
    {
           static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var v = new DataGenerator().GenerateDataVector(100,1); //new DataVector(new double[] {1,2,3,4,5,6,7,8,9,10}, new double[] {1.3,1.2,1.5,1.8,1.2,1.5,1.3,1.4,1.5,1.6});

            var plotter = new Plotter(0,100,-0.25,0.25);

            plotter.Plot(v,Color.Green,"vector",35);
            plotter.Plot(v.GetAnomalies(0.45),Color.Red,"anomalies",35);
        }



    }
}
