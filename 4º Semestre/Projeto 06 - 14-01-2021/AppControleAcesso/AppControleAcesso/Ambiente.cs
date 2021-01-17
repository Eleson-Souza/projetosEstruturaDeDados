using System;
using System.Collections.Generic;
using System.Text;

namespace AppControleAcesso
{
    class Ambiente
    {
        private int id;
        private string nome;
        private Queue<Log> logs;

        public Ambiente(int id, string nome, Queue<Log> logs)
        {
            this.id = id;
            this.nome = nome;
            this.logs = logs;
        }

        public int Id { get => id; set => id = value; }
        public string Nome { get => nome; set => nome = value; }
        internal Queue<Log> Logs { get => logs; set => logs = value; }

        public void RegistrarLog(Log log)
        {
            if(this.logs.Count > 100)
            {
                this.Logs.Dequeue();
                this.logs.Enqueue(log);
            }
            else
            {
                this.logs.Enqueue(log);
            }
        }

        public override bool Equals(object obj)
        {
            return this.id.Equals(((Ambiente)obj).Id);
        }
    }
}
