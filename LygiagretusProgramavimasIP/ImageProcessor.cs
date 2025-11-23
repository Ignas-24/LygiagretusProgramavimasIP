using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace LygiagretusProgramavimasIP
{
    internal static class ImageProcessor
    {
        public static void GrayscaleImage(string imagePath, string resultDirectory)
        {
            string resultPath = Path.Combine(resultDirectory, Path.GetFileName(imagePath));

            using (Image<Rgba32> image = Image.Load<Rgba32>(imagePath))
            {
                image.ProcessPixelRows(accessor =>
                {
                    for (int y = 0; y < accessor.Height; y++)
                    {
                        Span<Rgba32> row = accessor.GetRowSpan(y);

                        for (int x = 0; x < row.Length; x++)
                        {
                            ref Rgba32 pixel = ref row[x];

                            byte gray = (byte)(0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B);

                            pixel.R = gray;
                            pixel.G = gray;
                            pixel.B = gray;
                        }
                    }
                });

                image.SaveAsPng(resultPath);
            }
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
