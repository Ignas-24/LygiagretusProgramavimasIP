using SkiaSharp;

namespace LygiagretusProgramavimasIP
{
    internal static class ImageProcessor
    {
        public static void GrayscaleImage(string imagePath, string resultDirectory)
        {
            string resultPath = Path.Combine(resultDirectory, Path.GetFileName(imagePath));
            using var inputImage = SKBitmap.Decode(imagePath);
            var grayScaleImage = new SKBitmap(inputImage.Width, inputImage.Height);
            for(int x = 0; x < inputImage.Width; x++)
            {
                for(int y = 0; y < inputImage.Height; y++)
                {
                    var pixel = inputImage.GetPixel(x, y);

                    byte grayValue = (byte)(0.299 * pixel.Red + 0.587 * pixel.Green + 0.114 * pixel.Blue);
                    
                    grayScaleImage.SetPixel(x, y, new SKColor(grayValue, grayValue, grayValue, pixel.Alpha));
                }
            }
            using var surface = SKImage.FromPixels(grayScaleImage.PeekPixels());
            using var outputData = surface.Encode(SKEncodedImageFormat.Png, 100);
            using var outputStream = File.OpenWrite(resultPath);

            outputData.SaveTo(outputStream);
        }
        public static List<string> ReadImageDirectory(string directoryPath)
        {
            string[] pathsArr = Directory.GetFiles(directoryPath);
            List<string> paths = new List<string>(pathsArr);
            paths = paths.Where(path => path.EndsWith(".png")).ToList();
            return paths;
        }
    }
}
