using System;
using System.IO;
using System.Drawing;
using System.Runtime.Remoting.Messaging;
using System.Drawing.Imaging;

namespace BMPManip
{
    class ImageManip
    {
        public string Location { get; private set; }
        //public Pixel[][] Pic { get => pic; set => pic = value; }

        public BMPFileHeader bmpfh;
        public BMPImageHeader bmpih;
        public int bytesperpixel;
        public int SIZE = 2;
        //public Pixel[][] Pic;

        byte[] Convert16BitGrayScaleToRgb48(byte[] inBuffer, int width, int height)
        {
            int inBytesPerPixel = 2;
            int outBytesPerPixel = 6;

            byte[] outBuffer = new byte[width * height * outBytesPerPixel];
            int inStride = width * inBytesPerPixel;
            int outStride = width * outBytesPerPixel;

            // Step through the image by row  
            for (int y = 0; y < height; y++)
            {
                // Step through the image by column  
                for (int x = 0; x < width; x++)
                {
                    // Get inbuffer index and outbuffer index 
                    int inIndex = (y * inStride) + (x * inBytesPerPixel);
                    int outIndex = (y * outStride) + (x * outBytesPerPixel);

                    byte hibyte = inBuffer[inIndex + 1];
                    byte lobyte = inBuffer[inIndex];

                    //R
                    outBuffer[outIndex] = lobyte;
                    outBuffer[outIndex + 1] = hibyte;

                    //G
                    outBuffer[outIndex + 2] = lobyte;
                    outBuffer[outIndex + 3] = hibyte;

                    //B
                    outBuffer[outIndex + 4] = lobyte;
                    outBuffer[outIndex + 5] = hibyte;
                }
            }
            return outBuffer;
        }

