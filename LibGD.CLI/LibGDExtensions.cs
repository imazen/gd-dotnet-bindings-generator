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
            gdImageStruct result = gdImageCreateFromJpegEx(temp, ignore_warning);
            File.Delete(temp);
            return result;
        }

#if WITH_TIFF

        public static gdImageStruct gdImageCreateFromTiff(byte[] bytes)
        {
            return ReadFromByteArray(bytes, gdImageCreateFromTiff);
        }

#endif

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
            gdImageStruct result = gdImageCreateFromGd2Part(temp, srcx, srcy, w, h);
            File.Delete(temp);
            return result;
        }

        public static gdImageStruct gdImageCreateFromXbm(byte[] bytes)
        {
            return ReadFromByteArray(bytes, gdImageCreateFromXbm);
        }

        public static gdImageStruct gdImageCreateFromPng(string file)
        {
            return ReadFromFile(file, gdImageCreateFromPng);
        }

        public static gdImageStruct gdImageCreateFromGif(string file)
        {
            return ReadFromFile(file, gdImageCreateFromGif);
        }

        public static gdImageStruct gdImageCreateFromWBMP(string file)
        {
            return ReadFromFile(file, gdImageCreateFromWBMP);
        }

        public static gdImageStruct gdImageCreateFromJpeg(string file)
        {
            return ReadFromFile(file, gdImageCreateFromJpeg);
        }

        public static gdImageStruct gdImageCreateFromJpegEx(string file, int ignore_warning)
        {
            IntPtr fd = C.fopen(file, "rb");
            gdImageStruct result;
            try
            {
                result = gdImageCreateFromJpegEx(fd, ignore_warning);
            }
            finally
            {
                C.fclose(fd);
            }
            return result;
        }

#if WITH_TIFF

        public static gdImageStruct gdImageCreateFromTiff(string file)
        {
            return ReadFromFile(file, gdImageCreateFromTiff);
        }

#endif

        public static gdImageStruct gdImageCreateFromTga(string file)
        {
            return ReadFromFile(file, gdImageCreateFromTga);
        }

        public static gdImageStruct gdImageCreateFromBmp(string file)
        {
            return ReadFromFile(file, gdImageCreateFromBmp);
        }

        public static gdImageStruct gdImageCreateFromGd(string file)
        {
            return ReadFromFile(file, gdImageCreateFromGd);
        }

        public static gdImageStruct gdImageCreateFromGd2(string file)
        {
            return ReadFromFile(file, gdImageCreateFromGd2);
        }

        public static gdImageStruct gdImageCreateFromGd2Part(string file, int srcx, int srcy, int w, int h)
        {
            IntPtr fd = C.fopen(file, "rb");
            gdImageStruct result;
            try
            {
                result = gdImageCreateFromGd2Part(fd, srcx, srcy, w, h);
            }
            finally
            {
                C.fclose(fd);
            }
            return result;
        }

        public static gdImageStruct gdImageCreateFromXbm(string file)
        {
            return ReadFromFile(file, gdImageCreateFromXbm);
        }

        private static gdImageStruct ReadFromByteArray(byte[] bytes, Func<IntPtr, gdImageStruct> function)
        {
            string temp = Path.GetTempFileName();
            File.WriteAllBytes(temp, bytes);
            gdImageStruct result;
            try
            {
                result = ReadFromFile(temp, function);
            }
            finally
            {
                File.Delete(temp);
            }
            return result;
        }

        private static gdImageStruct ReadFromFile(string file, Func<IntPtr, gdImageStruct> function)
        {
            IntPtr fd = C.fopen(file, "rb");
            gdImageStruct result;
            try
            {
                result = function(fd);
            }
            finally
            {
                C.fclose(fd);
            }
            return result;
        }


        public static void gdImageBmp(gdImageStruct im, string outFile, int compression)
        {
            var fp = C.fopen(outFile, "wb");
            try
            {
                gd.gdImageBmp(im, fp, 1);
            }
            finally
            {
                C.fclose(fp);
            }
        }

        public static void gdImageGd(gdImageStruct im, string outFile)
        {
            var fp = C.fopen(outFile, "wb");
            try
            {
                gd.gdImageGd(im, fp);
            }
            finally
            {
                C.fclose(fp);
            }
        }

        public static void gdImageGd2(gdImageStruct im, string @out, int cs, int fmt)
        {
            var fp = C.fopen(@out, "wb");
            try
            {
                gd.gdImageGd2(im, fp, cs, fmt);
            }
            finally
            {
                C.fclose(fp);
            }
        }

        public static void gdImagePng(gdImageStruct im, string outFile)
        {
            var fp = C.fopen(outFile, "wb");
            try
            {
                gd.gdImagePng(im, fp);
            }
            finally
            {
                C.fclose(fp);
            }
        }

        public static void gdImageJpeg(gdImageStruct im, string @out, int quality)
        {
            var fp = C.fopen(@out, "wb");
            try
            {
                gd.gdImageJpeg(im, fp, quality);
            }
            finally
            {
                C.fclose(fp);
            }
        }

        public static void gdImageGif(gdImageStruct im, string outFile)
        {
            var fp = C.fopen(outFile, "wb");
            try
            {
                gd.gdImageGif(im, fp);
            }
            finally
            {
                C.fclose(fp);
            }
        }

#if WITH_TIFF

        public static void gdImageTiff(gdImageStruct im, string outFile)
        {
            var fp = C.fopen(outFile, "wb");
            try
            {
                gd.gdImageTiff(im, fp);
            }
            finally
            {
                C.fclose(fp);
            }
        }

#endif

    }
}
