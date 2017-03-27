using System;
using System.IO;
using System.Xml.Serialization;

namespace Smart.Utils
{
    public static class Serialization
    {
        static Serialization()
        {
            _xmlNS.Add("", "");
        }

        private static readonly XmlSerializerNamespaces _xmlNS = new XmlSerializerNamespaces();

        public static void XMLSerialize(string fileName, object instance)
        {
            var serializer = new XmlSerializer(instance.GetType());
            using (var writer = new StreamWriter(fileName))
                serializer.Serialize(writer, instance, _xmlNS);
        }

        public static T XMLDeserialize<T>(string fileName) 
            where T : class, new()
        {
            if (!File.Exists(fileName)) return new T();
            var serializer = new XmlSerializer(typeof(T));
            using (var reader = new StreamReader(fileName))
                return (T) serializer.Deserialize(reader);
        }

        public static object XMLDeserialize(string fileName, Type type) 
        {
            if (!File.Exists(fileName)) return Activator.CreateInstance(type);
            var serializer = new XmlSerializer(type);
            using (var reader = new StreamReader(fileName))
                return serializer.Deserialize(reader);
        }
    }
}