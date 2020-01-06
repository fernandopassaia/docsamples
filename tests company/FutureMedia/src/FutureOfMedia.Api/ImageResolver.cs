using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;

namespace FutureOfMedia.Api
{
    public static class ImageResolver
    {
        //Note: To resize the Image I'm using a Library called ImageSharp: Install -Package SixLabors.ImageSharp
        //.Net Core still don't have a native library to Deal with Images so i had 2 options:
        //(1) Create a Class Library in 4.6 Framework and use System.Drawing OR
        //(2) Use 100% Native .NET Core code. I Choose this: To Keep 100% Compatible with Linux.        

        public static void ResizeAndSaveImage(Stream stream, string filename)
        {
            using (Image<Rgba32> image = Image.Load(stream))
            {
                image.Mutate(x => x
                     .Resize(1024, 768)
                 );
                image.Save(filename); // Automatic encoder selected based on extension.
            }
        }
    }
}
