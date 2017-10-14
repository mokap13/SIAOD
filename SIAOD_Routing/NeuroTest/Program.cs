using System;
using Encog.Neural.Networks;
using Encog.Neural.Networks.Layers;
using Encog.Engine.Network.Activation;
using Encog.ML.Data;
using Encog.Neural.Networks.Training.Propagation.Back;
using Encog.Neural.Networks.Training.Propagation.Manhattan;
using Encog.Neural.Networks.Training.Propagation.SGD;
using Encog.Neural.Networks.Training.Propagation.SCG;
using Encog.ML.Train;
using Encog.ML.Data.Basic;
using Encog;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace encog_sample_csharp
{
    class Program
    {
        public static double[][] XORInput;

        public static double[][] XORIdeal;
        private static List<double[]> readFile(string path)
        {
            List<double[]> inputData = new List<double[]>();
            //List<double> result = new List<double>();
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
                    return inputData;
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
                for (int j = 0; j < srcData.Length; j++)
                {
                    srcData[j][i] = InterpolationLinear(min, max, minOfColumn, maxOfColumn, srcData[j][i]);
                }
            }
            
            return srcData;
        }
        private static void Main(string[] args)
        {
            List<double[]> result = new List<double[]>();

            List<double[]> input = readFile(@"C:/Users/user/Source/Repos/NewRepo/SIAOD_Routing/NeuroTest/resource/all.txt");
            var fileSize = input.Count;


            result = popResult(ref input);

            input = OptimizeRange(input.ToArray(), 0, 1).ToList();

            const int rangeDead = 5;
            const int rangeAlive = 15;
            const int startAlive = 100;

            var testInput = input.GetRange(0, rangeDead);
            var testResult = result.GetRange(0, rangeDead);

            testInput.AddRange(input.GetRange(startAlive, rangeAlive));
            testResult.AddRange(result.GetRange(startAlive, rangeAlive));

            input.RemoveRange(0, rangeDead);
            result.RemoveRange(0, rangeDead);

            input.RemoveRange(startAlive - rangeDead, rangeAlive);
            result.RemoveRange(startAlive - rangeDead, rangeAlive);

            var testInputArr = testInput.ToArray();

            XORInput = input.ToArray();
            XORIdeal = result.ToArray();
            // create a neural network, without using a factory
            var network = new BasicNetwork();
            network.AddLayer(new BasicLayer(null, false, input[0].Length));
            //var hiddenLayerSize = 50;
            network.AddLayer(new BasicLayer(new ActivationSigmoid(), false, 1));

            network.Structure.FinalizeStructure();
            network.Reset();

            // create training data
            IMLDataSet trainingSet = new BasicMLDataSet(XORInput, XORIdeal);

            IMLDataSet testSet = new BasicMLDataSet(testInputArr, testResult.ToArray());

            // train the neural network
            IMLTrain train = new Backpropagation(network, trainingSet);

            for (int epoch = 0; epoch < 7; epoch++)
            {
                for (int i = 0; i < XORInput.Length; i++)
                    train.Iteration();
                Console.WriteLine(@"Epoch #" + epoch + @" Error:" + train.Error);
            }

            train.FinishTraining();
            // test the neural network
            Console.WriteLine(@"Neural Network Results:");
            foreach (IMLDataPair pair in testSet)
            {
                IMLData output = network.Compute(pair.Input);
                Console.WriteLine(@"actual=" + Math.Round(output[0], 2) + @",ideal=" + pair.Ideal[0]);
            }
            string weigths = null;
            var min = network.Flat.Weights.Min();
            var max = network.Flat.Weights.Max();
            var weightItems = OptimizeRange(network.Flat.Weights.Select(x => (new double[] { x })).ToArray(), -startAlive, startAlive);
            foreach (var item in weightItems)
            {
                weigths += item[0].ToString() + " ";
            }

            Console.Read();
            EncogFramework.Instance.Shutdown();
        }

        private static List<double[]> popResult(ref List<double[]> input)
        {
            List<double[]> result = input
                            .Select(x => new double[] { x.Last() })
                            .ToList();
            var withoutResult = input
.Select(x => x.ToList()).ToList();
            for (int i = 0; i < withoutResult.Count(); i++)
            {
                withoutResult[i].RemoveAt(withoutResult[i].Count - 1);
            }
            input = withoutResult
                .Select(x => x.ToArray())
                .ToList();
            return result;
        }
    }
}