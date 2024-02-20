using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RectanguloPOO.Entidades
{
    public class Rectangulo : ICloneable
    {
        //atributos
        private int ladoMayor;
        private int ladoMenor;

        //Propiedades
        public int LadoMenor
        {
            get { return ladoMenor; }
            set { ladoMenor = value; }
        }


        public int LadoMayor
        {
            get { return ladoMayor; }
            set { ladoMayor = value; }
        }

        //Metodos
        public int GetPerimetro()
        {
            return 2 * LadoMayor + 2 * LadoMenor;
        }

        public int GetArea()
        {
            return LadoMayor * LadoMenor;
        }

        public bool Validar()
        {
            return LadoMayor > 0 && LadoMenor > 0 && LadoMayor > LadoMenor;
        }

        public override bool Equals(object obj)
        {
            if (obj==null || !(obj is Rectangulo))
            {
                return false;
            }

            return this.ladoMayor == ((Rectangulo)obj).ladoMayor &&
                   this.ladoMenor == ((Rectangulo)obj).ladoMenor;
        }

        public override int GetHashCode()
        {
            return (ladoMenor+ladoMayor).GetHashCode();
        }
        public object Clone()
        {
            return this.MemberwiseClone();
        }


    }
}
