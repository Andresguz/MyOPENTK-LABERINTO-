using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Windows;

using System.Drawing;
//using MyOPENTK;
using OpenTK.Input;
using System.Linq;
using OpenTK.Graphics;

namespace MyOPENTK
{
    class BasicWindow : GameWindow
    {
        bool menu = false;
        bool win = false;
        bool lose = false;  
        //contadores
        public float tiempo = 5000;
        bool stard = false;
        public int pts;
        //Fuentres de texto 
        TextRenderer renderer, renderer1, renderer2;

        Font serif = new Font(FontFamily.GenericSerif, 64);
        Font sans = new Font(FontFamily.GenericSansSerif, 11);
        Font mono = new Font(FontFamily.GenericMonospace, 15);
        // TextRenderer renderer;
        public double dirx;
        public double diry;
        public double dirz;

        int cont = 0;
        bool suma=false; 
        Cuerpo[] o1 = new Cuerpo[100];
        int[,] mapa =  {
            //   0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1},//0
                {1,4,0,0,0,0,1,0,0,1,0,0,1,0,0,1,0,0,2,1},//1
                {1,0,0,0,0,8,1,0,6,1,0,11,1,0,11,1,0,0,9,1},//2
                {1,0,9,0,0,0,1,0,0,1,0,0,1,0,0,1,0,0,0,1},//3
                {1,9,0,0,0,0,1,0,0,0,0,0,0,0,0,1,0,0,0,1},//4
                {1,9,9,0,0,0,1,0,0,0,0,0,0,0,0,1,0,0,0,1},//5
                {1,1,1,1,0,0,1,1,1,0,0,1,1,1,0,1,0,0,0,1},//6
                {1,0,0,0,0,0,1,1,1,7,7,1,1,1,1,1,1,0,0,1},//7
                {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1},//8
                {1,9,9,0,0,0,0,9,0,0,0,0,0,0,0,0,0,0,0,1},//9
                {1,0,0,0,0,0,0,0,0,0,9,0,9,0,0,0,0,0,0,1},//10
                {1,1,1,1,1,1,1,0,0,0,9,0,0,0,1,0,0,0,0,1},//11
                {1,0,0,0,0,9,1,0,0,0,0,0,0,0,1,0,0,0,0,1},//12
                {1,0,8,0,0,0,1,0,0,0,1,1,1,1,1,1,0,0,0,1},//13
                {1,0,0,0,0,9,1,0,0,0,1,0,0,9,9,1,0,0,1,1},//14
                {1,0,0,1,1,1,1,0,0,0,1,0,0,0,0,0,0,0,9,1},//15
                {1,0,0,0,0,0,0,0,0,0,1,0,0,0,0,0,8,0,0,1},//16
                {1,0,0,0,0,0,0,1,0,0,1,0,0,0,0,0,0,0,0,1},//17
                {1,0,0,0,9,9,0,1,0,0,0,0,0,0,0,0,9,9,0,1},//18
                {1,5,0,0,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,1},//19
                {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1}//20
        };
        //player
        int cpX =1;
        int cpY =19; 
        int cpX1 =1;
        int cpY1 =19;
        //enemigo
        double velocidadEnemyX;
        double velocidadEnemyY;
        int epX = 17;
        int epY = 18;

        int epX1 = 1;
        int epY1 = 1;

        int epX2 = 18;
        int epY2 = 1;
        //llaves
        int l1x=2;
        int l1y=13;

        int l2x=16;
        int l2y=16;
        //puerta
        int p1x=9;
        int p1y=7;

        int p2x=10;
        int p2y=7;

        //caja
        int b1x=2;
        int b1y=13;
        int b2x=17;
        int b2y=18;
        //mobiles
        int m1x =8;
        int m1y=2;
        int m2x=11;
        int m2y=2;
        int m3x = 14;
        int m3y = 2;

        int valor;
        int direccionX;
        int direccionY;


        double mx=0.0;
        double my=0.0;
        Random azar = new Random();
        #region CAMARA
        private double camPitch, camYaw, camX, camY, camZ;//camara
        #endregion
        double _rotar = 0.0;
        Mesh _objectMesh;
        int textura_Pared;
        int textura_piso;
        int textura_enemy;
        int textura_llave;
        int textura_puerta;
        int textura_caja;
        int texture_personaje;
        int texture_r1;
        int texture_r2;
        int texture_r3;
        int texture_control;
        int texture_acer;
        int texture_win;
        int texture_lose;
        int texture_menu;

        #region Luz_roja
        float[] lR_position = { -1f, 1.0f, 1.0f, 1.0f };
        float[] lR_ambient = { 0.8f, 0.0f, 0.0f, 1.0f };
        float[] lR_diffuse = { 0.6f, 0.0f, 0.0f, 1.0f };
        float[] lR_specular = { 1.0f, 1.0f, 1.0f, 1.0f };
        #endregion
        #region Luz_verde
        float[] lG_position = { 0.0f, 0.5f, 0.0f, 1.0f };
        float[] lG_ambient = { 0.0f, 0.5f, 0.0f, 1.0f };
        float[] lG_diffuse = { 0.0f, 0.6f, 0.0f, 1.0f };
        float[] lG_specular = { 1.0f, 1.0f, 1.0f, 1.0f };
        #endregion
        #region Luz_azul
        float[] lB_position = { 0.5f, 0.5f, 0.0f, 1.0f };
        float[] lB_ambient = { 0.0f, 0.0f, 0.5f, 1.0f };
        float[] lB_diffuse = { 0.0f, 0.0f, 0.6f, 1.0f };
        float[] lB_specular = { 1.0f, 1.0f, 1.0f, 1.0f };
        #endregion
        #region Luz_amarilla
        float[] lY_position = { -0.5f, -0.5f, 0.0f, 1.0f };
        float[] lY_ambient = { 0.5f, 0.5f, 0.0f, 1.0f };
        float[] lY_diffuse = { 0.3f, 0.3f, 0.0f, 1.0f };
        float[] lY_specular = { 1.0f, 1.0f, 1.0f, 1.0f };
        #endregion
        #region Luz_purpura
        double cXlP = 0.0, cYlP = -0.5, cZlP = 0.0;
        float[] lP_position = { 0.0f, 0.0f, 0.0f, 1.0f };
        float[] lP_ambient = { 0.5f, 0.0f, 0.5f, 1.0f };
        float[] lP_diffuse = { 0.6f, 0.0f, 0.6f, 1.0f };
        float[] lP_specular = { 1.0f, 1.0f, 1.0f, 1.0f };
        #endregion
        #region Luz_blanca
        float[] lW_position = { 0.0f, -0.5f, -1.0f, 1.0f };
        float[] lW_ambient = { 0.9f, 0.9f, 0.9f, 1.0f };
        float[] lW_diffuse = { 0.3f, 0.3f, 0.3f, 1.0f };
        float[] lW_specular = { 1.0f, 1.0f, 1.0f, 1.0f };
        #endregion

        Shapes MiFigura;
        double pX = 0.0, pY = 0.0, pZ = 0.0;
        double fs = 1.0;
        bool estado_mouse_Presionado = false;
        int c_mouseX_old = 0;
        int c_mouseX_actual = 0;
        int c_mouseY_old = 0;
        int c_mouseY_actual = 0;
        int c_mouseZ_old = 0;
        int c_mouseZ_actual = 0;

