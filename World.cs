using System;
using System.Collections.Generic;

namespace ecsharp
{
    public class World
    {
        private HashSet<System> systems = new HashSet<System>();
        private HashSet<System> systemsToRemove = new HashSet<System>();
        private HashSet<System> systemsToAdd = new HashSet<System>();

        private Dictionary<Entity, ComponentMap> entities = new Dictionary<Entity, ComponentMap>();
        private Dictionary<Type, HashSet<Entity>> componentEntities = new Dictionary<Type, HashSet<Entity>>();

        public void Update(float dt)
        {
            foreach (var system in this.systems)
            {
                system.Update(this, dt);
            }
        }

        public Entity createEntity()
        {
            var entity = new Entity();
            this.entities.Add(entity, new ComponentMap());

            return entity;
        }

        public Entity FindEntity(Type[] componentClasses)
        {
            if (componentClasses.Length == 0)
            {
                return null;
            }

            bool hasAllComponents = true;

            foreach (var ctor in componentClasses)
            {
                if (this.componentEntities.ContainsKey(ctor))
                {
                    hasAllComponents = false;
                    break;
                }
            }

            if (hasAllComponents)
            {
                return null;
            }

            var componentSets = new List<HashSet<Entity>>();

            foreach (var ctor in componentClasses)
            {
                componentSets.Add(this.componentEntities[ctor]);
            }

            HashSet<Entity> smallComponentSet = componentSets[0];
            var otherComponentSets = new List<HashSet<Entity>>();

            foreach (var componentSet in componentSets)
            {
                if (componentSet.Count < smallComponentSet.Count)
                {
                    smallComponentSet = componentSet;
                }
                else
                {
                    otherComponentSets.Add(componentSet);
                }
            }

            foreach (var entity in smallComponentSet)
            {
                var hasAll = true;

                foreach (var componentSet in otherComponentSets)
                {
                    if (componentSet.Contains(entity) == false)
                    {
                        break;
                    }
                }

                if (hasAll)
                {
                    return entity;
                }
            }

            return null;
        }

        public ComponentMap GetEntityComponents(Entity entity)
        {
            if (this.entities.ContainsKey(entity))
            {
                return this.entities[entity];
            }

            return null;
        }

        public World AddEntityComponents(Entity entity, Component[] components)
        {
            if (this.entities.ContainsKey(entity))
            {
                var componentMap = this.entities[entity];

                foreach (var component in components)
                {
                    componentMap.Add(component);

                    if (this.componentEntities.ContainsKey(component.GetType()))
                    {
                        this.componentEntities[component.GetType()].Add(entity);
                    }
                    else
                    {
                        this.componentEntities.Add(component.GetType(), new HashSet<Entity>() { entity });
                    }
                }
            }

            return this;
        }

        public World RemoveEntityComponents(Entity entity, Component[] components)
        {
            if (this.entities.ContainsKey(entity))
            {
                var componentMap = this.entities[entity];

                foreach (var component in components)
                {
                    componentMap.Remove(component.GetType());

                    if (this.componentEntities.ContainsKey(component.GetType()))
                    {
                        this.componentEntities[component.GetType()].Remove(entity);
                    }
                }
            }

            return this;
        }

        public void AddSystem(System system)
        {
            this.systemsToAdd.Add(system);
        }

        public void RemoveSystem(System system)
        {
            this.systemsToRemove.Add(system);
        }

        public void UpdateSystems(float dt)
        {
            foreach (var systemToRemove in this.systemsToRemove)
            {
                this.systems.Remove(systemToRemove);
            }

            this.systemsToRemove.Clear();

            foreach (var systemToAdd in this.systemsToAdd)
            {
                this.systems.Add(systemToAdd);
            }

            this.systemsToAdd.Clear();

            foreach (var system in this.systems)
            {
                system.Update(this, dt);
            }
        }
    }
}
