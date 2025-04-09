using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inbentarioa
{
    public static class Errola
    {
        private static string _erabiltzaileRola;

        public static string ErabiltzaileRola
        {
            get { return _erabiltzaileRola; }
            set { _erabiltzaileRola = value; }
        }
    }
}