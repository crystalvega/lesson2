using System;
using System.Linq;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;

namespace TestFormalNeuron
{
    class Program
    {
        struct FormalNeuron
        {
                public double[] w = { 0, 0, 0, 0 };
                public int c = 0;
                public FormalNeuron(int[][] X, int[] Y, double A, double B)
                {
                    a = A;
                    b = B;
                    while (learn(X, Y))
                    {
                        if (c++ > 10000) break;
                    }
                }

                public double calculate(int[] x)
                {
                    double s = b;
                    for (int i = 0; i < w.Length; i++) s += w[i] * x[i];
                    return (s > 0) ? 1 : 0;
                }

                bool learn(int[][] X, int[] Y)
                {
                    double[] w_ = new double[w.Length];

                    Array.Copy(w, w_, w.Length);

                    int i = 0;
                    foreach (int[] x in X)
                    {
                        int y = Y[i++];
                        for (int j = 0; j < x.Length; j++)
                        {
                            w[j] += a * (y - calculate(x)) * x[j];
                        }
                    }
                    return !Enumerable.SequenceEqual(w_, w);
                }
                public void GetInfo(int[][] Test)
                {
                    Console.WriteLine("[{0}] {1}",
                    string.Join(", ", w),
                    c
                    );
                    foreach (int[] test in Test)
                    {
                        Console.WriteLine("[{0}] {1} {2}",
                            string.Join(", ", test),
                            test[3],
                            calculate(test)
                        );
                    }
                }
        }

        class Settings
        {
            public double a = 0.02;
            public double b = -0.4;

            public int[][] X =
            {
                new int [] {0, 0, 0, 0},
                new int [] {0, 0, 0, 1},
                new int [] {1, 1, 1, 0},
                new int [] {1, 1, 1, 0},
                new int [] {1, 1, 1, 1}
            };

            public int[] Y = { 0, 1, 1, 0, 1 };

            public int[][] Test =
            {
                new int [] {0, 0, 0, 0},
                new int [] {0, 0, 0, 1},
                new int [] {0, 1, 0, 1},
                new int [] {0, 1, 1, 0},
                new int [] {1, 1, 1, 0},
                new int [] {1, 1, 1, 1}
            };
        }

        public static int Main()
        {
            Settings s = new Settings();
            FormalNeuron neuron = new(s.X, s.Y, s.a, s.b);
            neuron.GetInfo(s.Test);
            return 0;
        }
    }
}