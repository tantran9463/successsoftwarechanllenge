using System.Diagnostics;
using System.Text.RegularExpressions;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

Console.WriteLine("start service image!");
var rootFolder = System.Environment.CurrentDirectory;
string output = Path.Combine(rootFolder, "output");
Directory.CreateDirectory(output);
string patternImg = @"([a-zA-Z].*?)\.(jpg|JPG|jpeg|JPEG|bmp|BMP|gif|GIF|png|PNG)$";
string extensionOutput = ".jpeg";
await LoadImg(rootFolder, patternImg, output, extensionOutput);
Console.WriteLine("stop service image");

static async Task LoadImg(string path, string patternImg, string output, string extensionOutput)
{
    var files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).Where(f => Regex.IsMatch(f, patternImg));
    foreach (var file in files)
    {
        if (File.Exists(file))
        {
            using (var image = await Image.LoadAsync(file))
            {
                int width = image.Width;
                int height = image.Height;
                double ratio = width / height;
                if (ratio == 2)
                {
                    Rectangle rectangle = new Rectangle(0, 0, width / 2, height);
                    var firstHalf = image.Clone(i => i.Crop(rectangle));
                    await SaveImg(firstHalf, output, extensionOutput);
                    rectangle = new Rectangle(width / 2, 0, width / 2, height);
                    var secondHalf = image.Clone(i => i.Crop(rectangle));
                    await SaveImg(secondHalf, output, extensionOutput);
                }
                else
                {
                    image.Mutate(x => x.Resize(width / 2, height / 2));
                    await SaveImg(image, output, extensionOutput);
                }
            }
        }
    }
}
static async Task SaveImg(Image image, string output, string extentionOutput)
{
    await image.SaveAsJpegAsync(Path.Combine(output, $"{DateTime.Now.Ticks}{extentionOutput}"));
}