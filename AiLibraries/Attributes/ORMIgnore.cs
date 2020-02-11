using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiLibraries.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    public class ORMIgnore:Attribute
    {

        private bool ignore;

        public ORMIgnore(bool ignore)
        {
            this.ignore = ignore;
        }

        public virtual bool Ignore
        {
            get { return this.ignore; }
        }
    }
}
