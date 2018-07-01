using GP.Kernels;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MathNet.Numerics;
using Newtonsoft.Json;
using GP.AquisitionFunctions;
using GP.Result;

namespace GP.Example
{
    class Program
    {
        static void Main(string[] args)
        {

            for (int i = 0; i < 15; i++)
            {
                Run(i);
            }

            Console.WriteLine("Press any key to quit");
            Console.ReadLine();
        }

        private static void RunCmd(string cmd, string[] args)
        {
            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = @"C:\Users\pavelkoryakin\Anaconda3\python.exe";
            start.Arguments = string.Format("{0} {1}", cmd, string.Join(' ', args));
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    Console.Write(result);
                }
            }
        }

        private static void Run(int quries)
        {
            var kernel = new GaussianKernel(0.25, 1);
            var model = new Model(kernel, 0, 8, 800, ObjectiveFunction);
            var output = model.Explore(quries);

            var er = output.EstimationValues
                .Select(q => new double[] { q.Mean, q.UpperBound, q.LowerBound, q.X })
                .ToArray();

            var qr = output.QueryValues
                .Select(q => new double[] { q.X, q.FX })
                .ToArray();

            var af = output.AquisitionValues
                .Select(q => new double[] { q.X, q.FX })
                .ToArray();

            var json1 = JsonConvert.SerializeObject(er, Formatting.Indented);
            File.WriteAllText("predicted_test.json", json1);

            var json2 = JsonConvert.SerializeObject(qr, Formatting.Indented);
            File.WriteAllText("observed_test.json", json2);

            var json3 = JsonConvert.SerializeObject(af, Formatting.Indented);
            File.WriteAllText("aquisition_test.json", json3);

            RunCmd("script.py", new string[]
            {
                "predicted_test.json",
                "observed_test.json",
                "aquisition_test.json",
                $"{quries}.png"
            });
        }

        private static double ObjectiveFunction(double x)
        {
            return -x * Trig.Cos(-2 * x) * Math.Exp(-x / 3);
        }
    }
}
