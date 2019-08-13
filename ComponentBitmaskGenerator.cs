using System.Collections;

namespace ecsharp
{
    public class ComponentBitmasGenerator
    {
        private int size = 1;

        public BitArray Next()
        {
            var bitmask = new BitArray(this.size);
            bitmask.Set(this.size - 1, true);

            ++this.size;

            return bitmask;
        }
    }
}
