
namespace LygiagretusProgramavimasIP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string imageDirectory = "/TestCases/1";
            int threadCount = 1;
            List<string> imagePaths = ImageProcessor.ReadImageDirectory(imageDirectory);
            imagePaths.AsParallel().WithDegreeOfParallelism(threadCount).ForAll(imagePath =>
            {
                ImageProcessor.GrayScaleImage(imagePath, "resultDirectory");
            });
        }
    }
}