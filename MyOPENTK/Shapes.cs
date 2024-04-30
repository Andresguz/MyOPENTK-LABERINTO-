using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK.Graphics;
using OpenTK;
using OpenTK.Graphics.OpenGL;
namespace MyOPENTK
{
    public class Shapes
    {
        public void Piso()
        {
            GL.Color3(Color.White);
            GL.Begin(PrimitiveType.Polygon);
            GL.TexCoord2(0.0f, 0.0f);
            GL.Vertex3(0.0, 0.5, -1.0);

            GL.TexCoord2(1.0f, 0.0f);
            GL.Vertex3(0.0, 0.5, 1.0);

            GL.TexCoord2(1.0f, 1.0f);
            GL.Vertex3(0.0, -0.5, 1.0);

            GL.TexCoord2(0.0f, 1.0f);
            GL.Vertex3(0.0, -0.5, -1.0);
            GL.End();
        }
        public void Draw_Pyramid()
        {   // cara frontal
            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(Color.MidnightBlue);
            GL.Vertex3(-0.5f, 0.0f, 0.5f);
            GL.Color3(Color.SpringGreen);
            GL.Vertex3(0.0f, +1.0f, 0.0f);
            GL.Color3(Color.Ivory);
            GL.Vertex3(0.5f, 0.0f, 0.5f);
            GL.End();

            // cara posterior
            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(Color.MidnightBlue);
            GL.Vertex3(-0.5f, 0.0f, -0.5f);
            GL.Color3(Color.SpringGreen);
            GL.Vertex3(0.0f, +1.0f, 0.0f);
            GL.Color3(Color.Ivory);
            GL.Vertex3(0.5f, 0.0f, -0.5f);
            GL.End();

            // cara lateral Izq
            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(Color.MidnightBlue);
            GL.Vertex3(-0.5f, 0.0f, -0.5f);
            GL.Color3(Color.SpringGreen);
            GL.Vertex3(0.0f, +1.0f, 0.0f);
            GL.Color3(Color.Ivory);
            GL.Vertex3(-0.5f, 0.0f, 0.5f);
            GL.End();

            // cara lateral Der
            GL.Begin(PrimitiveType.Triangles);
            GL.Color3(Color.MidnightBlue);
            GL.Vertex3(0.5f, 0.0f, -0.5f);
            GL.Color3(Color.SpringGreen);
            GL.Vertex3(0.0f, +1.0f, 0.0f);
            GL.Color3(Color.Ivory);
            GL.Vertex3(0.5f, 0.0f, 0.5f);
            GL.End();
        }

        public void draw_cylinder(double radius,double height, double  R, double G, double B)
        {
            double x = 0.0;
            double y = 0.0;
            double angle = 0.0;
            double angle_stepsize = 0.1;

            /** Draw the tube */
            //GL.Color4(R,G,B,1.0);
            
            GL.Begin(PrimitiveType.QuadStrip);
            angle = 0.0;
            while (angle < 2 * Math.PI)
            {
                x = radius * Math.Cos(angle);
                y = radius * Math.Sin(angle);
                GL.Vertex3(x, y, height);
                GL.Vertex3(x, y, 0.0);
                angle = angle + angle_stepsize;
            }
            GL.Vertex3(radius, 0.0, height);
            GL.Vertex3(radius, 0.0, 0.0);
            GL.End();

            /** Draw the circle on top of cylinder */
            //GL.Color4(R+0.2, G+0.2, B+0.2,1.0);
            GL.Begin(PrimitiveType.Polygon);
            angle = 0.0;
            while (angle < 2 * Math.PI)
            {
                x = radius * Math.Cos(angle);
                y = radius * Math.Sin(angle);
                GL.Vertex3(x, y, height);
                angle = angle + angle_stepsize;
            }
            GL.Vertex3(radius, 0.0, height);
            GL.End();
        }

        int ResolucionEsfera = 20;
        float FinalPhi = 2 * (float)Math.PI;
        float FinalTeta = (float)Math.PI;

