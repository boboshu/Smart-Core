namespace Smart.Types
{
    public static class Struct
    {
        public static Pair<K, V> Pair<K, V>(K key, V value)
        {
            return new Pair<K, V>(key, value);
        }

        public static Triple<A, B, C> Triple<A, B, C>(A a, B b, C c)
        {
            return new Triple<A, B, C>(a, b, c);
        }
    }

    public struct Pair<K,V>
    {
        public readonly K key;
        public readonly V value;

        public Pair(K key, V value)
        {
            this.key = key;
            this.value = value;
        }
    }

    public struct Triple<A, B, C>
    {
        public readonly A a;
        public readonly B b;
        public readonly C c;

        public Triple(A a, B b, C c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }
    }
}
