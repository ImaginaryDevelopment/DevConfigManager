using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EnvDte
{
    public class DteCommand
    {
        public DteCommand(int id, string localizedName, IEnumerable<string> bindings)
        {
            LocalizedName = localizedName;
            Id = id;
            Bindings = bindings;
        }

        public string LocalizedName { get; private set; }
        public int Id { get; private set; }
        public IEnumerable<string > Bindings { get; private set; } 

    }
}
