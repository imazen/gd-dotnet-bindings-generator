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

#if !NO_TIFF

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
            IntPtr fd = C.fopen(file ?? string.Empty, "rb");
            try
            {
                return gdImageCreateFromJpegEx(fd, ignore_warning);
            }
            finally
            {
                Close(fd);
            }
        }

#if !NO_TIFF

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
            IntPtr fd = C.fopen(file ?? string.Empty, "rb");
            try
            {
                return gdImageCreateFromGd2Part(fd, srcx, srcy, w, h);
            }
            finally
            {
                Close(fd);
            }
        }

        public static gdImageStruct gdImageCreateFromXbm(string file)
        {
            return ReadFromFile(file, gdImageCreateFromXbm);
        }

        public static gdImageStruct gdImageCreateFromPng(Stream stream)
        {
            return ReadFromStream(stream, gdImageCreateFromPng);
        }

        public static gdImageStruct gdImageCreateFromGif(Stream stream)
        {
            return ReadFromStream(stream, gdImageCreateFromGif);
        }

        public static gdImageStruct gdImageCreateFromWBMP(Stream stream)
        {
            return ReadFromStream(stream, gdImageCreateFromWBMP);
        }

        public static gdImageStruct gdImageCreateFromJpeg(Stream stream)
        {
            return ReadFromStream(stream, gdImageCreateFromJpeg);
        }

        public static gdImageStruct gdImageCreateFromJpegEx(Stream stream, int ignore_warning)
        {
            string temp = Path.GetTempFileName();
            try
            {
                using (var output = File.OpenWrite(temp))
                {
                    stream.CopyTo(output);
                }
                return gdImageCreateFromJpegEx(temp, ignore_warning);
            }
            finally
            {
                File.Delete(temp);
            }
        }

#if !NO_TIFF

        public static gdImageStruct gdImageCreateFromTiff(Stream stream)
        {
            return ReadFromStream(stream, gdImageCreateFromTiff);
        }

#endif

        public static gdImageStruct gdImageCreateFromTga(Stream stream)
        {
            return ReadFromStream(stream, gdImageCreateFromTga);
        }

        public static gdImageStruct gdImageCreateFromBmp(Stream stream)
        {
            return ReadFromStream(stream, gdImageCreateFromBmp);
        }

        public static gdImageStruct gdImageCreateFromGd(Stream stream)
        {
            return ReadFromStream(stream, gdImageCreateFromGd);
        }

        public static gdImageStruct gdImageCreateFromGd2(Stream stream)
        {
            return ReadFromStream(stream, gdImageCreateFromGd2);
        }

        public static gdImageStruct gdImageCreateFromGd2Part(Stream stream, int srcx, int srcy, int w, int h)
        {
            string temp = Path.GetTempFileName();
            try
            {
                using (var output = File.OpenWrite(temp))
                {
                    stream.CopyTo(output);
                }
                return gdImageCreateFromGd2Part(temp, srcx, srcy, w, h);
            }
            finally
            {
                File.Delete(temp);
            }
        }

        public static gdImageStruct gdImageCreateFromXbm(Stream stream)
        {
            return ReadFromStream(stream, gdImageCreateFromXbm);
        }

        private static gdImageStruct ReadFromByteArray(byte[] bytes, Func<IntPtr, gdImageStruct> function)
        {
            string temp = Path.GetTempFileName();
            File.WriteAllBytes(temp, bytes);
            try
            {
                return ReadFromFile(temp, function);
            }
            finally
            {
                File.Delete(temp);
            }
        }

        private static gdImageStruct ReadFromFile(string file, Func<IntPtr, gdImageStruct> function)
        {
            IntPtr fd = C.fopen(file ?? string.Empty, "rb");
            try
            {
                return function(fd);
            }
            finally
            {
                Close(fd);
            }
        }

        private static gdImageStruct ReadFromStream(Stream stream, Func<IntPtr, gdImageStruct> function)
        {
            string temp = Path.GetTempFileName();
            try
            {
                using (var output = File.OpenWrite(temp))
                {
                    stream.CopyTo(output);
                }
                return ReadFromFile(temp, gdImageCreateFromPng);
            }
            finally
            {
                File.Delete(temp);
            }
        }


        public static void gdImageBmp(gdImageStruct im, string outFile, int compression)
        {
            var fp = C.fopen(outFile ?? string.Empty, "wb");
            try
            {
                gd.gdImageBmp(im, fp, 1);
            }
            finally
            {
                Close(fp);
            }
        }

        public static void gdImageGd(gdImageStruct im, string outFile)
        {
            var fp = C.fopen(outFile ?? string.Empty, "wb");
            try
            {
                gd.gdImageGd(im, fp);
            }
            finally
            {
                Close(fp);
            }
        }

        public static void gdImageGd2(gdImageStruct im, string @out, int cs, int fmt)
        {
            var fp = C.fopen(@out ?? string.Empty, "wb");
            try
            {
                gd.gdImageGd2(im, fp, cs, fmt);
            }
            finally
            {
                Close(fp);
            }
        }

        public static void gdImagePng(gdImageStruct im, string outFile)
        {
            var fp = C.fopen(outFile ?? string.Empty, "wb");
            try
            {
                gd.gdImagePng(im, fp);
            }
            finally
            {
                Close(fp);
            }
        }

        public static void gdImageJpeg(gdImageStruct im, string @out, int quality)
        {
            var fp = C.fopen(@out ?? string.Empty, "wb");
            try
            {
                gd.gdImageJpeg(im, fp, quality);
            }
            finally
            {
                Close(fp);
            }
        }

        public static void gdImageGif(gdImageStruct im, string outFile)
        {
            var fp = C.fopen(outFile ?? string.Empty, "wb");
            try
            {
                gd.gdImageGif(im, fp);
            }
            finally
            {
                Close(fp);
            }
        }

#if !NO_TIFF

        public static void gdImageTiff(gdImageStruct im, string outFile)
        {
            var fp = C.fopen(outFile ?? string.Empty, "wb");
            try
            {
                gd.gdImageTiff(im, fp);
            }
            finally
            {
                Close(fp);
            }
        }

#endif

        private static void Close(IntPtr fp)
        {
            if (fp != IntPtr.Zero)
            {
                C.fclose(fp);
            }
        }
    }
}