        public ImageManip(string _imageLocation)
        {
            Location = _imageLocation;



            if (File.Exists(_imageLocation))
            {

                //int offset = 0;
                 Byte[] fsBuffer;

                //string[] Path = { _imageLocation, _name };
                FileStream fs = File.Open(path: _imageLocation, mode: FileMode.Open, access: FileAccess.Read);

                // Start by reading file header..
                fsBuffer = new Byte[14]; // size 40 bytes
                fs.Read(fsBuffer, 0, 14);
                bmpfh = new BMPFileHeader(fsBuffer);

                // Then image header, 40 bytes
                fsBuffer = new Byte[40];
                fs.Read(fsBuffer, 0, 40);
                bmpih = new BMPImageHeader(fsBuffer);
                

                // How many bytes per pixel
                bytesperpixel= bmpih.BiBitCount / 8;

                // Read pixel array
                int totalBytes = (int)bmpih.BiWidth * (int)bmpih.BiHeight * bytesperpixel;
                int totalWidth = (int)bmpih.BiWidth;
                int totalHeight = (int)bmpih.BiHeight;

                fsBuffer = new Byte[totalBytes];
                fs.Read(fsBuffer, 0, totalBytes);
                //int indeks = 0;


                if (totalHeight == totalWidth)
                {

                    while (SIZE <= totalHeight)
                    {

                        SIZE *= 2;
                    }
                }
                else if (totalHeight < totalWidth)
                {

                    while (SIZE <= totalHeight)
                    {
                        SIZE *= 2;
                    }
                }
                else if (totalHeight > totalWidth)
                {

                    while (SIZE <= totalWidth)
                    {
                        SIZE *= 2;
                    }
                }


                Pixel[,] Pix = new Pixel[totalHeight, totalWidth];
                Complex[,] BigData = new Complex[SIZE, SIZE];


                Complex[] Data = new Complex[SIZE];
                if (bytesperpixel == 1) // If an image is in grayscale
                {
                    for (int j = 0; j < (int)bmpih.BiHeight; j++)
                    {
                        for (int i = 0; i < (int)bmpih.BiWidth; i++)
                        {
                            //
                            Pixel Pic = new Pixel(fsBuffer[j * (int)bmpih.BiWidth + i]);
                            Pix[j, i] = Pic;
                        }
                    }
                }
                else // Otherwise if image is RGB
                {
                    for (int j = 0; j < (int)bmpih.BiHeight; j++)
                    {
                        for (int i = 0; i < (int)bmpih.BiWidth; i++)
                        {
                            //if(i == 0 && j == 0)
                            //{

                            //Pix[j, i] = if0;
                            //Pix[j, i+1] = if1; 
                            // Complex data = new Complex()
                            //}
                            Pixel Pic = new Pixel(fsBuffer[j * (int)bmpih.BiWidth + i], fsBuffer[j * (int)bmpih.BiWidth + i + 1], fsBuffer[j * (int)bmpih.BiWidth + i + 2]);
                            Pix[j, i] = Pic;
                            double sum = Math.Sqrt(Math.Pow(Pix[j, i].Red, 2) + Math.Pow(Pix[j, i].Green, 2) + Math.Pow(Pix[j, i].Blue, 2));

                            //Pixel Buffer = new Pixel(fsBuffer[j * (int)bmpih.BiWidth + i + 3], fsBuffer[j * (int)bmpih.BiWidth + i + 4], fsBuffer[j * (int)bmpih.BiWidth + i + 5]);
                            //if (i < SIZE && j < SIZE)
                            //{

                                //double sumNext = Buffer.Red + Buffer.Green + Buffer.Blue;
                                //Complex data = new Complex(sum, sumNext);
                                Complex single = new Complex(sum);
                                Complex bit = new Complex(fsBuffer[j * (int)bmpih.BiWidth + i]);
                            //Data[indeks] = data;
                                BigData[j, i] = bit;
                               // Console.WriteLine(BigData[j, i].real + " " + BigData[j, i].imag);
                                //}

                                //indeks++;


                                //Console.WriteLine("Pix["+ j + ", " + i + "].Red " + Pix[j,i].Red + " " + "Pix[" + j + ", " + i + "].Green " + Pix[j,i].Green + " " + "Pix[" + j + ", " + i + "].Blue " + Pix[j,i].Blue + ", suma: " + sum);
                                //Complex complexPixel = new Complex(1, Pix[j-1,i-1], Pix[j,i]);
                        }
                    }


                }


                for (int j = 0; j < SIZE; j++)
                {
                    for (int i = 0; i < SIZE; i++)
                    {
                        if(j >= totalHeight) BigData[j, i] = new Complex(0);
                        if (i >= totalWidth) BigData[j, i] = new Complex(0);
                    }
                }


                Complex[,] Fft = new Complex[SIZE, SIZE];
                int[,] toInt2D = new int[SIZE, SIZE];
                uint[] toInt = new uint[SIZE * SIZE];
                for (int j = 0; j < SIZE; j++) // Transformacja wierszy
                {
                    Complex[] tempRow = new Complex[SIZE];
                    for (int i = 0; i < SIZE; i++)
                    {
                        tempRow[i] = BigData[j, i];

                    }
                    Complex[] tempFFT = Fourier.FFT(tempRow);

                    for(int k = 0; k < SIZE; k++)
                    {
                        Fft[j, k] = tempFFT[k];
                        //Console.WriteLine(Fft[j, k].real + " " + Fft[j, k].imag);
                    }

                }

                for(int j = 0; j < SIZE; j++) // Transformacja kolumn
                {
                    Complex[] tempColumn = new Complex[SIZE];
                    for(int i = 0; i < SIZE; i++)
                    {
                        tempColumn[i] = Fft[i, j];
                    }
                    Complex[] tempFFT = Fourier.FFT(tempColumn);
                    for (int k = 0; k < SIZE; k++)
                    {
                        Fft[j, k] = tempFFT[k];
                        //toInt2D[k,j] = (int)Math.Abs(255*Fft[k, j].Phase);
                        //Console.WriteLine(Fft[j, k].real + " " + Fft[j, k].imag);
                        //Console.WriteLine("Gauss polar form: " + toInt[k]);
                    }
                }


                //Wynik do tablicy int[,]

                toInt2D = Fourier.MagnitudePlot(Fft);

                byte[] pixelValues = new byte[2 * totalHeight * totalWidth];
                int index = 0;
                for (int j = 0; j < totalHeight; j++)
                {
                    for (int i = 0; i < totalWidth; i++)
                    {
                        pixelValues[index] = (byte)(((UInt16)(toInt2D[j, i]) & 0xFF00) >> 8);
                        pixelValues[index + 1] = (byte)(((UInt16)(toInt2D[j, i]) & 0x00FF));
                        index += 2;
                        //toInt[ind] = (uint)toInt2D[j, i];
                        //Color pixelColor = imageFinal.GetPixel(j, i);

                        //Color newColor = Color.FromArgb(toInt2D[j, i]);
                        //imageFinal.SetPixel(j, i, (Color)toInt2D[j,i]);

                        //Console.WriteLine(toInt2D[j, i]);

                    }
                }
                
                byte[] newPixelVal = new byte[totalHeight * totalWidth * 6];
                newPixelVal = Convert16BitGrayScaleToRgb48(pixelValues, totalWidth, totalHeight);
                Bitmap img = new Bitmap(totalWidth, totalHeight, System.Drawing.Imaging.PixelFormat.Format48bppRgb);
                Rectangle dimension = new Rectangle(0, 0, img.Width, img.Height);
                System.Drawing.Imaging.BitmapData picData = img.LockBits(dimension, System.Drawing.Imaging.ImageLockMode.ReadWrite, img.PixelFormat);
                IntPtr pixelStartAddress = picData.Scan0;
                System.Runtime.InteropServices.Marshal.Copy(newPixelVal, 0, pixelStartAddress, newPixelVal.Length);
                img.UnlockBits(picData);

                img.Save(@"F:\pablito\downloads\bmp_manip\finall.bmp");


                toInt2D = Fourier.PhasePlot(Fft);


                index = 0;
                for (int j = 0; j < totalHeight; j++)
                {
                    for (int i = 0; i < totalWidth; i++)
                    {
                        pixelValues[index] = (byte)(((UInt16)(toInt2D[j, i]) & 0xFF00) >> 8);
                        pixelValues[index + 1] = (byte)(((UInt16)(toInt2D[j, i]) & 0x00FF));
                        index += 2;
                        //toInt[ind] = (uint)toInt2D[j, i];
                        //Color pixelColor = imageFinal.GetPixel(j, i);

                        //Color newColor = Color.FromArgb(toInt2D[j, i]);
                        //imageFinal.SetPixel(j, i, (Color)toInt2D[j,i]);

                        //Console.WriteLine(toInt2D[j, i]);

                    }
                }

                newPixelVal = Convert16BitGrayScaleToRgb48(pixelValues, totalWidth, totalHeight);
                Bitmap imag = new Bitmap(totalWidth, totalHeight, System.Drawing.Imaging.PixelFormat.Format48bppRgb);
                Rectangle dimension1 = new Rectangle(0, 0, imag.Width, img.Height);
                System.Drawing.Imaging.BitmapData imgData = imag.LockBits(dimension1, System.Drawing.Imaging.ImageLockMode.ReadWrite, imag.PixelFormat);
                IntPtr pixelStartAddress1 = imgData.Scan0;
                System.Runtime.InteropServices.Marshal.Copy(newPixelVal, 0, pixelStartAddress1, newPixelVal.Length);
                imag.UnlockBits(picData);

                imag.Save(@"F:\pablito\downloads\bmp_manip\finall2.bmp");
            }
        }
        /*
        public static byte[] GetBytesAlt(uint[] values)
        {
            var result = new byte[values.Length * sizeof(uint)];
            Buffer.BlockCopy(values, 0, result, 0, result.Length);
            return result;
        }
        
        public bool ByteArrayToFile(string fileName, byte[] byteArray, int offset)
        {
            try
            {
                using (var fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    fs.Write(byteArray, offset, byteArray.Length);
                    //fs.Write(Header, 0, Header.Length);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in process: {0}", ex);
                return false;
            }
        }*/



        /*public Bitmap ToImage(byte[] array)
        {
            using (var ms = new System.IO.MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // here
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }*/




    }
}