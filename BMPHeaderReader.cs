using System;
using System.IO;

namespace BMPManip
{
    class BMPFileHeader
    {
        public BMPFileHeader(Byte[] headerBytes)
        {
            // Position
            int offset = 0;

            // Read 2 byes
            bfType = ((char)headerBytes[0]).ToString();
            bfType += ((char)headerBytes[1]).ToString();
            offset = offset + 2;
            // Read 4 bytes to uint32
            bfSize = BitConverter.ToUInt32(headerBytes, offset);
            offset = offset + sizeof(UInt32);

            // Read 2 bytes to uint16
            bfReserved1 = BitConverter.ToUInt16(headerBytes, offset);
            offset = offset + sizeof(UInt16);

            // Read 2 bytes to uint16
            bfReserved2 = BitConverter.ToUInt16(headerBytes, offset);
            offset = offset + sizeof(UInt16);

            // Read 4 bytes to uint32
            bfOffBits = BitConverter.ToUInt32(headerBytes, offset);
            offset = offset + sizeof(UInt32);
        }

        public void Wyswietl()
        {
            Console.WriteLine(string.Join(Environment.NewLine,
                                        new object[]
                                        {
                                        "Typ pliku: ",                                      bfType,
                                        "Rozmiar pliku w bajtach: ",                        bfSize
                                        }));
        }


        public string bfType;                      // Ascii characters "BM"
        public UInt32 bfSize;                      // The size of file in bytes
        public UInt16 bfReserved1;                 // Unused, must be zero
        public UInt16 bfReserved2;                 // Same ^^
        public UInt32 bfOffBits;                   // Pixel offset [ where pixel array starts ]
    }

}


