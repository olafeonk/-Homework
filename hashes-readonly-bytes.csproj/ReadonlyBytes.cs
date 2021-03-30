using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace hashes
{
    public class ReadonlyBytes : IEnumerable<byte>
    {
        private readonly byte[] bytes;

        private int hashCode;

        private bool isGetHashCode;
        public int Length => bytes.Length;

        public ReadonlyBytes(params byte[] bytes) => this.bytes = bytes ?? throw new ArgumentNullException();

        public byte this[int index]
        {
            get
            {
                if (index < 0 || index >= bytes.Length)
                {
                    throw new IndexOutOfRangeException();
                }

                return bytes[index];
            }
        }

        public IEnumerator<byte> GetEnumerator() => ((IEnumerable<byte>) bytes).GetEnumerator();
		

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        public override string ToString() => $"[{string.Join(", ", this)}]";

        public override bool Equals(object obj)
        {
            if (!(obj is ReadonlyBytes) || obj.GetType() != GetType())
            {
                return false;
            }

            var readonlyBytes = (ReadonlyBytes) obj;
            if (readonlyBytes.Length != Length )
            {
                return false;
            }
            return !readonlyBytes.Where((t, i) => !Equals(t, bytes[i])).Any();
        }

        public override int GetHashCode()
        {
            if (isGetHashCode)
            {
                return hashCode;
            }
            unchecked
            {
                foreach (var elem in this)
                {
                    hashCode = elem + hashCode * 307;
                }
            }

            isGetHashCode = true;

            return hashCode;
        }
    }
}