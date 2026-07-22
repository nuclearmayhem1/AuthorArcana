using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUTHORARCANA.ECS
{
    [AttributeUsage(AttributeTargets.Class)]
    internal class Update : Attribute { }
    [AttributeUsage(AttributeTargets.Class)]
    internal class FixedUpdate : Attribute { }
}
