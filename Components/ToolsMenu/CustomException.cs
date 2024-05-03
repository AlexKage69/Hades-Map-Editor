using Hades_Map_Editor.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hades_Map_Editor
{
    public class NoFileLoadedException : Exception
    {
        public NoFileLoadedException()
        {

        }

        public NoFileLoadedException(string message)
            : base(message)
        {

        }

        public NoFileLoadedException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