        //Constructor
        public BasicWindow() : base(640, 480)
        {

            for (int i = 0; i < o1.Length; i++)
            {
                // double grados = azar.Next(45, 91); //caso de 10 objetos 36grados

                //double cx =  Math.Cos((Math.PI/180)*(grados));
                //double cy =  Math.Sin((Math.PI/180)*(grados));
                double cx = 0.0;
                double cy = 0.0;

                o1[i] = new Cuerpo(cx, cy); // genera la instancia de los cuerpos

                o1[i].position.X = cx;
                o1[i].position.Y = cy;



                o1[i].velo.X = cx;
                o1[i].velo.Y = cy;
                o1[i].Existe_colision = false;
            }

            MiFigura = new Shapes();//creado la instancia

            KeyDown += LeerTeclado;
            MouseDown += Mouse_Presionado;
            MouseUp += Mouse_Soltado;
            MouseMove += Mouse_Mover;
            MouseWheel += Mouse_Rueda;

        }

        private void Mouse_Rueda(object sender, OpenTK.Input.MouseWheelEventArgs e)
        {
            c_mouseZ_actual = e.Value;
            if (c_mouseZ_actual > c_mouseZ_old)
            {
                cZlP += 0.1;
            }
            if (c_mouseZ_actual < c_mouseZ_old)
            {
                cZlP -= 0.1;
            }
            c_mouseZ_old = c_mouseZ_actual;
        }

        private void Mouse_Mover(object sender, OpenTK.Input.MouseMoveEventArgs e)
        {
            c_mouseX_actual = e.X;
            c_mouseY_actual = e.Y;
            if (estado_mouse_Presionado)
            {
                if (c_mouseX_actual > c_mouseX_old) camYaw += 0.3;
                if (c_mouseX_actual < c_mouseX_old) camYaw -= 0.3;
                if (c_mouseY_actual > c_mouseY_old) camPitch -= 0.2;
                if (c_mouseY_actual < c_mouseY_old) camPitch += 0.2;
            }
            //actualizar
            c_mouseX_old = c_mouseX_actual;
            c_mouseY_old = c_mouseY_actual;
        }

        private void Mouse_Soltado(object sender, OpenTK.Input.MouseButtonEventArgs e)
        {
            estado_mouse_Presionado = false;
        }

        private void Mouse_Presionado(object sender, OpenTK.Input.MouseButtonEventArgs e)
        {
            estado_mouse_Presionado = true;
            c_mouseX_old = e.X;
            c_mouseY_old = e.Y;
        }

