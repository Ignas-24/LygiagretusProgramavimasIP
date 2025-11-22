namespace LygiagretusProgramavimasIP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string imageDirectory = "TestCases\\1";
            string resultDirectory = "Results";
            var fullResultPath = Path.Combine(Environment.CurrentDirectory, resultDirectory);
            int threadCount = 3;
            List<string> imagePaths = ImageProcessor.ReadImageDirectory(imageDirectory);
            if (Directory.Exists(fullResultPath))
            {
                Directory.Delete(fullResultPath, true);
            }
            var dir = Directory.CreateDirectory(fullResultPath);
            string resultPath = dir.FullName;
            imagePaths.AsParallel().WithDegreeOfParallelism(threadCount).ForAll(imagePath =>
            {
                ImageProcessor.GrayscaleImage(imagePath, resultDirectory);
            });
        }
    }
}