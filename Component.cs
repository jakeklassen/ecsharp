using System;
using System.Collections;

namespace ecsharp
{
    public abstract class Component : IEquatable<Component>
    {
        private BitArray bitmask = new BitArray(0);

        public BitArray Bitmask { get => bitmask; }

        public override bool Equals(object obj)
        {
            return Equals(obj as Component);
        }

        public bool Equals(Component other)
        {
            return this.bitmask.Equals(other.Bitmask);
        }

        public override int GetHashCode()
        {
            return this.bitmask.GetHashCode();
        }
    }
}
