using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using System.IO;

namespace MyOPENTK
{
    class ObjectLoader
    {
        /// <summary>
        /// Create the object mesh
        /// </summary>
        /// <param name="fileName">Filename of the object model</param>
        /// <param name="path">Main path of the object models</param>
        /// <param name="mesh">Reference to the mesh</param>
        /// <returns>Load status</returns>
        public static bool GetMesh(String fileName, String path, out Mesh mesh)
        {
            mesh = new Mesh();

            var fileToString = GetFileString(fileName);
            if (String.IsNullOrEmpty(fileToString))
            {
                Console.WriteLine(@"The file name can not be empty or null");
                return false;
            }

            using (var reader = new StringReader(fileToString))
            {

                string line;
                var initRead = true;
                var materialIndex = -1;

                int lineCounter = 0;
                while ((line = reader.ReadLine()) != null)
                {
                    lineCounter++;

                    //#: representa comentarios dentro del archivo
                    if (line.Length > 0 && line.ElementAt(0) != '#')
                    {
                        //Console.WriteLine(lineCounter);
                        string[] split;
                        switch (line.ElementAt(0))
                        {
                            #region MATERIAL - GRUPO - USING

                            case 'm':

                                //MATERIAL NAME RECORDING
                                string matName = line.Substring(7, line.Length - 7);
                                GetMaterial(matName, path, ref mesh);
                                break;

                            case 'u':

                                //USE RECORDING
                                string[] currentMaterial = line.Split(' ');
                                materialIndex = GetMaterialIndex(mesh.Materials, currentMaterial[1]);
                                break;

                            #endregion

                            #region VERTEX

                            case 'v':

                                //Console.WriteLine("V " + lineCounter);

                                //UPDATE STRING NUMBERS
                                line = line.Replace('.', ',');
                                int space;
                                var value = Vector3d.Zero;

                                for (space = 1; space < line.Length; space++)
                                    if (!line.ElementAt(space).Equals(' ')) break;

                                split = line.Substring(space, line.Length - space).Split(' ');
                                switch (line.ElementAt(1))
                                {
                                    //VERTEX RECORDING
                                    case ' ':
                                        if (double.TryParse(split[0], out value.X) && double.TryParse(split[1], out value.Y) && double.TryParse(split[2], out value.Z))
                                        {
                                            mesh.Vertices.Add(new Vertex { Value = value });

                                            //MAX AND MIN
                                            if (initRead) { mesh.MaxVertex = mesh.MinVertex = value; initRead = false; }
                                            else
                                            {
                                                mesh.MaxVertex = new Vector3d(Math.Max(mesh.MaxVertex.X, value.X), Math.Max(mesh.MaxVertex.Y, value.Y), Math.Max(mesh.MaxVertex.Y, value.Y));
                                                mesh.MinVertex = new Vector3d(Math.Min(mesh.MinVertex.X, value.X), Math.Min(mesh.MinVertex.Y, value.Y), Math.Min(mesh.MinVertex.Y, value.Y));
                                            }
                                        }
                                        break;

                                    //TEXTURE CORORDINATES RECORING
                                    case 't':
                                        if (double.TryParse(split[1], out value.X) && double.TryParse(split[2], out value.Y))
                                        {
                                            mesh.TextureCoordinates.Add(new Vector2d(value.X, 1.0 - value.Y));
                                        }
                                        break;

                                    //NORMAL RECORING
                                    case 'n':
                                        if (double.TryParse(split[1], out value.X) && double.TryParse(split[2], out value.Y) && double.TryParse(split[3], out value.Z))
                                            mesh.Normals.Add(value);
                                        break;
                                }

                                break;

                            #endregion

                            #region FACE AND LINE

                            case 'f':

                                //Console.WriteLine("F" + lineCounter);

                                //ORDEN BY FACE - VERTEX / TEXTURE / NORMAL

                                //FACE RECORDING
                                var currentsIndexs = line.Substring(2, line.Length - 2).Split(' ');
                                var currentFace = new Polygon { MaterailIndex = materialIndex };

                                //EVALUETE EAXH VERTEX
                                var index = 0;
                                foreach (String vertex in currentsIndexs)
                                {
                                    if (vertex == "") continue;

                                    var indices = vertex.Split('/');
                                    int currentIndex;

                                    //Vertex
                                    if (int.TryParse(indices[0], out currentIndex))
                                    {
                                        currentFace.VerticesIndices[index] = currentIndex - 1;
                                        mesh.Vertices[currentIndex - 1].FaceList.Add(mesh.Faces.Count);
                                    }

                                    //Texture coordinates
                                    if (int.TryParse(indices[1], out currentIndex))
                                        currentFace.TxtCoordsIndices[index] = currentIndex - 1;

                                    //Normal
                                    if (int.TryParse(indices[2], out currentIndex))
                                        currentFace.NormalsIndices[index] = currentIndex - 1;

                                    index++;
                                }

                                if (index != 3)
                                    throw new Exception("Number of vertices is invalid");

                                mesh.Faces.Add(currentFace);

                                break;

                                #endregion
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Normalize the vertexs (Mesh)
        /// </summary>
        /// <param name="vertexs">Vertices list to normalize</param>
        /// <param name="maxVertexs">Max point in the mesh</param>
        /// <param name="minVertexs">Min point in the mesh</param>
        static public void Normalize(List<Vertex> vertexs, Vector3d maxVertexs, Vector3d minVertexs)
        {
            var offSet = new Vector3d((maxVertexs.X + minVertexs.X) * 0.5, (maxVertexs.Y + minVertexs.Y) * 0.5, (maxVertexs.Z + minVertexs.Z) * 0.5);
            var maximun = new Vector3d(maxVertexs.X - minVertexs.X, maxVertexs.Y - minVertexs.Y, maxVertexs.Z - minVertexs.Z);
            double aux = Math.Max(maximun.X, Math.Max(maximun.Y, maximun.Z));
            double delta = 1.0 / aux;

            foreach (Vertex vertex in vertexs)
                vertex.Value = (vertex.Value - offSet) * delta;
        }

        /// <summary>
        /// CAlcula las nirmals por caras
        /// </summary>
        /// <param name="mesh">Mesh del objeto</param>
        static public void ComputePolygonNormnal(ref Mesh mesh)
        {
            foreach (Polygon face in mesh.Faces)
            {
                //NORMAL
                var vector1 = mesh.Vertices[face.VerticesIndices[1]].Value - mesh.Vertices[face.VerticesIndices[0]].Value;
                var vector2 = mesh.Vertices[face.VerticesIndices[1]].Value - mesh.Vertices[face.VerticesIndices[2]].Value;

                face.Normal = Vector3d.Cross(vector1, vector2);
                face.Normal.Normalize();

                //CENTRO
                for (int vertex = 0; vertex < face.VerticesIndices.Count(); vertex++)
                    face.Center += mesh.Vertices[face.VerticesIndices[vertex]].Value;

                face.Center /= face.VerticesIndices.Count();
            }
        }

        /// <summary>
        /// Calcula las normales por vertice
        /// </summary>
        /// <param name="mesh">Mesh del objeto</param>
        static public void ComputeVertexNormal(ref Mesh mesh)
        {
            foreach (Vertex vertice in mesh.Vertices)
            {
                foreach (int faceIndex in vertice.FaceList)
                    vertice.Normal += mesh.Faces[faceIndex].Normal;

                vertice.Normal /= vertice.FaceList.Count;
            }
        }

        /// <summary>
        /// Cargar todos los materiales del objeto
        /// </summary>
        /// <param name="fileName">nombre del archivo de materiales</param>
        /// <param name="path">Path del objeto</param>
        /// <param name="mesh">Mesh</param>
        /// <returns></returns>
        static public void GetMaterial(string fileName, String path, ref Mesh mesh)
        {
            if (mesh == null) throw new ArgumentNullException("mesh");

            string fileToString = GetFileString(@"..\..\" + path + @"\" + fileName);
            int lines = 0;
            if (String.IsNullOrEmpty(fileToString)) throw new ArgumentException(fileToString);

            using (StringReader reader = new StringReader(fileToString))
            {

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines++;
                    if (line.Length > 0 && line.ElementAt(0) != '#')
                    {
                        int index = line.IndexOf("\u0009"); //TAB
                        if (index != -1) line = line.Remove(index, 1);

                        switch (line.ElementAt(0))
                        {
                            case 'n':
                                string[] matName = line.Split(' ');
                                mesh.Materials.Add(new Material(matName[1]));
                                break;

                            case 'K':

                                line = line.Replace('.', ',');
                                string[] numbers = line.Split(' ');

                                switch (line.ElementAt(1))
                                {
                                    case 'd':
                                        mesh.Materials.ElementAt(mesh.Materials.Count - 1).RGB_KD = new Vector4((float)Convert.ToDouble(numbers[1]), (float)Convert.ToDouble(numbers[2]), (float)Convert.ToDouble(numbers[3]), mesh.Materials.ElementAt(mesh.Materials.Count - 1).RGB_KD.W);
                                        break;
                                }
                                break;

                            case 'm':
                                List<string> parameters = line.Split(' ').ToList();

                                if (parameters.Count != 2)
                                {
                                    Console.WriteLine(@"The texture entry is invalid, it's have " + parameters.Count + @" parameters (expected 2). [MATERIAL]");
                                    break;
                                }

                                if (parameters[0].ToLower().Equals("map_ka") || parameters[0].ToLower().Equals("map_kd"))
                                {
                                    string materialFileName = @"..\..\" + path + @"\" + parameters[1];

                                    List<string> name = materialFileName.Split('\\').ToList();
                                    try { mesh.Textures.Add(materialFileName, mesh.Textures.Count); }
                                    catch (ArgumentException)
                                    {
                                        Console.WriteLine(@"Duplicate texture [" + name[name.Count - 1] + @"]");
                                    }

                                    mesh.Materials.ElementAt(mesh.Materials.Count - 1).HasTexture = true;
                                    mesh.Materials.ElementAt(mesh.Materials.Count - 1).TextureFileName = @"..\..\" + path + @"\" + parameters[1];
                                }

                                break;

                            case 'd':
                                line = line.Replace('.', ',');
                                string[] alpha = line.Split(' ');
                                mesh.Materials.ElementAt(mesh.Materials.Count - 1).RGB_KD.W = (float)Convert.ToDouble(alpha[1]);
                                break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Busca el indice de un material dado su nombre
        /// </summary>
        /// <param name="materials">Lista de material donde se desea buscar</param>
        /// <param name="materialName">Nombre del material a buscar</param>
        /// <returns>Index</returns>
        static public int GetMaterialIndex(List<Material> materials, string materialName)
        {
            int index = 0;
            foreach (Material material in materials)
            {
                if (material.Name.Equals(materialName))
                    return index;

                index++;
            }

            return -1;
        }

        /// <summary>
        /// Transfiere un archivo completo a un string
        /// </summary>
        /// <param name="fileName">Archivo a cargar</param>
        /// <returns>Archivo en el string</returns>
        static public string GetFileString(string fileName)
        {
            try
            {
                var file = new StreamReader(fileName);
                var fileSTR = file.ReadToEnd();
                file.Close();
                return fileSTR;
            }
            catch { Console.Write(@"ERROR FILE"); }
            return "";
        }
    }
}
