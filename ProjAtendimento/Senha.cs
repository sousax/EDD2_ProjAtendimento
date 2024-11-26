using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjAtendimento
{
    internal class Senha
    {
        private int id;
        private DateTime dataGerac;
        private DateTime horaGerac;
        private DateTime dataAtend;
        private DateTime horaAtend;

        public Senha(int id)
        {
            this.id = id;
            dataGerac = DateTime.Now;
            horaGerac = DateTime.Now;
        }

        public string dadosParciais()
        {
            return this.id + " - " + dataGerac.ToString() + " - " + horaGerac.ToString();
        }

        public string dadosCompletos()
        {
            return dadosParciais() + " - " + dataAtend.ToString() + " - " + horaAtend.ToString();
        }
    }
}
