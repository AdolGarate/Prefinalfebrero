using RectanguloPOO.Entidades;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RectanguloPOO.Datos
{
    public class RepositorioDeRectangulos
    {
        private readonly string _archivo = Environment.CurrentDirectory + "\\Rectangulos.txt";
        private readonly string _archivoBak = Environment.CurrentDirectory + @"\\Rectangulos.bak";


        //Atributos
        private List<Rectangulo> listaRectangulo= new List<Rectangulo>();

        //Constructor

        public RepositorioDeRectangulos()
        {
            listaRectangulo = new List<Rectangulo>();
            listaRectangulo = LeerDatosDelArchivo();
        }

        private List<Rectangulo> LeerDatosDelArchivo()
        {
            var lista = new List<Rectangulo>();
            if (File.Exists(_archivo))
            {
                StreamReader lector = new StreamReader(_archivo);
                while (!lector.EndOfStream)
                {
                    var linea = lector.ReadLine();
                    Rectangulo rectangulo = ConstruirRectangulo(linea);
                    lista.Add(rectangulo);
                }
                lector.Close();
            }
            return lista;
        }

        private Rectangulo ConstruirRectangulo(string linea)
        {
            var campos = linea.Split(';');
            return new Rectangulo()
            {
                LadoMayor = int.Parse(campos[0]),
                LadoMenor = int.Parse(campos[1])

            };
        }

        //Metodos
        public void Agregar(Rectangulo rectangulo)
        {
            listaRectangulo.Add(rectangulo);
            AgregarEnArchivo(rectangulo);
        }

        private void AgregarEnArchivo(Rectangulo rectangulo)
        {
            StreamWriter escritor = new StreamWriter(_archivo, true);
            var linea = ConstruirLinea(rectangulo);
            escritor.WriteLine(linea);
            escritor.Close();
        }

        private string ConstruirLinea(Rectangulo rectangulo)
        {
            return $"{rectangulo.LadoMayor};{rectangulo.LadoMenor}";
        }

        public void Borrar(Rectangulo rectangulo)
        {
            listaRectangulo.Remove(rectangulo);
        }

        public int GetCantidad()
        {
            return listaRectangulo.Count();
        }

        public List<Rectangulo> GetList()
        {
            return listaRectangulo;
        }

        public Rectangulo GetPorPosicion(int index)
        {
            return listaRectangulo[index];
        }

        public bool Existe(Rectangulo rectangulo)
        {
            return listaRectangulo.Contains(rectangulo);
        }

        public void Editar(Rectangulo rectanguloSeleccionado, Rectangulo rectanguloEditado)
        {
            var index = listaRectangulo.FindIndex(r =>
                r.LadoMayor == rectanguloSeleccionado.LadoMayor &&
                r.LadoMenor == rectanguloSeleccionado.LadoMenor);
            listaRectangulo.RemoveAt(index);
            listaRectangulo.Insert(index, rectanguloEditado);
            EditarRegistroEnArchivo(rectanguloSeleccionado, rectanguloEditado);
        }

        private void EditarRegistroEnArchivo(Rectangulo rectanguloSeleccionado, Rectangulo rectanguloEditado)
        {
            StreamReader lector = new StreamReader(_archivo);
            StreamWriter escritor = new StreamWriter(_archivoBak);
            while (!lector.EndOfStream)
            {
                var linea = lector.ReadLine();
                Rectangulo rectanguloEnArchivo = ConstruirRectangulo(linea);
                if (!rectanguloEnArchivo.Equals(rectanguloSeleccionado))
                {
                    escritor.WriteLine(linea);
                }
                else
                {
                    linea = ConstruirLinea(rectanguloEditado);
                    escritor.WriteLine(linea);
                }
            }
            escritor.Close();
            lector.Close();
            File.Delete(_archivo);
            File.Move(_archivoBak, _archivo);
        }
    }
}
