using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL;


namespace MyOPENTK
{
    /**MODIFICADO**/
    public class Material
    {
        #region Properties

        /// <summary>
        /// Nombre del material
        /// </summary>
        public String Name { set; get; }

        /// <summary>
        /// Color difuso. W = ALPHA
        /// </summary>
        public Vector4 RGB_KD = Vector4.One;

        /// <summary>
        /// Tiene textura el material
        /// </summary>
        public bool HasTexture { set; get; }

        /// <summary>
        /// Ruta del la textura, si aplica
        /// </summary>
        public String TextureFileName { set; get; }

        #endregion

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public Material()
        {
            TextureFileName = Name = "";
        }

        /// <summary>
        /// Contructor base
        /// </summary>
        /// <param name="name"></param>
        public Material(String name)
        {
            Name = name;
            TextureFileName = "";
        }
    }

    /**NUEVO**/
    public class Vertex
    {
        public Vector3d Value = Vector3d.Zero;
        public Vector3d Normal = Vector3d.Zero;
        public List<int> FaceList = new List<int>();

        public void Clone(ref Vertex newVertex)
        {
            //CLONE LIST
            foreach (int currentFaceIndex in FaceList)
                newVertex.FaceList.Add(currentFaceIndex);

            //NORMAL AND VERTEX
            newVertex.Value = new Vector3d(Value);
            newVertex.Normal = new Vector3d(Normal);
        }
    }

    /**MODIFICADO**/
    public class Polygon
    {
        /// <summary>
        /// Constructor basico
        /// </summary>
        public Polygon()
        {
            NumOfVertices = 3;
            VerticesIndices = new int[NumOfVertices];
            NormalsIndices = new int[NumOfVertices];
            TxtCoordsIndices = new int[NumOfVertices];
        }

        /// <summary>
        /// Contructor especifico
        /// </summary>
        /// <param name="numOfVertices">Número de vértices para el poligono</param>
        public Polygon(int numOfVertices)
        {
            NumOfVertices = numOfVertices;
            VerticesIndices = new int[NumOfVertices];
            NormalsIndices = new int[NumOfVertices];
            TxtCoordsIndices = new int[NumOfVertices];
        }

        /// <summary>
        /// Indica cuantos vertices tiene nuestro poligono
        /// También se podría saber con el Lenght de cualquyiera de los arreglos
        /// </summary>
        public int NumOfVertices { set; get; }

        /// <summary>
        /// Indice del material aplicado a la cara
        /// </summary>
        public int MaterailIndex { set; get; }

        /// <summary>
        /// Indices the los vertices
        /// </summary>
        public int[] VerticesIndices { set; get; }

        /// <summary>
        /// Indices the los vertices
        /// </summary>
        public int[] NormalsIndices { set; get; }

        /// <summary>
        /// Indices the los vertices
        /// </summary>
        public int[] TxtCoordsIndices { set; get; }

        /// <summary>
        /// Vector normal del poligono
        /// </summary>
        public Vector3d Normal { set; get; }

        /// <summary>
        /// Punto central del poligono
        /// </summary>
        public Vector3d Center { set; get; }
    }

    /**MODIFICADO**/
    class Mesh
    {
        #region Properties

        /// <summary>
        /// Lista de poligonos [Triangulos] del modelo
        /// </summary>
        public List<Polygon> Faces { set; get; }

        /**NUEVO**/
        /// <summary>
        /// Lista de vertices
        /// </summary>
        public List<Vertex> Vertices { set; get; }

        /// <summary>
        /// Lista de vertices
        /// </summary>
        public List<Vector3d> Normals { set; get; }

        /// <summary>
        /// Lista de cordenadas de texturas
        /// </summary>
        public List<Vector2d> TextureCoordinates { set; get; }

        /// <summary>
        /// Texturas
        /// </summary>
        public Dictionary<string, int> Textures { set; get; }

        /// <summary>
        /// Lista de materiales del modelo
        /// </summary>
        public List<Material> Materials { set; get; }

        /**NUEVO**/
        /// <summary>
        /// Vertice mayor y menor
        /// </summary>
        public Vector3d MaxVertex = Vector3d.Zero, MinVertex = Vector3d.Zero;

        #endregion

        #region Constructors

        /// <summary>
        /// The base Constructor.
        /// </summary>
        public Mesh()
        {
            Faces = new List<Polygon>();
            Vertices = new List<Vertex>();
            Normals = new List<Vector3d>();

            TextureCoordinates = new List<Vector2d>();
            Textures = new Dictionary<string, int>();
            Materials = new List<Material>();

        }

        #endregion

        #region Draw /**MODIFICADO**/

        /// <summary>
        /// Draw the mesh
        /// </summary>
        /// <param name="polygonMode">Tipo de despliegue para los poligonos</param>
        /// <param name="shadingMode">Tipo de sombreado</param>
        public void Draw(PolygonMode polygonMode = PolygonMode.Fill, ShadingModel shadingMode = ShadingModel.Smooth)
        {
            GL.PolygonMode(MaterialFace.FrontAndBack, polygonMode);

            foreach (Polygon face in Faces)
            {
                Material material = Materials[face.MaterailIndex];
                GL.BindTexture(TextureTarget.Texture2D, (material.HasTexture) ? ((uint)Textures[material.TextureFileName]) : 0);
                GL.Color4(material.RGB_KD);

                GL.Begin(BeginMode.Polygon);

                if (shadingMode == ShadingModel.Smooth) GL.Normal3(face.Normal);

                for (int vertexs = 0; vertexs < face.NumOfVertices; vertexs++)
                {
                    GL.TexCoord2(TextureCoordinates[face.TxtCoordsIndices[vertexs]].X, TextureCoordinates[face.TxtCoordsIndices[vertexs]].Y);

                    if (shadingMode == ShadingModel.Smooth)
                        GL.Normal3(Normals[face.NormalsIndices[vertexs]]);
                        //GL.Normal3(Vertices[face.VerticesIndices[vertexs]].Normal);

                    GL.Vertex3(Vertices[face.VerticesIndices[vertexs]].Value);
                }

                GL.End();
            }
        }

        #endregion
    }
}