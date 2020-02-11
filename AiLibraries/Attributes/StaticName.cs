using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiLibraries.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class StaticName:Attribute
    {
        private bool staticName;

        public StaticName(bool staticName)
        {
            this.staticName = staticName;
        }

        public virtual bool Static
        {
            get { return this.staticName; }
        }
    }
}
