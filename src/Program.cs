using System;
using System.Drawing;

namespace src
{    class Program
    {
           static void Main(string[] args)
        {
            Console.WriteLine("Trying to plot and analyze the data...");

            var v = new DataGenerator().GenerateDataVector(250,1); //new DataVector(new double[] {1,2,3,4,5,6,7,8,9,10}, new double[] {1.3,1.2,1.5,1.8,1.2,1.5,1.3,1.4,1.5,1.6});

            var plotter = new Plotter(0,250,-0.25,0.25);

            plotter.Plot(v,Color.Green,"vector",30);
            plotter.Plot(v.GetAnomaliesByRaise(0.3),Color.Red,"anomalies",30);

            Console.WriteLine("Analyzed and plotted the data!");
        }



    }
}
