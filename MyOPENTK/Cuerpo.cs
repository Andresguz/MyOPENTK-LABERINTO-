using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using System;
using OpenTK;
namespace MyOPENTK
{
    public class Cuerpo//partícula
    {   //atributos
        public double masa;
        //var de rebte
        public double reboteX = 1.0;
        public double reboteY = 1.0;

        //avisa si colisiono
        public bool Existe_colision;

        public double ttl; //Tiempo vida inicial
        public double ttl_restante; //Tiempo vida restante

        public bool estaConVida = true;
        public double transparencia = 1.0;

        //Coordenadas
        public Vector3d position; // position(x,y,z) //public double x;//public double y; //public double z;
        public Vector3d velo;// velocidad(dirx, diry, dirz)
        public double V0;// inicial

        //Dimensiones
        public double ancho;
        public double alto;

        public double th = 0.0;
        public double gravedad = 0.9;
        public double Angulo;
        public double angulo_2;
        public double r = 0.0;
        public double g = 0.0;
        public double b = 0.0;
        //public double profundidad;
        static Random azar = new Random();

        public double t;

        //constructor inicializa datos del CUeRPO
        public Cuerpo(double cx, double cy)
        {

            position.X = cx;
            position.Y = cy;
            masa = azar.NextDouble() * 0.3 + 0.1;
            ancho = masa / 10;
            alto = ancho;
            ttl = azar.Next(1, 150);
            ttl_restante = ttl;
            Existe_colision = false;
            V0 = azar.NextDouble() * 0.8 + 0.1;
            t = 0.0;
            Angulo = azar.NextDouble() * 136.0 + 45.0;
            angulo_2 = azar.NextDouble() * 360;
            r = azar.NextDouble() * 0.5 * 1.0;
            g = azar.NextDouble() * 0.5 * 1.0;
            b = azar.NextDouble() * 0.5 * 1.0;
            //velocidadX = azar.NextDouble()/30;//[0.01---0.05]
            //velocidadY = azar.NextDouble() / 30;
        }

    }
}
