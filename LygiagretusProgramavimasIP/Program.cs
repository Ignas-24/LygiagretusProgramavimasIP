namespace LygiagretusProgramavimasIP
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<TimeSpan> timings = new List<TimeSpan>();
            int runCount = 2;
            for (int i = 0; i < runCount; i++)
            {
                string projectDir = Directory.GetParent(Directory.GetParent(Directory.GetParent(Directory.GetParent(AppContext.BaseDirectory).FullName).FullName).FullName).FullName;
                string imageDirectory = "TestCases\\Default";
                string resultDirectory = "Results";
                var fullResultPath = Path.Combine(projectDir, resultDirectory);
                var fullImageDirectory = Path.Combine(projectDir, imageDirectory);
                int threadCount = 8;
                List<string> imagePaths = ImageProcessor.ReadImageDirectory(fullImageDirectory);
                if (Directory.Exists(fullResultPath))
                {
                    Directory.Delete(fullResultPath, true);
                }
                var dir = Directory.CreateDirectory(fullResultPath);
                string resultPath = dir.FullName;
                var start = DateTime.Now;
                //imagePaths.AsParallel().WithDegreeOfParallelism(threadCount).ForAll(imagePath =>
                //{
                //    ImageProcessor.GrayscaleImage(imagePath, fullResultPath);
                //});

                foreach (var imagePath in imagePaths)
                {
                    ImageProcessor.GrayscaleImage(imagePath, fullResultPath);
                }
                var end = DateTime.Now;
                timings.Add(end - start);
                Console.WriteLine(end - start);
            }
            Console.WriteLine("average:");
            if (timings.Count > 1) timings = timings.Skip(1).ToList();
            Console.WriteLine(timings.Select(t => t.TotalMilliseconds).Average());
            Console.WriteLine(timings.Select(t => t.TotalSeconds).Average());

        }
    }
}