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
            result = input
                .Select(x => new double[] { x.Last() })
                .ToList();
            var Sinput = input
                .Select(x => x.ToList()).ToList();
            for (int i = 0; i < Sinput.Count(); i++)
            {
                Sinput[i].RemoveAt(Sinput[i].Count - 1);
            }
            input = Sinput
                .Select(x => x.ToArray())
                .ToList();

            input = OptimizeRange(input.ToArray(), 0, 1).ToList();

            var testInput = input.GetRange(0, 5);
            var testResult = result.GetRange(0, 5);
            testInput.AddRange(input.GetRange(105, 15));
            testResult.AddRange(result.GetRange(105, 15));

            input.RemoveRange(0, 5);
            result.RemoveRange(0, 5);
            input.RemoveRange(100, 15);
            result.RemoveRange(100, 15);

            var testInputArr = testInput.ToArray();

            XORInput = input.ToArray();
            XORIdeal = result.ToArray();
            // create a neural network, without using a factory
            var network = new BasicNetwork();
            network.AddLayer(new BasicLayer(null, true, input[0].Length));
            var hiddenLayerSize = 50;
            network.AddLayer(new BasicLayer(new ActivationSigmoid(), true, hiddenLayerSize));
            network.AddLayer(new BasicLayer(new ActivationSigmoid(), false, 1));

            network.Structure.FinalizeStructure();
            network.Reset();

            // create training data
            IMLDataSet trainingSet = new BasicMLDataSet(XORInput, XORIdeal);

            IMLDataSet testSet = new BasicMLDataSet(testInputArr, testResult.ToArray());

            // train the neural network
            IMLTrain train = new ScaledConjugateGradient(network, trainingSet);

            for (int epoch = 0; epoch < 1; epoch++)
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
                Console.WriteLine(@"actual=" + Math.Round(output[0],2) + @",ideal=" + pair.Ideal[0]);
            }
            
            Console.Read();
            EncogFramework.Instance.Shutdown();
        }
    }
}