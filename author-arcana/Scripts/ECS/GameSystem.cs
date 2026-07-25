using Friflo.Engine.ECS;
using Friflo.Engine.ECS.Systems;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUTHORARCANA.ECS
{
    public abstract class GameSystem<T1> : QuerySystem<T1>, IEntityDeleteListener where T1 : struct, Friflo.Engine.ECS.IComponent
    {
        private HashSet<Entity> matchedEntities = new HashSet<Entity>();

        protected override void OnUpdate()
        {
            Query.ForEachEntity((ref T1 component, Entity entity) =>
            {
                CheckMatch(ref component, entity);
                Update(ref component, entity);
            });
        }

        private void CheckMatch(ref T1 component, Entity entity)
        {
            if (matchedEntities.Add(entity))
            {
                EcsManager.Instance.SubscribeDeleteEvent(this, entity);
                entity.OnComponentChanged += HandleComponentChanged;
                OnMatch(ref component, entity); 
            }
        }

        private void HandleComponentChanged(ComponentChanged changed)
        {
            if (changed.Action == ComponentChangedAction.Update)
            {
                OnChanged(changed.ComponentType, changed.Entity);
            }
            else if (changed.Action == ComponentChangedAction.Remove)
            {
                matchedEntities.Remove(changed.Entity);
                changed.Entity.OnComponentChanged -= HandleComponentChanged;
                OnUnmatch(changed.ComponentType, changed.Entity);
            }
        }

        protected virtual void Update(ref T1 component, Entity entity) { }

        protected virtual void OnMatch(ref T1 component, Entity entity) { }
        protected virtual void OnUnmatch(ComponentType componentType, Entity entity) { }
        protected virtual void OnChanged(ComponentType componentType, Entity entity) { }
        protected virtual void OnDelete(Entity entity) { }

        void IEntityDeleteListener.OnDeleteEntity(Entity entity)
        {
            matchedEntities.Remove(entity);
            OnDelete(entity);
        }
    }

    public abstract class GameSystem<T1,T2> : QuerySystem<T1,T2>, IEntityDeleteListener
        where T1 : struct, Friflo.Engine.ECS.IComponent
        where T2 : struct, Friflo.Engine.ECS.IComponent
    {
        private HashSet<Entity> matchedEntities = new HashSet<Entity>();

        protected override void OnUpdate()
        {
            Query.ForEachEntity((ref T1 component1, ref T2 component2, Entity entity) =>
            {
                CheckMatch(ref component1, ref component2, entity);
                Update(ref component1, ref component2, entity);
            });
        }

        private void CheckMatch(ref T1 component1, ref T2 component2, Entity entity)
        {
            if (matchedEntities.Add(entity))
            {
                EcsManager.Instance.SubscribeDeleteEvent(this, entity);
                entity.OnComponentChanged += HandleComponentChanged;
                OnMatch(ref component1, ref component2, entity);
            }
        }

        private void HandleComponentChanged(ComponentChanged changed)
        {
            if (changed.Action == ComponentChangedAction.Update)
            {
                OnChanged(changed.ComponentType, changed.Entity);
            }
            else if (changed.Action == ComponentChangedAction.Remove)
            {
                matchedEntities.Remove(changed.Entity);
                changed.Entity.OnComponentChanged -= HandleComponentChanged;
                OnUnmatch(changed.ComponentType, changed.Entity);
            }
        }

        protected virtual void Update(ref T1 component1, ref T2 component2, Entity entity) { }

        protected virtual void OnMatch(ref T1 component1, ref T2 component2, Entity entity) { }
        protected virtual void OnUnmatch(ComponentType componentType, Entity entity) { }
        protected virtual void OnChanged(ComponentType componentType, Entity entity) { }

        protected virtual void OnDelete(Entity entity) { }

        void IEntityDeleteListener.OnDeleteEntity(Entity entity)
        {
            matchedEntities.Remove(entity);
            OnDelete(entity);
        }
    }

    public abstract class GameSystem<T1, T2, T3> : QuerySystem<T1, T2, T3>, IEntityDeleteListener
        where T1 : struct, Friflo.Engine.ECS.IComponent
        where T2 : struct, Friflo.Engine.ECS.IComponent
        where T3 : struct, Friflo.Engine.ECS.IComponent
    {
        private HashSet<Entity> matchedEntities = new HashSet<Entity>();

        protected override void OnUpdate()
        {
            Query.ForEachEntity((ref T1 component1, ref T2 component2, ref T3 component3, Entity entity) =>
            {
                CheckMatch(ref component1, ref component2, ref component3, entity);
                Update(ref component1, ref component2, ref component3, entity);
            });
        }

        private void CheckMatch(ref T1 component1, ref T2 component2, ref T3 component3, Entity entity)
        {
            if (matchedEntities.Add(entity))
            {
                EcsManager.Instance.SubscribeDeleteEvent(this, entity);
                entity.OnComponentChanged += HandleComponentChanged;
                OnMatch(ref component1, ref component2, ref component3, entity);
            }
        }

        private void HandleComponentChanged(ComponentChanged changed)
        {
            if (changed.Action == ComponentChangedAction.Update)
            {
                OnChanged(changed.ComponentType, changed.Entity);
            }
            else if (changed.Action == ComponentChangedAction.Remove)
            {
                matchedEntities.Remove(changed.Entity);
                changed.Entity.OnComponentChanged -= HandleComponentChanged;
                OnUnmatch(changed.ComponentType, changed.Entity);
            }
        }

        protected virtual void Update(ref T1 component1, ref T2 component2, ref T3 component3, Entity entity) { }

        protected virtual void OnMatch(ref T1 component1, ref T2 component2, ref T3 component3, Entity entity) { }
        protected virtual void OnUnmatch(ComponentType componentType, Entity entity) { }
        protected virtual void OnChanged(ComponentType componentType, Entity entity) { }

        protected virtual void OnDelete(Entity entity) { }

        void IEntityDeleteListener.OnDeleteEntity(Entity entity)
        {
            matchedEntities.Remove(entity);
            OnDelete(entity);
        }
    }

    public abstract class GameSystem<T1, T2, T3, T4> : QuerySystem<T1, T2, T3, T4>, IEntityDeleteListener
        where T1 : struct, Friflo.Engine.ECS.IComponent
        where T2 : struct, Friflo.Engine.ECS.IComponent
        where T3 : struct, Friflo.Engine.ECS.IComponent
        where T4 : struct, Friflo.Engine.ECS.IComponent
    {
        private HashSet<Entity> matchedEntities = new HashSet<Entity>();

        protected override void OnUpdate()
        {
            Query.ForEachEntity((ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, Entity entity) =>
            {
                CheckMatch(ref component1, ref component2, ref component3, ref component4, entity);
                Update(ref component1, ref component2, ref component3, ref component4, entity);
            });
        }

        private void CheckMatch(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, Entity entity)
        {
            if (matchedEntities.Add(entity))
            {
                EcsManager.Instance.SubscribeDeleteEvent(this, entity);
                entity.OnComponentChanged += HandleComponentChanged;
                OnMatch(ref component1, ref component2, ref component3, ref component4, entity);
            }
        }

        private void HandleComponentChanged(ComponentChanged changed)
        {
            if (changed.Action == ComponentChangedAction.Update)
            {
                OnChanged(changed.ComponentType, changed.Entity);
            }
            else if (changed.Action == ComponentChangedAction.Remove)
            {
                matchedEntities.Remove(changed.Entity);
                changed.Entity.OnComponentChanged -= HandleComponentChanged;
                OnUnmatch(changed.ComponentType, changed.Entity);
            }
        }

        protected virtual void Update(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, Entity entity) { }

        protected virtual void OnMatch(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, Entity entity) { }
        protected virtual void OnUnmatch(ComponentType componentType, Entity entity) { }
        protected virtual void OnChanged(ComponentType componentType, Entity entity) { }

        protected virtual void OnDelete(Entity entity) { }

        void IEntityDeleteListener.OnDeleteEntity(Entity entity)
        {
            matchedEntities.Remove(entity);
            OnDelete(entity);
        }
    }

    public abstract class GameSystem<T1, T2, T3, T4, T5> : QuerySystem<T1, T2, T3, T4, T5>, IEntityDeleteListener
        where T1 : struct, Friflo.Engine.ECS.IComponent
        where T2 : struct, Friflo.Engine.ECS.IComponent
        where T3 : struct, Friflo.Engine.ECS.IComponent
        where T4 : struct, Friflo.Engine.ECS.IComponent
        where T5 : struct, Friflo.Engine.ECS.IComponent
    {
        private HashSet<Entity> matchedEntities = new HashSet<Entity>();

        protected override void OnUpdate()
        {
            Query.ForEachEntity((ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, Entity entity) =>
            {
                CheckMatch(ref component1, ref component2, ref component3, ref component4, ref component5, entity);
                Update(ref component1, ref component2, ref component3, ref component4, ref component5, entity);
            });
        }

        private void CheckMatch(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, Entity entity)
        {
            if (matchedEntities.Add(entity))
            {
                EcsManager.Instance.SubscribeDeleteEvent(this, entity);
                entity.OnComponentChanged += HandleComponentChanged;
                OnMatch(ref component1, ref component2, ref component3, ref component4, ref component5, entity);
            }
        }

        private void HandleComponentChanged(ComponentChanged changed)
        {
            if (changed.Action == ComponentChangedAction.Update)
            {
                OnChanged(changed.ComponentType, changed.Entity);
            }
            else if (changed.Action == ComponentChangedAction.Remove)
            {
                matchedEntities.Remove(changed.Entity);
                changed.Entity.OnComponentChanged -= HandleComponentChanged;
                OnUnmatch(changed.ComponentType, changed.Entity);
            }
        }

        protected virtual void Update(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, Entity entity) { }

        protected virtual void OnMatch(ref T1 component1, ref T2 component2, ref T3 component3, ref T4 component4, ref T5 component5, Entity entity) { }
        protected virtual void OnUnmatch(ComponentType componentType, Entity entity) { }
        protected virtual void OnChanged(ComponentType componentType, Entity entity) { }

        protected virtual void OnDelete(Entity entity) { }

        void IEntityDeleteListener.OnDeleteEntity(Entity entity)
        {
            matchedEntities.Remove(entity);
            OnDelete(entity);
        }
    }
}
