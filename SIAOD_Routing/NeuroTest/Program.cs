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
using System.Drawing;

namespace encog_sample_csharp
{
    class Program
    {
        static ConsoleColor ClosestConsoleColor(byte r, byte g, byte b)
        {
            ConsoleColor ret = 0;
            double rr = r, gg = g, bb = b, delta = double.MaxValue;

            foreach (ConsoleColor cc in Enum.GetValues(typeof(ConsoleColor)))
            {
                var n = Enum.GetName(typeof(ConsoleColor), cc);
                var c = System.Drawing.Color.FromName(n == "DarkYellow" ? "Orange" : n); // bug fix
                var t = Math.Pow(c.R - rr, 2.0) + Math.Pow(c.G - gg, 2.0) + Math.Pow(c.B - bb, 2.0);
                if (t == 0.0)
                    return cc;
                if (t < delta)
                {
                    delta = t;
                    ret = cc;
                }
            }
            return ret;
        }

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

            var testInput = input.GetRange(0, 10);
            testInput.AddRange(input.GetRange(100, 35));

            const int rangeDead = 10;
            const int rangeAlive = 50;
            const int startAlive = 100;

            input.RemoveRange(0, rangeDead);
            input.RemoveRange(startAlive-rangeDead, rangeAlive);

            result = popResult(ref input);
            var testResult = popResult(ref testInput);

            input = OptimizeRange(input.ToArray(), 0, 1).ToList();
            testInput = OptimizeRange(testInput.ToArray(), 0, 1).ToList();

            var testInputArr = testInput.ToArray();

            XORInput = input.ToArray();
            XORIdeal = result.ToArray();
            // create a neural network, without using a factory
            var network = new BasicNetwork();
            network.AddLayer(new BasicLayer(null, false, input[0].Length));
            var hiddenLayerSize = 1;
            network.AddLayer(new BasicLayer(new ActivationSigmoid(), false, hiddenLayerSize));
            //network.AddLayer(new BasicLayer(new ActivationSigmoid(), true, 1));

            network.Structure.FinalizeStructure();
            network.Reset();

            // create training data
            IMLDataSet trainingSet = new BasicMLDataSet(XORInput, XORIdeal);

            IMLDataSet testSet = new BasicMLDataSet(testInputArr, testResult.ToArray());

            // train the neural network
            IMLTrain train = new Backpropagation(network, trainingSet);

            for (int epoch = 0; epoch < 3; epoch++)
            {
                for (int i = 0; i < XORInput.Length; i++)
                    train.Iteration();
                Console.WriteLine(@"Epoch #" + epoch + @" Error:" + train.Error);
            }

            train.FinishTraining();
            // test the neural network
            Console.WriteLine(@"Neural Network Results:");
            int count = 0;
            Console.WriteLine("Deads cases");
            foreach (IMLDataPair pair in testSet)
            {
                Console.ForegroundColor = ConsoleColor.White;
                if (count == rangeDead)
                {
                    Console.WriteLine("Alive cases");
                    count += startAlive - rangeDead;
                }
                    
                Console.Write(count++ + "  ");
                IMLData output = network.Compute(pair.Input);
                int color = (int)InterpolationLinear(-220, 220, 0, 1, output[0]);

                
                if (Math.Abs(color) > 60)
                {
                    int red = 0;
                    int green = 0;
                    if (color > 0)
                        red = color;
                    else
                        green = Math.Abs(color);
                    Console.ForegroundColor = ClosestConsoleColor((byte)red, (byte)green, 0);
                }
                
                Console.WriteLine(@"actual=" 
                    + output[0].ToString("0.00") 
                    + @",ideal="
                    + pair.Ideal[0]);
            }
            string weigths = null;
            var min = network.Flat.Weights.Min();
            var max = network.Flat.Weights.Max();
            var weightItems = OptimizeRange(network.Flat.Weights.Select(x => (new double[] { x })).ToArray(), -startAlive, startAlive);
            foreach (var item in network.Flat.Weights)
            {
                weigths += item.ToString() + " ";
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