        private void LeerTeclado(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
        {
            //if (e.Key == OpenTK.Input.Key.A)//MOVER IZQ
            //{
            //    camX += 0.1;
            //}
            //if (e.Key == OpenTK.Input.Key.D)//MOVER DER
            //{
            //    camX -= 0.1;
            //}
            //if (e.Key == OpenTK.Input.Key.W)//AVANZA ADELANTE (EJE Z)
            //{
            //    camZ += 0.1;
            //}
            //if (e.Key == OpenTK.Input.Key.S)//RETROCEDE (EJE Z)
            //{
            //    camZ -= 0.1;
                
            //}
            if (e.Key == OpenTK.Input.Key.Escape)//RETROCEDE (EJE Z)
            {
               
                this.Exit();
            }
            if (e.Key == OpenTK.Input.Key.Space)
            {
                menu = true;
            }
            if (menu ==true)
            {
                if (e.Key == OpenTK.Input.Key.D)
                {
                    if (mapa[cpY, cpX + 1] == 0)
                    {
                        mx += 0.08;
                        camX -= 0.008;
                    }
                    if (mx > 1.9)
                    {
                        if (mapa[cpY, cpX + 1] == 0)
                        {
                            mapa[cpY, cpX + 1] = 5;
                            mapa[cpY, cpX] = 0;
                            cpX += 1;
                        }
                        mx = 0.0;
                        if (mapa[cpY, cpX + 1] == 1)
                        {
                            mx = 0.0;
                        }


                    }

                }


                if (e.Key == OpenTK.Input.Key.A)
                {
                    if (mapa[cpY, cpX - 1] == 0)
                    {
                        mx -= 0.08;
                        camX += 0.008;
                    }

                    if (mx < -1.9)
                    {
                        if (mapa[cpY, cpX - 1] == 0)
                        {
                            mapa[cpY, cpX - 1] = 5;
                            mapa[cpY, cpX] = 0;
                            cpX -= 1;
                        }
                        mx = 0.0;
                        if (mapa[cpY, cpX - 1] == 1)
                        {
                            mx = 0.0;
                        }


                    }
                }
                if (e.Key == OpenTK.Input.Key.W)
                {


                    if (mapa[cpY - 1, cpX] == 0)
                    {
                        my += 0.08;
                        camY -= 0.008;
                    }

                    if (my > 1.9)
                    {
                        if (mapa[cpY - 1, cpX] == 0)
                        {
                            mapa[cpY - 1, cpX] = 5;
                            mapa[cpY, cpX] = 0;
                            cpY -= 1;
                        }
                        my = 0.0;
                        if (mapa[cpY - 1, cpX] == 1)
                        {
                            my = 0.0;
                        }


                    }
                }
                if (e.Key == OpenTK.Input.Key.S)
                {

                    if (mapa[cpY + 1, cpX] == 0)
                    {
                        my -= 0.08;
                        camY += 0.008;
                    }

                    if (my < -1.9)
                    {
                        if (mapa[cpY + 1, cpX] == 0)
                        {
                            mapa[cpY + 1, cpX] = 5;
                            mapa[cpY, cpX] = 0;
                            cpY += 1;
                        }
                        my = 0.0;
                        if (mapa[cpY + 1, cpX] == 1)
                        {
                            my = 0.0;
                        }


                    }
                }
            }
                
        }
        

       

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            //if (stard)
            {
                tiempo -= 1;
            }

            // renderer1 = new TextRenderer(120, 130);
            PointF position1 = PointF.Empty;
            ///
            renderer1.Clear(Color.Blue);
            renderer1.ToString();
            renderer1.DrawString("LLAVES: " + pts.ToString(), sans, Brushes.Yellow, position1);
            position1.Y += serif.Height;

            ///
          //  renderer2 = new TextRenderer(190, 20);
            PointF position2 = PointF.Empty;
            ///
            renderer2.Clear(Color.Yellow);
            renderer2.ToString();
            renderer2.DrawString("Tiempo : " + tiempo.ToString(), sans, Brushes.Black, position2);
            position2.Y += serif.Height;


            _rotar += 0.7;
            #region particulas
            for (int k = 0; k < o1.Length; k++)
            {


                //incrementar dirx / diry
                o1[k].position.X += o1[k].t * o1[k].V0 * Math.Cos((Math.PI / 180) * o1[k].Angulo) * Math.Sin((Math.PI / 180) * o1[k].angulo_2);
                o1[k].position.Y += o1[k].t * o1[k].V0 * Math.Sin((Math.PI / 180) * o1[k].Angulo) - 0.5 * 9.81 * o1[k].t * o1[k].t;
                o1[k].position.Z += o1[k].t * o1[k].V0 * Math.Sin((Math.PI / 180) * o1[k].Angulo) * Math.Cos((Math.PI / 180) * o1[k].angulo_2);

                o1[k].t += 0.001;

                if (o1[k].position.Y < -1.0)
                {
                    reset(k);


                }

                if (o1[k].position.Y < 0.0)
                {
                    o1[k].Existe_colision = true;
                }


                ////decrementa el tiempo de vida
                if (o1[k].Existe_colision)
                {
                    o1[k].ttl_restante -= 1;
                    //porcentaje de Transparencia
                    o1[k].transparencia = o1[k].ttl_restante / o1[k].ttl;

                }
                if (o1[k].ttl_restante <= 0)
                {
                    o1[k].estaConVida = false;//muere



                }

            }
            for (int k = 0; k < o1.Length; k++)
            {


                //incrementar dirx / diry
                o1[k].position.X += o1[k].t * o1[k].V0 * Math.Cos((Math.PI / 180) * o1[k].Angulo) * Math.Sin((Math.PI / 180) * o1[k].angulo_2);
                o1[k].position.Y += o1[k].t * o1[k].V0 * Math.Sin((Math.PI / 180) * o1[k].Angulo) - 0.5 * 9.81 * o1[k].t * o1[k].t;
                o1[k].position.Z += o1[k].t * o1[k].V0 * Math.Sin((Math.PI / 180) * o1[k].Angulo) * Math.Cos((Math.PI / 180) * o1[k].angulo_2);

                o1[k].t += 0.001;

                if (o1[k].position.Y < -1.0)
                {
                    reset(k);


                }

                if (o1[k].position.Y < 0.0)
                {
                    o1[k].Existe_colision = true;
                }


                ////decrementa el tiempo de vida
                if (o1[k].Existe_colision)
                {
                    o1[k].ttl_restante -= 1;
                    //porcentaje de Transparencia
                    o1[k].transparencia = o1[k].ttl_restante / o1[k].ttl;

                }
                if (o1[k].ttl_restante <= 0)
                {
                    o1[k].estaConVida = false;//muere



                }

            }
            #endregion
            if (menu==true)
            {
                valor = azar.Next(0, 100);

                if (valor == 5 || valor == 10)
                {
                    direccionX = valor;
                    velocidadEnemyX = 0;
                }
                if (valor == 8 || valor == 9)
                {
                    direccionY = valor;
                    velocidadEnemyY = 0;
                }
                if (valor == 2 || valor == 3)
                {
                    direccionX = valor;
                    velocidadEnemyX = 0;
                }
                if (valor == 1 || valor == 11)
                {
                    direccionY = valor;
                    velocidadEnemyY = 0;
                }

                if (valor == 6 || valor == 7)
                {
                    direccionX = valor;
                    velocidadEnemyX = 0;

                }
                if (valor == 12 || valor == 13)
                {
                    direccionY = valor;
                    velocidadEnemyY = 0;
                }

                if (direccionX == 5)
                {//avanza derecha
                 //velocidadEnemyX += 0.01;
                 //if (velocidadEnemyX>1.0)
                    {
                        if (mapa[epY, epX + 1] == 0)
                        {
                            mapa[epY, epX + 1] = 3;
                            mapa[epY, epX] = 0;
                            epX += 1;
                        }
                    }
                }
                if (direccionX == 2)
                {//avanza derecha
                 //velocidadEnemyX += 0.01;
                 //if (velocidadEnemyX > 1.0)
                    {
                        if (mapa[epY1, epX1 + 1] == 0)
                        {
                            mapa[epY1, epX1 + 1] = 4;
                            mapa[epY1, epX1] = 0;
                            epX1++;
                        }
                    }
                }
                if (direccionX == 6)
                {//avanza derecha
                 //velocidadEnemyX += 0.01;
                 //if (velocidadEnemyX > 1.0)
                    {
                        if (mapa[epY2, epX2 + 1] == 0)
                        {
                            mapa[epY2, epX2 + 1] = 2;
                            mapa[epY2, epX2] = 0;
                            epX2++;
                        }
                    }
                }
                if (direccionX == 10)
                {//avanza izquierda   
                 //velocidadEnemyX += 0.01;
                 //if (velocidadEnemyX > 1.0)
                    {
                        if (mapa[epY, epX - 1] == 0)
                        {
                            mapa[epY, epX - 1] = 3;
                            mapa[epY, epX] = 0;
                            epX -= 1;
                        }
                    }

                }
                if (direccionX == 3)
                {//avanza izquierda
                 //velocidadEnemyX += 0.01;
                 //if (velocidadEnemyX > 1.0)
                    {
                        if (mapa[epY1, epX1 - 1] == 0)
                        {
                            mapa[epY1, epX1 - 1] = 4;
                            mapa[epY1, epX1] = 0;
                            epX1--;
                        }
                    }
                }
                if (direccionX == 7)
                {//avanza izquierda
                 //velocidadEnemyX += 0.01;
                 //if (velocidadEnemyX > 1.0)
                    {
                        if (mapa[epY2, epX2 - 1] == 0)
                        {
                            mapa[epY2, epX2 - 1] = 2;
                            mapa[epY2, epX2] = 0;
                            epX2--;
                        }
                    }
                }
                /////////////////
                if (direccionY == 8)
                {
                    //velocidadEnemyY += 0.01;
                    //if (velocidadEnemyY > 1.0)
                    {
                        if (mapa[epY - 1, epX] == 0)
                        {
                            mapa[epY - 1, epX] = 3;
                            mapa[epY, epX] = 0;
                            epY -= 1;
                        }
                    }
                }
                if (direccionY == 1)
                {
                    //velocidadEnemyY += 0.01;
                    //if (velocidadEnemyY > 1.0)
                    {
                        if (mapa[epY1 - 1, epX1] == 0)
                        {
                            mapa[epY1 - 1, epX1] = 4;
                            mapa[epY1, epX1] = 0;
                            epY1--;
                        }
                    }
                }
                if (direccionY == 12)
                {
                    //velocidadEnemyY += 0.01;
                    //if (velocidadEnemyY > 1.0)
                    {
                        if (mapa[epY2 - 1, epX2] == 0)
                        {
                            mapa[epY2 - 1, epX2] = 2;
                            mapa[epY2, epX2] = 0;
                            epY2--;
                        }
                    }
                }
                ////
                if (direccionY == 9)
                {
                    //velocidadEnemyY += 0.01;
                    //if (velocidadEnemyY > 1.0)
                    {
                        if (mapa[epY + 1, epX] == 0)
                        {
                            mapa[epY + 1, epX] = 3;
                            mapa[epY, epX] = 0;
                            epY += 1;
                        }
                    }

                }
                if (direccionY == 11)
                {
                    //velocidadEnemyY += 0.01;
                    //if (velocidadEnemyY > 1.0)
                    {
                        if (mapa[epY1 + 1, epX1] == 0)
                        {
                            mapa[epY1 + 1, epX1] = 4;
                            mapa[epY1, epX1] = 0;
                            epY1++;
                        }
                    }
                }
                if (direccionY == 13)
                {
                    //velocidadEnemyY += 0.01;
                    //if (velocidadEnemyY > 1.0)
                    {
                        if (mapa[epY2 + 1, epX2] == 0)
                        {
                            mapa[epY2 + 1, epX2] = 2;
                            mapa[epY2, epX2] = 0;
                            epY2++;
                        }
                    }
                }
            }
            

        }

