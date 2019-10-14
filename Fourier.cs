using System;


namespace BMPManip
{
public class Fourier
    {
	public Fourier()
	{
	}

    public static Complex[] FFT(Complex[] x) // algorytm FFT Cooley Tukey
    {
        int N = x.Length;
        Complex[] X = new Complex[N];
        Complex[] D, E;
        if (N == 1)
        {
            X[0] = x[0];
            return X;
        }
        int k;
        Complex[] e = new Complex[N / 2];
        Complex[] d = new Complex[N / 2];
        for (k = 0; k < N / 2; k++)
        {
            e[k] = x[2 * k]; // parzyste 
            d[k] = x[2 * k + 1]; // nieparzyste

                //Console.WriteLine("d[" + k + "] = " + d[k].real + " " + d[k].imag);
        }
        D = FFT(d);
        E = FFT(e);
        for (k = 0; k < N / 2; k++)
        {
                if (D[k] != null)
                {
                    Complex temp = Complex.fromPolar(1, -2 * Math.PI * k / N);
                    D[k] *= temp;
                }
        }
            for (k = 0; k < N / 2; k++)
            {
                if (E[k] != null)
                {
                    Complex temp = Complex.fromPolar(1, -2 * Math.PI * k / N);
                    E[k] *= temp;
                }
            }



            for (k = 0; k < N / 2; k++)
        {
                if (E[k] != null && D[k] != null)
                {
                    X[k] = E[k] + D[k];
                    X[k + N / 2] = E[k] - D[k];
                }
        }
        return X;
    }

        public static int[,] PhasePlot(Complex[,] comp)
        {
            int nx = comp.GetLength(0);
            int ny = comp.GetLength(1);

            int i, j;
            double max;
            
            double[,] FFTPhaseLog = new double[nx, ny];
            
            double[,] FourierPhase = new double[nx, ny];
            
            int[,] FFTPhaseNormalized = new int[nx, ny];

            for (i = 0; i < nx; i++)
                for (j = 0; j < ny; j++)
                {
                    FourierPhase[i, j] = comp[i, j].Phase;
                    FFTPhaseLog[i, j] = Math.Log(1 + Math.Abs(FourierPhase[i, j]));
                }


            FFTPhaseLog[0, 0] = 0;
            max = FFTPhaseLog[1, 1];
            for (i = 0; i < nx; i++)// Skalowanie na 8 bitów
                for (j = 0; j < ny; j++)
                {
                    if (FFTPhaseLog[i, j] > max)
                        max = FFTPhaseLog[i, j];
                }

            for (i = 0; i < nx; i++)
                for (j = 0; j < ny; j++)
                {
                    FFTPhaseLog[i, j] = FFTPhaseLog[i, j] / max;
                }

            for (i = 0; i < nx; i++)
                for (j = 0; j < ny; j++)
                {
                    FFTPhaseNormalized[i, j] = (int)(255 * FFTPhaseLog[i, j]);
                }

            return FFTPhaseNormalized;
        }

        public static int[,] MagnitudePlot(Complex[,] comp)
        {
            int nx = comp.GetLength(0);
            int ny = comp.GetLength(1);

            int i, j;
            double max;

            double[,] FFTLog = new double[nx, ny];

            double[,] FourierMagnitude = new double[nx, ny];

            int[,] normalizedFft = new int[nx, ny];

            for (i = 0; i < nx; i++)
                for (j = 0; j < ny; j++)
                {
                    FourierMagnitude[i, j] = comp[i, j].Magnitude;
                    FFTLog[i, j] = Math.Log(1 + FourierMagnitude[i, j]);
                    //Console.WriteLine(FFTLog[i, j]);
                }

            max = FFTLog[0, 0];
            for (i = 0; i < nx; i++) // Skalowanie na 8 bitów
                for (j = 0; j < ny; j++)
                {
                    if (FFTLog[i, j] > max)
                        max = FFTLog[i, j];
                }
            for (i = 0; i < nx; i++)
                for (j = 0; j < ny ; j++)
                {
                    FFTLog[i, j] = FFTLog[i, j] / max;

                    
                }
            for (i = 0; i < nx ; i++)
                for (j = 0; j < ny ; j++)
                {
                    normalizedFft[i, j] = (int)(255*FFTLog[i, j]);
                    //Console.WriteLine(normalizedFft[i, j]);
                }

            return normalizedFft;
        }

    }
}
