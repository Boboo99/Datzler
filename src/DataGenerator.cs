using System;
using System.Collections.Generic;
using System.Linq;

namespace src 
{
    class DataGenerator 
    {
        public DataVector GenerateDataVector(int count,double mean)
        {
            var xs = Enumerable.Range(0, count);
            var generator = new Random();
            
            var datapoints = xs.Select<int,DataPoint>(x => new DataPoint(x,mean + generator.NextDouble())).ToList();

            return new DataVector(datapoints);
        }
    }

}