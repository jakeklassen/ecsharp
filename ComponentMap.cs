using System;
using System.Collections.Generic;

namespace ecsharp
{
    public class ComponentMap
    {
        private Dictionary<Type, Component> components = new Dictionary<Type, Component>();

        public ComponentMap Add(Component component)
        {
            this.components.Add(component.GetType(), component);

            return this;
        }

        public int Count
        {
            get
            {
                return this.components.Count;
            }
        }

        public T Get<T>(Type componentType) where T : Component
        {
            if (this.components.ContainsKey(componentType))
            {
                return this.components[componentType] as T;
            }

            return null;
        }

        public void Remove(Type componentType)
        {
            this.components.Remove(componentType);
        }

        public bool Has(Type componentType)
        {
            return this.components.ContainsKey(componentType);
        }
    }
}
