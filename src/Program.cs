using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ScottPlot;



namespace src
{
    class DataPoint
    {
        public double X { get;}
        public double Y { get;}

        public DataPoint(double x, double y) 
        {
            X = x;
            Y = y;
        }

    }

    class DataVector
    {
        private List<DataPoint> _list = new List<DataPoint>();

        private double[] xs;
        private double[] ys;
        public DataVector(double[] x, double[] y)
        {
            if(x.Length != y.Length)
                throw new Exception("Each X value has to have a corresponding Y value");

            for(int i = 0;i<x.Length;i++)
            {
                _list.Add(new DataPoint(x[i],y[i])); //ayy stateful but fuck it. 
            }

            xs = x;
            ys = y;
        }

        public DataVector(List<DataPoint> vdp) 
        {
            _list = vdp;
            xs = vdp.Select(dp => dp.X).ToArray<double>(); 
            ys = vdp.Select(dp => dp.Y).ToArray<double>();
        }


        public void Plot(string fileName) 
        {
            if(_list.Count == 0) 
                return;
            var plot = new ScottPlot.Plot();
            plot.PlotScatter(xs,ys,lineWidth: 0);
            plot.SaveFig(fileName + ".png");
        }

        public DataVector GetAnomaliesFor(double maxRaise)
        {
            //The idea is quite simple:
            //figure out the raise between two points if it greater than the allowed raise it's part of the Anomalies
            //so raise between dv0 and dv1, the  raise between dv1 and dv2 ...

            List<DataPoint> anomalies = new List<DataPoint>();

            double raise(DataPoint p1,DataPoint p2) 
            {
                return (p2.Y - p1.Y) / (p2.X - p1.X);
            }


            for(int i = 0;i<_list.Count - 1;i++)
            {
                if(Math.Abs(raise(_list[i],_list[i + 1])) > Math.Abs(maxRaise))
                {
                    if(raise(_list[i],_list[i + 1]) > 0)
                        anomalies.Add(_list[i + 1]);
                    anomalies.Add(_list[i]);
                }
            }

            return new DataVector(anomalies);

        }
    }
    class Program
    {
           static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var v = new DataVector(new double[] {1,2,3,4,5,6,7,8,9,10}, new double[] {1.3,1.2,1.5,1.8,1.2,1.5,1.3,1.4,1.5,1.6});

            v.Plot("v");

            v.GetAnomaliesFor(0.4).Plot("anomalies");

        }



    }
}
