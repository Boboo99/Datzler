using System;
using System.Drawing;
using ScottPlot;

namespace src
{
    class Plotter 
    {
        private ScottPlot.Plot _plot = new Plot();

        public Plotter(double xMin,double xMax,double yMin, double yMax)
        {
            _plot.Axis(xMin,xMax,yMin,yMax);
        }
        public void Plot(IPlottable plottable, Color c,string fileName, int smoothing = 1)  //stateful i know
        { 
            if(plottable.AsPlottable()[0].Length == 0)
                return;

            plottable.ApplySmoothing(smoothing);

            _plot.PlotScatter(plottable.AsPlottable()[0],plottable.AsPlottable()[1],color: c, lineWidth: 0);
            _plot.SaveFig(fileName + ".png");

            return;
        }

        
    }
}