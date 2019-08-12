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

        public T Get<T>(Type componentClass) where T : Component
        {
            return this.components[componentClass] as T;
        }
    }
}