        public void reset(int k)
        {
            o1[k].position.X = 0.0;
            o1[k].position.Y = 0.0;
            o1[k].position.Z = 0.0;
            o1[k].V0 = azar.NextDouble() * 0.8 + 0.1;
            o1[k].t = 0.0;
            o1[k].Angulo = azar.NextDouble() * 96.0 + 85.0;
            o1[k].transparencia = 1.0;
            // o1[k].masa = azar.NextDouble() * 0.5 + 0.3;
            o1[k].angulo_2 = azar.NextDouble() * 180;
            o1[k].r = azar.NextDouble() * 0.5 * 1.0;
            o1[k].g = azar.NextDouble() * 0.5 * 1.0;
            o1[k].b = azar.NextDouble() * 0.5 * 1.0;
            o1[k].estaConVida = true;
            o1[k].ttl = azar.Next(1, 150);
            o1[k].ttl_restante = o1[k].ttl;
            //  o1[k].Existe_colision=false;
        }
        //Método ONLOAD
        protected override void OnLoad(EventArgs e)
        {
  

            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            GL.Enable(EnableCap.DepthTest);// REVISA LA PROFUNDIDAD
        
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            //luz roja
            GL.Light(LightName.Light0, LightParameter.Position, lR_position);
            GL.Light(LightName.Light0, LightParameter.Ambient, lR_ambient);
            GL.Light(LightName.Light0, LightParameter.Diffuse, lR_diffuse);
            GL.Light(LightName.Light0, LightParameter.Specular, lR_specular);
            //luz verde
            GL.Light(LightName.Light1, LightParameter.Position, lG_position);
            GL.Light(LightName.Light1, LightParameter.Ambient, lG_ambient);
            GL.Light(LightName.Light1, LightParameter.Diffuse, lG_diffuse);
            GL.Light(LightName.Light1, LightParameter.Specular, lG_specular);
            //luz azul
            GL.Light(LightName.Light2, LightParameter.Position, lB_position);
            GL.Light(LightName.Light2, LightParameter.Ambient, lB_ambient);
            GL.Light(LightName.Light2, LightParameter.Diffuse, lB_diffuse);
            GL.Light(LightName.Light2, LightParameter.Specular, lB_specular);
            //luz amarilla
            GL.Light(LightName.Light3, LightParameter.Position, lY_position);
            GL.Light(LightName.Light3, LightParameter.Ambient, lY_ambient);
            GL.Light(LightName.Light3, LightParameter.Diffuse, lY_diffuse);
            GL.Light(LightName.Light3, LightParameter.Specular, lY_specular);
            //luz purpura
            GL.Light(LightName.Light4, LightParameter.Position, lP_position);
            GL.Light(LightName.Light4, LightParameter.Ambient, lP_ambient);
            GL.Light(LightName.Light4, LightParameter.Diffuse, lP_diffuse);
            GL.Light(LightName.Light4, LightParameter.Specular, lP_specular);
            //luz BLANCA
            GL.Light(LightName.Light5, LightParameter.Position, lW_position);
            GL.Light(LightName.Light5, LightParameter.Ambient, lW_ambient);
            GL.Light(LightName.Light5, LightParameter.Diffuse, lW_diffuse);
            GL.Light(LightName.Light5, LightParameter.Specular, lW_specular);
            //HABILITAR LA ILUMINACION

            GL.Enable(EnableCap.Normalize);
            GL.ShadeModel(ShadingModel.Smooth);

            GL.Enable(EnableCap.Lighting);
            GL.Enable(EnableCap.Light0);
            GL.Enable(EnableCap.Light1);
            GL.Enable(EnableCap.Light2);
            GL.Enable(EnableCap.Light3);
            GL.Enable(EnableCap.Light4);
          //  GL.Enable(EnableCap.Light5);

            GL.Enable(EnableCap.Texture2D);//habilita textura
            //carga del Mesh
            if (!ObjectLoader.GetMesh(@"..\..\Files\Johnny.obj", @"Files", out _objectMesh))
                Console.WriteLine(@"The file is Invalid");
            else
            {
                for (int i = 0; i < _objectMesh.Textures.Count; i++)
               ////_objectMesh.Textures[_objectMesh.Textures.ElementAt(i).Key] = TextureLoader.LoadTexture(_objectMesh.Textures.ElementAt(i).Key);
                ////**NUEVO**//
                ObjectLoader.Normalize(_objectMesh.Vertices, _objectMesh.MaxVertex, _objectMesh.MinVertex);
                ObjectLoader.ComputePolygonNormnal(ref _objectMesh);
                ObjectLoader.ComputeVertexNormal(ref _objectMesh);
            }
            //// cargar textura
            textura_Pared = TextureLoader.LoadTexture(@"..\..\Files\wall.jpg");
            textura_piso = TextureLoader.LoadTexture(@"..\..\Files\floor.jpg");
            textura_puerta = TextureLoader.LoadTexture(@"..\..\Files\p.jpg");
            textura_llave = TextureLoader.LoadTexture(@"..\..\Files\key.png");
            textura_caja = TextureLoader.LoadTexture(@"..\..\Files\box.jpg");
            textura_enemy = TextureLoader.LoadTexture(@"..\..\Files\enemy.png"); 
            texture_r1 = TextureLoader.LoadTexture(@"..\..\Files\contra.png"); 
            texture_r2 = TextureLoader.LoadTexture(@"..\..\Files\r2.png"); 
            texture_r3 = TextureLoader.LoadTexture(@"..\..\Files\r3.png"); 
            texture_control = TextureLoader.LoadTexture(@"..\..\Files\progra.png"); 
            texture_acer = TextureLoader.LoadTexture(@"..\..\Files\acer.png"); 
            texture_win = TextureLoader.LoadTexture(@"..\..\Files\WIN.png"); 
            texture_lose = TextureLoader.LoadTexture(@"..\..\Files\lose.png"); 
            texture_menu = TextureLoader.LoadTexture(@"..\..\Files\MENU.png"); 
            texture_personaje = TextureLoader.LoadTexture(@"..\..\Files\Johnny\johnny.jpg");


            //renderizA EL TEXTO
            renderer = new TextRenderer(Width, Height);
            PointF position = PointF.Empty;
            ///
            renderer.Clear(Color.Black);
            renderer.ToString();
            renderer.DrawString("Andres Guzman", serif, Brushes.Red, position);
            position.Y += serif.Height;
            //renderer.DrawString("The quick brown fox jumps over the lazy dog", sans, Brushes.White, position);
            //position.Y += sans.Height;
            //renderer.DrawString("The quick brown fox jumps over the lazy dog", mono, Brushes.White, position);
            //position.Y += mono.Height;
            //////////////
            ///
            renderer1 = new TextRenderer(100, 100);
            PointF position1 = PointF.Empty;
            ///
            renderer1.Clear(Color.Blue);
            renderer1.ToString();
            renderer1.DrawString("puntos", sans, Brushes.Yellow, position1);
            position1.Y += serif.Height;

            //////////////
            ///
            renderer2 = new TextRenderer(1100, 20);
            PointF position2 = PointF.Empty;
            ///
            renderer2.Clear(Color.Yellow);
            renderer2.ToString();
            // renderer2.DrawString("Tiempo ", mono, Brushes.Black, position2);
            position2.Y += serif.Height;
        }

