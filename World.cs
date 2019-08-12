using System.Collections.Generic;

namespace ecsharp
{
    public class World
    {
        private System[] systems = { };
        private System[] systemsToRemove = { };
        private System[] systemsToAdd = { };

        private Dictionary<Entity, ComponentMap> entities = new Dictionary<Entity, ComponentMap>();
        private Dictionary<Component, HashSet<Entity>> componentEntities = new Dictionary<Component, HashSet<Entity>>();

        public Entity createEntity()
        {
            var entity = new Entity();
            this.entities.Add(entity, new ComponentMap());

            return entity;
        }
    }
}
