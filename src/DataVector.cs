using System;
using System.Collections.Generic;
using System.Linq;

namespace src 
{

    interface IPlottable
    {
        List<double[]> AsPlottable();
        void ApplySmoothing(int smoothing); //ayy muting the object :|
    }

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

    class DataVector : IPlottable
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

        public DataVector GetAnomalies(double maxRaise)
        {
            //The idea is quite simple:
            //figure out the raise between two points if it greater than the allowed raise it's part of the Anomalies
            //so raise between dv0 and dv1, the  raise between dv1 and dv2 ...
            //turns out it is not that simple :joy:

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
                    if(raise(_list[i],_list[i + 1]) < 0)
                        anomalies.Add(_list[i]);

                    //not enough, I have to look at t = 1, t = 2, t = 3
                    //rise(ft1,ft2) > abs && rise(ft1,ft3) > abs => anomalie
                    //remove anomaly as reference point, therefore shift x so the next rise is good again

                    
                }
            }

            return new DataVector(anomalies);
        }

        public List<double[]> AsPlottable()
        {
            return new List<double[]> { xs,ys};
        }

        public void ApplySmoothing(int smoothing)
        {
            var tmp = ys.Select(d => d/smoothing).ToArray<double>();

            ys = tmp;
        }
    }
}