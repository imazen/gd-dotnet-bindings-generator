using System;
using System.IO;

namespace LibGD
{
    public partial class gd
    {
        public static gdImageStruct gdImageCreateFromPng(byte[] bytes)
        {
            return ReadFromByteArray(bytes, gdImageCreateFromPng);
        }

        public static gdImageStruct gdImageCreateFromGif(byte[] bytes)
        {
            return ReadFromByteArray(bytes, gdImageCreateFromGif);
        }

        public static gdImageStruct gdImageCreateFromWBMP(byte[] bytes)
        {
            return ReadFromByteArray(bytes, gdImageCreateFromWBMP);
        }

        public static gdImageStruct gdImageCreateFromJpeg(byte[] bytes)
        {
            return ReadFromByteArray(bytes, gdImageCreateFromJpeg);
        }

        public static gdImageStruct gdImageCreateFromJpegEx(byte[] bytes, int ignore_warning)
        {
            string temp = Path.GetTempFileName();
            File.WriteAllBytes(temp, bytes);
            IntPtr fd = C.fopen(temp, "rb");
            gdImageStruct result = gdImageCreateFromJpegEx(fd, ignore_warning);
            C.fclose(fd);
            File.Delete(temp);
            return result;
        }

        public static gdImageStruct gdImageCreateFromTiff(byte[] bytes)
        {
            return ReadFromByteArray(bytes, gdImageCreateFromTiff);
        }

        public static gdImageStruct gdImageCreateFromTga(byte[] bytes)
        {
            return ReadFromByteArray(bytes, gdImageCreateFromTga);
        }

        public static gdImageStruct gdImageCreateFromBmp(byte[] bytes)
        {
            return ReadFromByteArray(bytes, gdImageCreateFromBmp);
        }

        public static gdImageStruct gdImageCreateFromGd(byte[] bytes)
        {
            return ReadFromByteArray(bytes, gdImageCreateFromGd);
        }

        public static gdImageStruct gdImageCreateFromGd2(byte[] bytes)
        {
            return ReadFromByteArray(bytes, gdImageCreateFromGd2);
        }

        public static gdImageStruct gdImageCreateFromGd2Part(byte[] bytes, int srcx, int srcy, int w, int h)
        {
            string temp = Path.GetTempFileName();
            File.WriteAllBytes(temp, bytes);
            IntPtr fd = C.fopen(temp, "rb");
            gdImageStruct result = gdImageCreateFromGd2Part(fd, srcx, srcy, w, h);
            C.fclose(fd);
            File.Delete(temp);
            return result;
        }

        public static gdImageStruct gdImageCreateFromXbm(byte[] bytes)
        {
            return ReadFromByteArray(bytes, gdImageCreateFromXbm);
        }

        private static gdImageStruct ReadFromByteArray(byte[] bytes, Func<IntPtr, gdImageStruct> function)
        {
            string temp = Path.GetTempFileName();
            File.WriteAllBytes(temp, bytes);
            IntPtr fd = C.fopen(temp, "rb");
            gdImageStruct result;
            try
            {
                result = function(fd);
            }
            finally
            {
                C.fclose(fd);
                File.Delete(temp);
            }
            return result;
        }
    }
}
