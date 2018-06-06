using System.IO;
using Android.Graphics;
using MvvmCross;
using MvvmCross.Plugin.File;

namespace Services
{
    public class AndroidImageLoader : ImageLoaderService
	{
        private static string ImagePath = "Lost_";
		private readonly IMvxFileStore fileStore = Mvx.Resolve<IMvxFileStore>();

		public static byte[] ReadFully(Stream input)
		{
			using (MemoryStream ms = new MemoryStream())
			{
				input.CopyTo(ms);
				return ms.ToArray();
			}
		}

		public byte[] LoadImage(string name)
		{
			var path = this.fileStore.NativePath(ImagePath) + name;
            return ReadFully(fileStore.OpenRead(path));
		}

		public void SaveImage(byte[] bytes, string name, int maxDimenSize = -1)
		{
			var path = this.fileStore.NativePath(ImagePath) + name;

            if (maxDimenSize != -1)
			{
				var stream = new MemoryStream();
				Bitmap bitmap = BitmapFactory.DecodeByteArray(bytes, 0 ,bytes.Length);
                float scaleRatio;
                if (bitmap.Width >= bitmap.Height)
                {
                    scaleRatio = (float)maxDimenSize / (float)bitmap.Width;
                } else 
                {
                    scaleRatio = (float)maxDimenSize / (float)bitmap.Height;
                }

                var matrix = new Matrix();
                matrix.PostScale(scaleRatio, scaleRatio);

                var scaledBitmap = Bitmap.CreateBitmap(bitmap, 0, 0, bitmap.Width, bitmap.Height, matrix, false);
                scaledBitmap.Compress(Bitmap.CompressFormat.Jpeg, 75, stream);
                bitmap.Recycle();
				fileStore.WriteFile(path, stream.ToArray());	
			}
			else 
			{
				fileStore.WriteFile(path, bytes);	
			}
		}
	}
}
