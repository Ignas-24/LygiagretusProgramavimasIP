using System.Diagnostics;

namespace LygiagretusProgramavimasIP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            for(int threadCount = 1; threadCount <= 16; threadCount++)
            {
                List<TimeSpan> timings = new List<TimeSpan>();
                int runCount = 5;
               
                for (int i = 0; i < runCount; i++)
                {
                    var start = DateTime.Now;
                    string projectDir = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(AppContext.BaseDirectory).FullName).FullName).FullName).FullName;
                    string imageDirectory = "TestCases\\5";
                    string resultDirectory = "Results";
                    var fullResultPath = Path.Combine(projectDir, resultDirectory);
                    var fullImageDirectory = Path.Combine(projectDir, imageDirectory);
                    List<string> imagePaths = ImageProcessor.ReadImageDirectory(fullImageDirectory);
                    if (Directory.Exists(fullResultPath))
                    {
                        Directory.Delete(fullResultPath, true);
                    }
                    var dir = Directory.CreateDirectory(fullResultPath);
                    string resultPath = dir.FullName;

                    imagePaths.AsParallel().WithDegreeOfParallelism(threadCount).ForAll(imagePath =>
                    {
                        ImageProcessor.GrayscaleImage(imagePath, fullResultPath);
                    });

                    //foreach (var imagePath in imagePaths)
                    //{
                    //    ImageProcessor.GrayscaleImage(imagePath, fullResultPath);
                    //}
                    var end = DateTime.Now;
                    timings.Add(end - start);
                }
                if (timings.Count > 1) timings = timings.Skip(1).ToList();
                Console.WriteLine($"{threadCount}   {timings.Select(t => t.TotalMilliseconds).Average()}");
            }
        }
    }
}