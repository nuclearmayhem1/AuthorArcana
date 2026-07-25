using Friflo.Engine.ECS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUTHORARCANA.ECS
{
    public interface IEntityDeleteListener
    {
        void OnDeleteEntity(Entity entity);
    }
}
