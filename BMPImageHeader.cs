using System;
using System.IO;

namespace BMPManip
{
    class BMPImageHeader
    {
        private UInt32 biSize;          // Size of header, must be least 40
        private UInt32 biWidth;         // Image width in pixels
        private UInt32 biHeight;        // Image height in pixels
        private UInt16 biPlanes;        // Must be 1
        private UInt16 biBitCount;      // Bits per pixels.. 1..4..8..16..32    
        private UInt32 biCompression;   // 0 No compression
        private UInt32 biSizeImage;     // Image size, may be zer for   uncompressed
        private UInt32 xPelsPerMeter;   // Preferred resolution in pixels per meter
        private UInt32 yPelsPerMeter;   // Same ^^
        private UInt32 biClrUsed;       // Number color map entries
        private UInt32 biClrImportant;  // Number of significant colors

        public uint BiSize { get => biSize; set => biSize = value; }
        public uint BiWidth { get => biWidth; set => biWidth = value; }
        public uint BiHeight { get => biHeight; set => biHeight = value; }
        public ushort BiPlanes { get => biPlanes; set => biPlanes = value; }
        public ushort BiBitCount { get => biBitCount; set => biBitCount = value; }
        public uint BiCompression { get => biCompression; set => biCompression = value; }
        public uint BiSizeImage { get => biSizeImage; set => biSizeImage = value; }
        public uint XPelsPerMeter { get => xPelsPerMeter; set => xPelsPerMeter = value; }
        public uint YPelsPerMeter { get => yPelsPerMeter; set => yPelsPerMeter = value; }
        public uint BiClrUsed { get => biClrUsed; set => biClrUsed = value; }
        public uint BiClrImportant { get => biClrImportant; set => biClrImportant = value; }

        public BMPImageHeader(Byte[] headerBytes)
        {
            // Position
            int offset = 0;

            BiSize = BitConverter.ToUInt32(headerBytes, offset);
            offset = offset + sizeof(UInt32);

            BiWidth = BitConverter.ToUInt32(headerBytes, offset);
            offset = offset + sizeof(UInt32);

            BiHeight = BitConverter.ToUInt32(headerBytes, offset);
            offset = offset + sizeof(UInt32);

            BiPlanes = BitConverter.ToUInt16(headerBytes, offset);
            offset = offset + sizeof(UInt16);

            BiBitCount = BitConverter.ToUInt16(headerBytes, offset);
            offset = offset + sizeof(UInt16);

            BiCompression = BitConverter.ToUInt32(headerBytes, offset);
            offset = offset + sizeof(UInt32);

            BiSizeImage = BitConverter.ToUInt32(headerBytes, offset);
            offset = offset + sizeof(UInt32);

            XPelsPerMeter = BitConverter.ToUInt32(headerBytes, offset);
            offset = offset + sizeof(UInt32);

            YPelsPerMeter = BitConverter.ToUInt32(headerBytes, offset);
            offset = offset + sizeof(UInt32);

            BiClrUsed = BitConverter.ToUInt32(headerBytes, offset);
            offset = offset + sizeof(UInt32);

            BiClrImportant = BitConverter.ToUInt32(headerBytes, offset);
            offset = offset + sizeof(uint);
        }

        public void Wyswietl()
        {
            Console.WriteLine("Informacje pobrane z nagłówka pliku graficznego źródłowego.");
            Console.WriteLine();
            Console.WriteLine(string.Join(Environment.NewLine,
                                        new object[]
                                        {
                                        "Rozmiar nagłówka: ",                               BiSize,
                                        "Stosunek bitów na pojedynczy pixel: ",             BiBitCount,
                                        "Rozmiar obrazu w bajtach: ",                       BiSizeImage,
                                        "Rodzaj kompresji (0=zadna, 1=RLE-8, 2=RLE-4): ",   BiCompression,
                                        "Paleta kolorów(0 dla nieindeksowanych obrazow): ",                          BiClrUsed
                                        }));

        }


    }
}



