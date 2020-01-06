using System;
using System.Collections.Generic;
using System.Text;

namespace App6_Tarefa.Modelos
{
    public class Tarefa
    {
        public string Nome { get; set; }
        public DateTime? DataFinalizacao { get; set; }
        public byte Prioridade { get; set; } //byte can store values from 0 to 255 (i just need 4 priorities)
    }
}