        //Método ONREZISE
        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            float aspect_ratio = (float)Width / (float)Height;
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspect_ratio, 0.01f, 64.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);
            //GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0,5.0);
        }

        public void Cube(int tx0, int tx1, int tx2)
        {//frontal

            GL.BindTexture(TextureTarget.Texture2D, tx0);
            GL.PushMatrix();
            GL.Begin(PrimitiveType.Quads);
            GL.Color4(1.0, 1.0, 1.0, 1.0);
            GL.TexCoord2(0.0f, 0.0f);
            GL.Vertex3(0.5, 0.5, 0.5);//P0
            GL.TexCoord2(1.0f, 0.0f);
            GL.Vertex3(0.5, -0.5, 0.5);//P1
            GL.TexCoord2(1.0f, 1.0f);
            GL.Vertex3(-0.5, -0.5, 0.5);//P2
            GL.TexCoord2(0.0f, 1.0f);
            GL.Vertex3(-0.5, 0.5, 0.5);//P3
            GL.End();

            //derecha
            GL.Begin(PrimitiveType.Quads);
            GL.Color4(1.0, 1.0, 1.0, 1.0);
            GL.TexCoord2(0.0f, 0.0f);
            GL.Vertex3(0.5, 0.5, -0.5);//P4
            GL.TexCoord2(1.0f, 0.0f);


            GL.Vertex3(0.5, -0.5, -0.5);//P5
            GL.TexCoord2(1.0f, 1.0f);
            GL.Vertex3(0.5, -0.5, 0.5);//P1
            GL.TexCoord2(0.0f, 1.0f);

            GL.Vertex3(0.5, 0.5, 0.5);//P0

            GL.End();
            GL.PopMatrix();
            GL.BindTexture(TextureTarget.Texture2D, tx1);
            GL.PushMatrix();
            //arriba
            GL.Begin(PrimitiveType.Quads);
            GL.Color4(1.0, 1.0, 1.0, 1.0);
            GL.TexCoord2(0.0f, 0.0f);

            GL.Vertex3(0.5, 0.5, -0.5);//P4
            GL.TexCoord2(1.0f, 0.0f);
            GL.Vertex3(0.5, 0.5, 0.5);//P0
            GL.TexCoord2(1.0f, 1.0f);
            GL.Vertex3(-0.5, 0.5, 0.5);//P3
            GL.TexCoord2(0.0f, 1.0f);
            GL.Vertex3(-0.5, 0.5, -0.5);//P7
            GL.End();

            //izquierda


            GL.Begin(PrimitiveType.Quads);
            GL.Color4(1.0, 1.0, 1.0, 1.0);
            GL.TexCoord2(0.0f, 0.0f);

            GL.Vertex3(-0.5, 0.5, -0.5);//P4
            GL.TexCoord2(1.0f, 0.0f);
            GL.Vertex3(-0.5, 0.5, 0.5);//P0
            GL.TexCoord2(1.0f, 1.0f);
            GL.Vertex3(-0.5, -0.5, 0.5);//P3
            GL.TexCoord2(0.0f, 1.0f);
            GL.Vertex3(-0.5, -0.5, -0.5);//P7
            GL.End();
            GL.PopMatrix();
            //abajo

            GL.BindTexture(TextureTarget.Texture2D, tx2);

            GL.Begin(PrimitiveType.Quads);
            GL.Color4(1.0, 1.0, 1.0, 1.0);
            GL.TexCoord2(0.0f, 0.0f);

            GL.Vertex3(0.5, -0.5, -0.5);//P4
            GL.TexCoord2(1.0f, 0.0f);
            GL.Vertex3(0.5, -0.5, 0.5);//P0
            GL.TexCoord2(1.0f, 1.0f);
            GL.Vertex3(-0.5, -0.5, 0.5);//P3
            GL.TexCoord2(0.0f, 1.0f);
            GL.Vertex3(-0.5, -0.5, -0.5);//P7
            GL.End();
            //atras

            GL.Begin(PrimitiveType.Quads);
            GL.Color4(1.0, 1.0, 1.0, 1.0);
            GL.TexCoord2(0.0f, 0.0f);

            GL.Vertex3(0.5, 0.5, -0.5);//P4
            GL.TexCoord2(1.0f, 0.0f);
            GL.Vertex3(0.5, -0.5, -0.5);//P0
            GL.TexCoord2(1.0f, 1.0f);
            GL.Vertex3(-0.5, -0.5, -0.5);//P3
            GL.TexCoord2(0.0f, 1.0f);
            GL.Vertex3(-0.5, 0.5, -0.5);//P7
            GL.End();
        }

        #region cubo
        public void Draw_Cube(double O1ancho, double O1alto, double O1largo, float red, float green, float blue, float transparencia)
            {
                //Cara Fontal
                GL.Begin(PrimitiveType.Quads);
                GL.Color4(red - 0.2, green - 0.2, blue - 0.2, transparencia);
                GL.Vertex3(O1ancho, O1alto, O1largo);
                GL.Vertex3(O1ancho, -O1alto, O1largo);
                GL.Vertex3(-O1ancho, -O1alto, O1largo);
                GL.Vertex3(-O1ancho, O1alto, O1largo);
                GL.End();

                //Cara Trasera
                GL.Begin(PrimitiveType.Quads);
                GL.Color4(red - 0.2, green - 0.2, blue - 0.2, transparencia);
                GL.Vertex3(O1ancho, O1alto, -O1largo);
                GL.Vertex3(O1ancho, -O1alto, -O1largo);
                GL.Vertex3(-O1ancho, -O1alto, -O1largo);
                GL.Vertex3(-O1ancho, O1alto, -O1largo);
                GL.End();

                //Cara Izquierda
                GL.Begin(PrimitiveType.Quads);
                GL.Color4(red - 0.4, green - 0.4, blue - 0.4, transparencia);
                GL.Vertex3(-O1ancho, O1alto, O1largo);
                GL.Vertex3(-O1ancho, O1alto, -O1largo);
                GL.Vertex3(-O1ancho, -O1alto, -O1largo);
                GL.Vertex3(-O1ancho, -O1alto, O1largo);
                GL.End();

                //Cara Derecha
                GL.Begin(PrimitiveType.Quads);
                GL.Color4(red - 0.4, green - 0.4, blue - 0.4, transparencia);
                GL.Vertex3(O1ancho, O1alto, O1largo);
                GL.Vertex3(O1ancho, O1alto, -O1largo);
                GL.Vertex3(O1ancho, -O1alto, -O1largo);
                GL.Vertex3(O1ancho, -O1alto, O1largo);
                GL.End();

                //Cara Arriba
                GL.Begin(PrimitiveType.Quads);
                GL.Color4(red, green, blue, transparencia);
                GL.Vertex3(O1ancho, O1alto, O1largo);
                GL.Vertex3(O1ancho, O1alto, -O1largo);
                GL.Vertex3(-O1ancho, O1alto, -O1largo);
                GL.Vertex3(-O1ancho, O1alto, O1largo);
                GL.End();

                //Cara Abajo
                GL.Begin(PrimitiveType.Quads);
                GL.Color4(red, green, blue, transparencia);
                GL.Vertex3(O1ancho, -O1alto, O1largo);
                GL.Vertex3(O1ancho, -O1alto, -O1largo);
                GL.Vertex3(-O1ancho, -O1alto, -O1largo);
                GL.Vertex3(-O1ancho, -O1alto, O1largo);
                GL.End();
            }
        #endregion

        //MÉTODO DIBUJA EN PANTALLA

        protected override void OnUnload(EventArgs e)
        {
            renderer.Dispose();
            renderer1.Dispose();
            renderer2.Dispose();

        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            Matrix4 lookat = Matrix4.LookAt(Vector3.UnitZ * 3, Vector3.Zero, Vector3.UnitY);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);
            GL.LoadIdentity();
            
            //aqui la camara
           GL.Rotate(camPitch-90, 1, 0, 0);//inclinacion
          // GL.Rotate(camPitch, 1, 0, 0);//inclinacion
            GL.Rotate(camYaw, 0, 0, 1);//rotacion
            GL.Translate(camX+1.8, camY+1.8, camZ-0.1);//traslacion
                                                       //GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            if (menu==false)
            {
                //gamestar
                GL.PushMatrix();
                GL.BindTexture(TextureTarget.Texture2D, texture_menu);
                GL.Enable(EnableCap.Texture2D);
                GL.Rotate(90, 0, 0, 1);
                GL.Rotate(90, 1, 0, 0);
                //adelante,arriba,dereiz
                GL.Translate(-1.5, 0.12, -1.75);
                GL.Scale(0.2, 0.3, 0.2);
                MiFigura.Piso();
                GL.Disable(EnableCap.Texture2D);
                GL.PopMatrix();
            }
            if (tiempo < 0)
            {
                this.Exit();
            }

           

            ///////////////
            GL.PushMatrix();
            //textoooooooooooooo
            GL.Translate(0.2, 0.9, 1.0);
            GL.Rotate(90, 1, 0, 0);
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, renderer1.Texture);
            GL.Begin(BeginMode.Quads);
            GL.TexCoord2(0.0f, 0.3f); GL.Vertex2(-1.0f, -0.3f);
            GL.TexCoord2(1.0f, 0.3f); GL.Vertex2(1f, -0.3f);
            GL.TexCoord2(1.0f, 0.0f); GL.Vertex2(1f, 0.3f);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(-1f, 0.3f);
            GL.End();
            GL.PopMatrix();

            ///////////////
            GL.PushMatrix();
            //textoooooooooooooo
            GL.Translate(-1.5, 1, 1);
            GL.Scale(0.5, 0.5, 0.5);
        
            GL.Rotate(90, 1, 0, 0);
            GL.Enable(EnableCap.Texture2D);
            GL.BindTexture(TextureTarget.Texture2D, renderer2.Texture);
            GL.Begin(BeginMode.Quads);
            GL.TexCoord2(0.0f, 0.7f); GL.Vertex2(-1.0f, -0.3f);
            GL.TexCoord2(0.1f, 0.7f); GL.Vertex2(1f, -0.3f); ;
            GL.TexCoord2(0.1f, 0.0f); GL.Vertex2(1f, 0.3f);
            GL.TexCoord2(0.0f, 0.0f); GL.Vertex2(-1f, 0.3f);
            GL.End();
            GL.PopMatrix();



            //dibujar los cuerpos
            for (int i = 0; i < o1.Length; i++)
            {
                if (o1[i].estaConVida)
                {
                    GL.Disable(EnableCap.Texture2D);
                    GL.PushMatrix();
                   
                    GL.Color4(o1[i].r, o1[i].g, o1[i].b, o1[i].transparencia);//alpha 0.0  --- 1.0
                    GL.Translate(o1[i].position.X-1.7, o1[i].position.Y-0.7, o1[i].position.Z-0.1);
                    GL.Begin(PrimitiveType.Quads);
                    GL.Vertex3(0.0, 0.0, 0.0);//1er vertex
                    GL.Vertex3(o1[i].ancho, 0.0, 0.0);//2do
                    GL.Vertex3(o1[i].ancho, o1[i].alto, 0.0);
                    GL.Vertex3(0.0, o1[i].alto, 0.0);
                    GL.End();
                    GL.Disable(EnableCap.Texture2D);
                    GL.PopMatrix();
                }
            }

            //
            //dibujar los cuerpos
            for (int i = 0; i < o1.Length; i++)
            {
                if (o1[i].estaConVida)
                {
                    GL.Disable(EnableCap.Texture2D);
                    GL.PushMatrix();

                    GL.Color4(o1[i].r, o1[i].g, o1[i].b, o1[i].transparencia);//alpha 0.0  --- 1.0
                    GL.Translate(o1[i].position.X - 1.0, o1[i].position.Y +1.7, o1[i].position.Z - 0.1);
                    GL.Begin(PrimitiveType.Quads);
                    GL.Vertex3(0.0, 0.0, 0.0);//1er vertex
                    GL.Vertex3(o1[i].ancho, 0.0, 0.0);//2do
                    GL.Vertex3(o1[i].ancho, o1[i].alto, 0.0);
                    GL.Vertex3(0.0, o1[i].alto, 0.0);
                    GL.End();
                    GL.Disable(EnableCap.Texture2D);
                    GL.PopMatrix();
                }
            }
            //tuto
            GL.PushMatrix();
            GL.BindTexture(TextureTarget.Texture2D, texture_control);
            GL.Enable(EnableCap.Texture2D);
            GL.Rotate(90, 0, 0, 1);
            GL.Rotate(90, 1, 0, 0);
            //adelante,arriba,dereiz
            GL.Translate(-1.11, 0.1, -1.2);
            GL.Scale(0.3, 0.3, 0.3);
            MiFigura.Piso();
            GL.Disable(EnableCap.Texture2D);
            GL.PopMatrix();

            //acertijo
            GL.PushMatrix();
            GL.BindTexture(TextureTarget.Texture2D, texture_acer);
            GL.Enable(EnableCap.Texture2D);
            GL.Rotate(90, 0, 0, 1);
            GL.Rotate(90, 1, 0, 0);
            //adelante,arriba,dereiz
            GL.Translate(1.28, 0.5, -0.1);
            GL.Scale(0.3, 0.3, 0.3);
            MiFigura.Piso();
            GL.Disable(EnableCap.Texture2D);
            GL.PopMatrix();

            //r1
            GL.PushMatrix();
            GL.BindTexture(TextureTarget.Texture2D, texture_r1);
            GL.Enable(EnableCap.Texture2D);
            GL.Rotate(90, 0, 0, 1);
            GL.Rotate(90, 1, 0, 0);
            GL.Translate(1.28, 0.2, -0.5);
            GL.Scale(0.15, 0.15, 0.15);
            MiFigura.Piso();
            GL.Disable(EnableCap.Texture2D);
            GL.PopMatrix();

            //r2
            GL.PushMatrix();
            GL.BindTexture(TextureTarget.Texture2D, texture_r2);
            GL.Enable(EnableCap.Texture2D);
            GL.Rotate(90, 0, 0, 1);
            GL.Rotate(90, 1, 0, 0);
            GL.Translate(1.28, 0.2, 0.0);
            GL.Scale(0.15, 0.15, 0.15);
            MiFigura.Piso();
            GL.Disable(EnableCap.Texture2D);
            GL.PopMatrix();

            if (lose==true)
            {
                GL.PushMatrix();
                GL.BindTexture(TextureTarget.Texture2D, texture_lose);
                GL.Enable(EnableCap.Texture2D);
                GL.Rotate(90, 0, 0, 1);
                GL.Rotate(90, 1, 0, 0);
                GL.Translate(1.8, 0.2, 0.1);
                GL.Scale(0.15, 0.15, 0.15);
                MiFigura.Piso();
                GL.Disable(EnableCap.Texture2D);
                GL.PopMatrix();

                GL.PushMatrix();
                GL.BindTexture(TextureTarget.Texture2D, texture_lose);
                GL.Enable(EnableCap.Texture2D);
                GL.Rotate(90, 0, 0, 1);
                GL.Rotate(90, 1, 0, 0);
                GL.Translate(1.8, 0.2, 0.65);
                GL.Scale(0.15, 0.15, 0.15);
                MiFigura.Piso();
                GL.Disable(EnableCap.Texture2D);
                GL.PopMatrix();
            }
            //r3
            GL.PushMatrix();
            GL.BindTexture(TextureTarget.Texture2D, texture_r3);
            GL.Enable(EnableCap.Texture2D);
            GL.Rotate(90, 0, 0, 1);
            GL.Rotate(90, 1, 0, 0);
            GL.Translate(1.28, 0.2, 0.58);
            GL.Scale(0.15, 0.15, 0.15);
            MiFigura.Piso();
            GL.Disable(EnableCap.Texture2D);
            GL.PopMatrix();


            //if (mapa[cpY, cpX + 1] == mapa[epY,epX]|| mapa[cpY, cpX + 1] == mapa[epY1, epX1]|| mapa[cpY, cpX + 1] == mapa[epY2, epX2])
            //enemy
            if (mapa[cpY, cpX + 1] == 6)
            {
                win = true;
            }
            
            //win
            if (win == true)
            {
                GL.PushMatrix();
                GL.BindTexture(TextureTarget.Texture2D, texture_win);
                GL.Enable(EnableCap.Texture2D);
                GL.Rotate(90, 0, 0, 1);
                GL.Rotate(90, 1, 0, 0);
                GL.Translate(1.8, 0.2, -0.5);
                GL.Scale(0.15, 0.15, 0.15);
                MiFigura.Piso();
                GL.Disable(EnableCap.Texture2D);
                GL.PopMatrix();

                //
                //dibujar los cuerpos
                for (int i = 0; i < o1.Length; i++)
                {
                    if (o1[i].estaConVida)
                    {
                        GL.Enable(EnableCap.Texture2D);
                        GL.PushMatrix();

                        GL.Color4(0, 1, 0, 1);//alpha 0.0  --- 1.0
                        GL.Translate(o1[i].position.X - 0.5, o1[i].position.Y + 1.7, o1[i].position.Z - 0.1);
                        GL.Begin(PrimitiveType.Quads);
                        GL.Vertex3(0.0, 0.0, 0.0);//1er vertex
                        GL.Vertex3(o1[i].ancho, 0.0, 0.0);//2do
                        GL.Vertex3(o1[i].ancho, o1[i].alto, 0.0);
                        GL.Vertex3(0.0, o1[i].alto, 0.0);
                        GL.End();
                        GL.Disable(EnableCap.Texture2D);
                        GL.PopMatrix();
                    }
                }
            }
            if (mapa[cpY, cpX + 1] == 3)
            {
                this.Exit();
            }
            //mala lose
            if (mapa[cpY, cpX + 1] == 11)
            {
                lose = true;
                Console.WriteLine("f");
               // mapa[cpY, cpX + 1] = 0;
               // this.Exit();
            }
            if (mapa[cpY, cpX + 1] == 11)
            {
                lose = true;
                Console.WriteLine("1f");
                //mapa[cpY, cpX + 1] = 0;
                //this.Exit();
            }

            if (mapa[cpY, cpX + 1] == mapa[m1y, m1x])
            {
                mapa[cpY, cpX + 1] = 0;
            }
            //llaves
            if (mapa[cpY, cpX+1] == 8)
            {
                suma = true;
                mapa[cpY, cpX+1] = 0;
            }
            else
            {
                suma = false;
            }
            if (suma==true)
            {
                pts += 1;
                Console.WriteLine("collison" + pts);
            }
          
            //abre puertas
            if (pts == 1)
            {

                mapa[p1y, p1x] = 0;
                mapa[p2y, p2x] = 0;
  

            }

            GL.PushMatrix();
            Laberinto();
            GL.PopMatrix();

            GL.PushMatrix();
            _objectMesh.Draw();
            GL.PopMatrix();
            this.SwapBuffers();
        }
        public class TextRenderer : IDisposable
        {
            Bitmap bmp;
            Graphics gfx;
            int texture;
            Rectangle dirty_region;
            bool disposed;

            #region Constructors

            /// <summary>
            /// Constructs a new instance.
            /// </summary>
            /// <param name="width">The width of the backing store in pixels.</param>
            /// <param name="height">The height of the backing store in pixels.</param>
            public TextRenderer(int width, int height)
            {
                if (width <= 0)
                    throw new ArgumentOutOfRangeException("width");
                if (height <= 0)
                    throw new ArgumentOutOfRangeException("height ");
                if (GraphicsContext.CurrentContext == null)
                    throw new InvalidOperationException("No GraphicsContext is current on the calling thread.");

                bmp = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                gfx = Graphics.FromImage(bmp);
                gfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

                texture = GL.GenTexture();
                GL.BindTexture(TextureTarget.Texture2D, texture);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
                GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, width, height, 0,
                    PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);
            }

            #endregion

            #region Public Members

            /// <summary>
            /// Clears the backing store to the specified color.
            /// </summary>
            /// <param name="color">A <see cref="System.Drawing.Color"/>.</param>
            public void Clear(Color color)
            {
                gfx.Clear(color);
                dirty_region = new Rectangle(0, 0, bmp.Width, bmp.Height);
            }

            /// <summary>
            /// Draws the specified string to the backing store.
            /// </summary>
            /// <param name="text">The <see cref="System.String"/> to draw.</param>
            /// <param name="font">The <see cref="System.Drawing.Font"/> that will be used.</param>
            /// <param name="brush">The <see cref="System.Drawing.Brush"/> that will be used.</param>
            /// <param name="point">The location of the text on the backing store, in 2d pixel coordinates.
            /// The origin (0, 0) lies at the top-left corner of the backing store.</param>
            public void DrawString(string text, Font font, Brush brush, PointF point)
            {
                gfx.DrawString(text, font, brush, point);

                SizeF size = gfx.MeasureString(text, font);
                dirty_region = Rectangle.Round(RectangleF.Union(dirty_region, new RectangleF(point, size)));
                dirty_region = Rectangle.Intersect(dirty_region, new Rectangle(0, 0, bmp.Width, bmp.Height));
            }

            /// <summary>
            /// Gets a <see cref="System.Int32"/> that represents an OpenGL 2d texture handle.
            /// The texture contains a copy of the backing store. Bind this texture to TextureTarget.Texture2d
            /// in order to render the drawn text on screen.
            /// </summary>
            public int Texture
            {
                get
                {
                    UploadBitmap();
                    return texture;
                }
            }

            #endregion

            #region Private Members

            // Uploads the dirty regions of the backing store to the OpenGL texture.
            void UploadBitmap()
            {
                if (dirty_region != RectangleF.Empty)
                {
                    System.Drawing.Imaging.BitmapData data = bmp.LockBits(dirty_region,
                        System.Drawing.Imaging.ImageLockMode.ReadOnly,
                        System.Drawing.Imaging.PixelFormat.Format32bppArgb);

                    GL.BindTexture(TextureTarget.Texture2D, texture);
                    GL.TexSubImage2D(TextureTarget.Texture2D, 0,
                        dirty_region.X, dirty_region.Y, dirty_region.Width, dirty_region.Height,
                        PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

                    bmp.UnlockBits(data);

                    dirty_region = Rectangle.Empty;
                }
            }

            #endregion

            #region IDisposable Members

            void Dispose(bool manual)
            {
                if (!disposed)
                {
                    if (manual)
                    {
                        bmp.Dispose();
                        gfx.Dispose();
                        if (GraphicsContext.CurrentContext != null)
                            GL.DeleteTexture(texture);
                    }

                    disposed = true;
                }
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            ~TextRenderer()
            {
                Console.WriteLine("[Warning] Resource leaked: {0}.", typeof(TextRenderer));
            }

            #endregion
        }
        public void Laberinto()
        {
            
            for (int i = 0; i < mapa.GetLength(0); i++)
            {
                for (int j = 0; j < mapa.GetLength(1); j++)
                {
                    GL.PushMatrix();
                 
                    GL.Translate((j * 0.2f )-2, (i * 0.2f*-1)+2, 0.0);
                    //paredes
                    if (mapa[i, j] == 1)
                    {
                        GL.PushMatrix();
                        GL.Scale(0.2, 0.2, 0.8);
                        GL.Enable(EnableCap.Texture2D);
                        GL.BindTexture(TextureTarget.Texture2D, textura_Pared);
                        Cube(textura_Pared, textura_Pared, textura_Pared);
                        GL.Disable(EnableCap.Texture2D);
                        //Draw_Cube(0.1, 0.2, 0.1, 0.5f, 0.5f, 0.5f, 1.0f);
                        GL.PopMatrix();
                    }
                    if (mapa[i, j] == 0)
                    {
                        GL.PushMatrix();
                        GL.BindTexture(TextureTarget.Texture2D, textura_piso);
                        GL.Enable(EnableCap.Texture2D);
                        GL.Rotate(90, 0, 1, 0);
                        GL.Scale(0.2, 0.2, 0.1);
                        GL.Translate(1, 0, 0);

                        MiFigura.Piso();
                        GL.Disable(EnableCap.Texture2D);
                        GL.PopMatrix();
                    }
                    //puertas
                    if (mapa[i, j] == 7)
                    {
                        GL.PushMatrix();
                        GL.Scale(0.2, 0.2, 0.8);
                        GL.Enable(EnableCap.Texture2D);
                        GL.BindTexture(TextureTarget.Texture2D, textura_puerta);
                        Cube(textura_puerta, textura_puerta, textura_puerta);
                        GL.Disable(EnableCap.Texture2D);
                        //Draw_Cube(0.1, 0.2, 0.1, 0.5f, 0.5f, 0.5f, 1.0f);
                        GL.PopMatrix();
                    }
                    //player
                    if (mapa[i, j] == 5)
                    {
                        GL.PushMatrix();                     
                        GL.Scale(0.1, 0.1, 0.1);
                        GL.Translate( mx, my, 0.0);        
                        GL.Color4(1, 1, 0, 1);                                       
                        MiFigura.MiEsfera();
                        GL.PopMatrix();                        
                    }
                    //caja
                    if (mapa[i,j]==9)
                    {
                        GL.PushMatrix();
                        GL.Enable(EnableCap.Texture2D);
                        GL.BindTexture(TextureTarget.Texture2D, textura_caja);
                        GL.Scale(0.2,0.2, 0.2);
                        GL.Translate(0, 0, -0.5);
                        Cube(textura_caja, textura_caja, textura_caja);
                        GL.Disable(EnableCap.Texture2D);
                        GL.PopMatrix();
                    }
                    //llave
                    if (mapa[i, j] == 8)
                    {
                        GL.Scale(0.2, 0.2, 0.2);
                        GL.PushMatrix();
                        GL.Enable(EnableCap.Texture2D);
                        GL.BindTexture(TextureTarget.Texture2D, textura_llave);
                       
                        GL.Translate(0, 0,-0.2);
                        GL.Rotate(_rotar, 0, 1, 0);
                        Cube(textura_llave, textura_llave, textura_llave);
                        GL.Disable(EnableCap.Texture2D);
                        GL.PopMatrix();
                    }
                    //Enemigos
                    if (mapa[i, j] == 3)
                    {
                        GL.PushMatrix();
                        GL.Enable(EnableCap.Texture2D);
                        GL.BindTexture(TextureTarget.Texture2D, textura_caja);
                       
                        GL.Scale(0.15, 0.3, 0.15);
                        GL.Rotate(90, 1, 0, 0);
                        GL.Translate(velocidadEnemyX, velocidadEnemyY,0 );
                        //GL.Translate(cpX / 5, cpY / 5, -2);
                        {
                           // MiFigura.Draw_Pyramid();
                            _objectMesh.Draw();
                        }
                        GL.Disable(EnableCap.Texture2D);
                        GL.PopMatrix();
                    } 
                    if (mapa[i, j] == 4)
                    {
                        GL.PushMatrix();
                        GL.Enable(EnableCap.Texture2D);
                        GL.BindTexture(TextureTarget.Texture2D, textura_caja);

                        GL.Scale(0.15, 0.3, 0.15);
                        GL.Rotate(90, 1, 0, 0);
                        GL.Translate(velocidadEnemyX, velocidadEnemyY, 0);
                        // MiFigura.Draw_Pyramid();
                        _objectMesh.Draw();
                        GL.Disable(EnableCap.Texture2D);
                        GL.PopMatrix();
                    }
                    if (mapa[i, j] == 2)
                    {
                        GL.PushMatrix();
                        GL.Enable(EnableCap.Texture2D);
                        GL.BindTexture(TextureTarget.Texture2D, textura_caja);

                        GL.Scale(0.15, 0.3, 0.15);
                        GL.Rotate(90, 1, 0, 0);
                        GL.Translate(velocidadEnemyX, velocidadEnemyY, 0);
                        // MiFigura.Draw_Pyramid();
                        _objectMesh.Draw();
                        GL.Disable(EnableCap.Texture2D);
                        GL.PopMatrix();
                    }

                    if (mapa[i, j] == 6)
                    {
                        GL.PushMatrix();
                        GL.BindTexture(TextureTarget.Texture2D, textura_enemy);
                         GL.Enable(EnableCap.Texture2D);
                         GL.Rotate(_rotar, 0, 1, 0);
                        GL.Scale(0.2, 0.2, 0.2);
                        MiFigura.MiEsfera();
                         GL.Disable(EnableCap.Texture2D);
                        GL.PopMatrix();
                    }

                    if (mapa[i, j] == 11)
                    {
                        GL.PushMatrix();
                        GL.BindTexture(TextureTarget.Texture2D, textura_enemy);
                        GL.Enable(EnableCap.Texture2D);
                        GL.Rotate(_rotar, 0, 1, 0);
                        GL.Scale(0.2, 0.2, 0.2);
                        MiFigura.MiEsfera();
                        GL.Disable(EnableCap.Texture2D);
                        GL.PopMatrix();
                    }

                    GL.PopMatrix();
                }
            }
        }
    }
    
}
