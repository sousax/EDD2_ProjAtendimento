using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjAtendimento
{
    internal class Guiche
    {
        private int id;
        private Queue<Senha> atendimentos;

        public Guiche()
        {
            this.atendimentos = new Queue<Senha>();
            this.id = 0;
        }

        public Guiche(int id)
        {
            this.atendimentos = new Queue<Senha>();
            this.id = id;
        }

        public int getId()
        {
            return this.id;
        }

        public Queue<Senha> getAtendimentos()
        {
            return this.atendimentos;
        }

        public bool chamar(Queue<Senha> filaSenhas)
        {
            if (filaSenhas.Count > 0)
            {
                Senha proximaSenha = filaSenhas.Dequeue();

                this.atendimentos.Enqueue(proximaSenha);

                return true;
            }
            else
                return false;
        }
    }
}
