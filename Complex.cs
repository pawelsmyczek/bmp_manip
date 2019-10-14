using System;

namespace BMPManip
{
    public class Complex
    {
        public double real = 0.0;
        public double imag = 0.0;

        public Complex()
        {

        }
        public Complex(double real, double imag)
        {
            this.real = real;
            this.imag = imag;
        }

        public Complex(double real)
        {
            this.real = real;
            this.imag = 0.0;
        }

        public static Complex fromPolar(double r, double radians) // For every pixel scale
        {
            Complex Data = new Complex(r * Math.Cos(radians), r * Math.Sin(radians));

            return Data;
        }

        public static double toPolar(Complex A) // After computing FFT
        {
            double magnitude = A.Magnitude;
            double phase = A.Phase;
            double polarComplex = magnitude * Math.Exp(phase);
            return polarComplex;
        }


    


    public static Complex operator +(Complex A, Complex B)
        {
            Complex Data = new Complex(A.real + B.real, B.imag + B.imag);
            return Data;
        }
        public static Complex operator -(Complex A, Complex B)
        {
            Complex Data = new Complex(A.real - B.real, B.imag - B.imag);
            return Data;
        }
        public static Complex operator *(Complex A, Complex B)
        {
            Complex Data = new Complex((A.real * B.real)-(A.imag * B.imag),
                                        (A.real * B.imag) + (A.imag * B.real));
            return Data;
        }



        public double Magnitude // Moduł
        {
            get
            {
                return Math.Sqrt(Math.Pow(real,2) + Math.Pow(imag, 2));
            }
        }

        public double Phase // Faza
        {
            get
            {
                return Math.Atan(imag / real);
            }
        }

    }
}
