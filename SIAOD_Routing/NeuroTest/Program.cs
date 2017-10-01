using System;
using Encog.Neural.Networks;
using Encog.Neural.Networks.Layers;
using Encog.Engine.Network.Activation;
using Encog.ML.Data;
using Encog.Neural.Networks.Training.Propagation.Resilient;
using Encog.ML.Train;
using Encog.ML.Data.Basic;
using Encog;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace encog_sample_csharp
{
    internal class Program
    {
        /// <summary>
        /// Input for the XOR function.
        /// </summary>
        public static double[][] XORInput =
        {
            new[] {0.0, 0.0},
            new[] {1.0, 0.0},
            new[] {0.0, 1.0},
            new[] {1.0, 1.0}
        };

        /// <summary>
        /// Ideal output for the XOR function.
        /// </summary>
        public static double[][] XORIdeal =
        {
            new[] {0.0},
            new[] {1.0},
            new[] {1.0},
            new[] {0.0}
        };
        private static double[][] readFile()
        {
            List<double[]> inputData = new List<double[]>();
            //List<double> result = new List<double>();
            string path = @"C:/Users/user/Source/Repos/NewRepo/SIAOD_Routing/NeuroTest/resource/deads.txt";
            try
            {
                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
                {
                    string line;

                    while ((line = sr.ReadLine()) != null)
                    {
                            inputData.Add(line.Split(' ', '\t')
                            .Select(x => Double.Parse(x))
                            .ToArray());
                    }
                    var inputDataWithoutResult = inputData
                        .Select(d => d
                            .Where(x => x != inputData[0].Length-1)
                            .ToArray())
                        .ToArray();
                    return inputDataWithoutResult;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
        private static double InterpolationLinear(double minOut, double maxOut,double minIn, double maxIn, double value)
        {
            return (minOut + ((value - minIn) / (maxIn - minIn)) * (maxOut - minOut) / 1);
        }
        private static double[][] OptimizeRange(double[][] srcData,double min,double max)
        {
            //var linesTuples = srcData.Select(x => Tuple.Create(x, x.Min(), x.Max())).ToArray();
            double maxOfColumn;
            double minOfColumn;

            for (int i = 0; i < srcData[0].Length; i++)
            {
                maxOfColumn = double.MinValue;
                minOfColumn = double.MaxValue;
                for (int j = 0; j < srcData.Length; j++)
                {
                    if (maxOfColumn < srcData[j][i])
                        maxOfColumn = srcData[j][i];
                    if (minOfColumn > srcData[j][i])
                        minOfColumn = srcData[j][i];
                }
                for (int j = 0; j < srcData[i].Length; j++)
                {
                    srcData[j][i] = InterpolationLinear(min, max, minOfColumn, maxOfColumn, srcData[j][i]);
                }
            }
            
            //srcData = linesTuples.Select(x=>InterpolationLinear(1,0,x.Item3,x.Item2,x.Item1))
            return srcData;
        }
        private static void Main(string[] args)
        {
            double[][] lines = readFile();
           
            lines = OptimizeRange(lines, 0, 1);
            // create a neural network, without using a factory
            var network = new BasicNetwork();
            network.AddLayer(new BasicLayer(null, true, 2));
            network.AddLayer(new BasicLayer(new ActivationSigmoid(), true, 7));
            network.AddLayer(new BasicLayer(new ActivationSigmoid(), false, 1));
            
            network.Structure.FinalizeStructure();
            network.Reset();

            // create training data
            IMLDataSet trainingSet = new BasicMLDataSet(XORInput, XORIdeal);

            // train the neural network
            IMLTrain train = new ResilientPropagation(network, trainingSet);

            int epoch = 1;

            do
            {
                train.Iteration();
                Console.WriteLine(@"Epoch #" + epoch + @" Error:" + train.Error);
                epoch++;
            } while (train.Error > 0.01);

            train.FinishTraining();

            // test the neural network
            Console.WriteLine(@"Neural Network Results:");
            foreach (IMLDataPair pair in trainingSet)
            {
                IMLData output = network.Compute(pair.Input);
                Console.WriteLine(pair.Input[0] + @"," + pair.Input[1]
                                  + @", actual=" + output[0] + @",ideal=" + pair.Ideal[0]);
            }
            Console.Read();
            EncogFramework.Instance.Shutdown();
        }
    }
}