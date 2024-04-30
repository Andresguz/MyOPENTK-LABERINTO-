using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;
using PixelFormat = OpenTK.Graphics.OpenGL.PixelFormat;

namespace MyOPENTK
{
    class TextureLoader
    {
        /// <summary>
        /// Cargar a memoria una textura
        /// </summary>
        /// <param name="filename">Ruta de la textura en disco</param>
        /// <returns>Indice OpenGL de la textura</returns>
        public static int LoadTexture(string filename)
        {
            if (String.IsNullOrEmpty(filename))
                throw new ArgumentException("The file name can not be empty or null.");
            int id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, id);
            //LOAD FILE
            Bitmap image;
            try { image = new Bitmap(filename); }
            catch { Console.WriteLine(@"ERROR LOAD BITMAP"); return -1; };

            BitmapData imageData = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppRgb);//Format32bppRgb
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, imageData.Width, imageData.Height, 0,PixelFormat.Bgra, PixelType.UnsignedByte, imageData.Scan0);//PixelFormat.Bgra
            image.UnlockBits(imageData);

            //TEXTURE PROPERTY
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);

            return id;
        }
    }
}
