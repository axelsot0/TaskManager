using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_Manager.Entity
{

    public class TareaEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? prioridad { get; set; }
        public bool? Due { get; set; }
        public DateTime? Deadline { get; set; }

        // Propiedades y métodos existentes...

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            TareaEntity other = (TareaEntity)obj;
            return this.Name == other.Name && this.Description == other.Description && this.prioridad == other.prioridad && other.Deadline == this.Deadline;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Description, prioridad,Deadline);
        }
}   } 
