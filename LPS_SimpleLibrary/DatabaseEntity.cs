using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPS_SimpleLibrary
{
    public abstract class DatabaseEntity
    {
        public int Id { get; set; }
        public abstract void Insert();
        public abstract void Update();
        public abstract void Delete();
    }
}