        public void MiEsfera()
        {
            int i = 0;
            int j = 0;
            float radio = 0.5f;
            float teta;
            float phi;
            float porcentajex = 0;
            float porcentajey = 1;

            float incrementox = (float)(1 / ((float)2 * ResolucionEsfera)); //1/20
            float incrementoy = (float)(-1 / ((float)ResolucionEsfera)); //1/10
            float increRad = (float)(Math.PI / ResolucionEsfera);
            float Vertice1x, Vertice1y, Vertice1z = 0;
            float Vertice2x, Vertice2y, Vertice2z = 0;
            float Vertice3x, Vertice3y, Vertice3z = 0;
            float Vertice4x, Vertice4y, Vertice4z = 0;
            //-----------\/---------Construcción de la esfera poco a poco---------------
            if (FinalPhi < 2 * (float)Math.PI) FinalPhi += increRad / 50;/*FinalPhi=2*PI;*/
            if (FinalTeta < (float)Math.PI) FinalTeta += increRad / 100;/*FinalTeta=(float)PI;*/
            //-----------/\---------Construcción de la esfera poco a poco---------------
            for (teta = 0; teta < FinalTeta; teta += increRad)
            {
                for (phi = 0; phi < FinalPhi; phi += increRad)
                {
                    //VERTICE 1
                    Vertice1z = (radio) * ((float)Math.Sin(teta)) * ((float)Math.Cos(phi));
                    Vertice1x = (radio) * ((float)Math.Sin(teta)) * ((float)Math.Sin(phi));
                    Vertice1y = (radio) * ((float)Math.Cos(teta));
                    //VERTICE 2
                    Vertice2z = (radio) * ((float)Math.Sin(teta + increRad)) * ((float)Math.Cos(phi));
                    Vertice2x = (radio) * ((float)Math.Sin(teta + increRad)) * ((float)Math.Sin(phi));
                    Vertice2y = (radio) * ((float)Math.Cos(teta + increRad));
                    //VERTICE 3
                    Vertice3z = (radio) * ((float)Math.Sin(teta + increRad)) * ((float)Math.Cos(phi + increRad));
                    Vertice3x = (radio) * ((float)Math.Sin(teta + increRad)) * ((float)Math.Sin(phi + increRad));
                    Vertice3y = (radio) * ((float)Math.Cos(teta + increRad));
                    //VERTICE 4
                    Vertice4z = (radio) * ((float)Math.Sin(teta)) * ((float)Math.Cos(phi + increRad));
                    Vertice4x = (radio) * ((float)Math.Sin(teta)) * ((float)Math.Sin(phi + increRad));
                    Vertice4y = (radio) * ((float)Math.Cos(teta));

                    GL.Normal3((float)1.5 * (float)Math.Sin(teta) * (float)Math.Sin(phi), (float)1.5 * (float)Math.Cos(teta), (float)1.5 * (float)Math.Sin(teta) * (float)Math.Cos(phi));
                    GL.Begin(PrimitiveType.Triangles);
                    //TRIANGULO 1
                    GL.TexCoord2(porcentajex, porcentajey); GL.Vertex3(Vertice1x, Vertice1y, Vertice1z);
                    GL.TexCoord2(porcentajex, porcentajey + incrementoy); GL.Vertex3(Vertice2x, Vertice2y, Vertice2z);
                    GL.TexCoord2(porcentajex + incrementox, porcentajey + incrementoy); GL.Vertex3(Vertice3x, Vertice3y, Vertice3z);
                    //TRIANGULO
                    GL.TexCoord2(porcentajex, porcentajey); GL.Vertex3(Vertice1x, Vertice1y, Vertice1z);
                    GL.TexCoord2(porcentajex + incrementox, porcentajey + incrementoy); GL.Vertex3(Vertice3x, Vertice3y, Vertice3z);
                    GL.TexCoord2(porcentajex + incrementox, porcentajey); GL.Vertex3(Vertice4x, Vertice4y, Vertice4z);

                    GL.End();

                    porcentajex += incrementox;
                }
                porcentajey += incrementoy;
                porcentajex = 0;
            }

        }

    }




}
