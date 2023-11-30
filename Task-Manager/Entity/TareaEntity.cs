using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Manager.Entity
{
    internal class TareaEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool prioridad { get; set; }
        public bool Due { get; set; }
        public DateTime Deadline { get; set; }
        public TimeSpan hora { get; set; }
    }
}
