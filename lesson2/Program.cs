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
            double a;
            double b;
            int c;

            public FormalNeuron(int[][] X, int[] Y, double A, double B, int C)
            {
                a = A;
                b = B;
                c = C;
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
            internal double a, b;
            internal int c;

            public Settings()
            {
                a = 0.02;
                b = -0.4;
                c = 0;
            }
            public Settings(double A)
            {
                a = A;
                b = -0.4;
                c = 0;
            }
            public Settings(double A, double B)
            {
                a = A;
                b = B;
                c = 0;
            }
            public Settings(double A, double B, int C)
            {
                a = A;
                b = B;
                c = C;
            }
            public Settings(double B, int C)
            {
                a = 0.02;
                b = B;
                c = C;

            }
            internal int[][] X =
            {
                new int [] {0, 0, 0, 0},
                new int [] {0, 0, 0, 1},
                new int [] {1, 1, 1, 0},
                new int [] {1, 1, 1, 0},
                new int [] {1, 1, 1, 1}
            };

            internal int[] Y = { 0, 1, 1, 0, 1 };

            internal int[][] Test =
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
            double a, b;
            Console.Write("Введите значение A: ");
            bool isDouble = Double.TryParse(Console.ReadLine(), out a);
            if (!isDouble)
            {
                Console.WriteLine("Введено значение не соответствующее переменной Double, установлено значение 0.02");
                a = 0.02;
            }

            Console.Write("Введите значение B: ");
            isDouble = Double.TryParse(Console.ReadLine(), out b);
            if (!isDouble)
            {
                Console.WriteLine("Введено значение не соответствующее переменной Double, установлено значение -0.04");
                b = -0.04;
            }

            Console.Write("Введите значение C: ");
            isDouble = Int32.TryParse(Console.ReadLine(), out int c);
            if (!isDouble)
            {
                Console.WriteLine("Введено значение не соответствующее переменной Int, установлено значение 0");
                c = 0;
            }

            Console.Write("Для переменных установлены следующие значения: (A = ");
            Console.Write(a);
            Console.Write(", B = ");
            Console.Write(b);
            Console.Write(", C = ");
            Console.Write(c);
            Console.WriteLine(")");

            Settings s = new Settings(a, b, c);
            FormalNeuron neuron = new(s.X, s.Y, s.a, s.b, s.c);
            neuron.GetInfo(s.Test);
            return 0;
        }
    }
}