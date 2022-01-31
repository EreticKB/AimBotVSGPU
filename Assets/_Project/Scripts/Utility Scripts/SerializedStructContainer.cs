using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

//Взято среди ответов на https://answers.unity.com/ Answer by Cherno · Apr 28, 2015 at 11:46 PM. Many thanks, guy, if you read it.
namespace SerializedStructContainer
{
    [System.Serializable]//помечаем структуру SerializableQuaternion как сериализуемую.
    public struct SerializableQuaternion
    {
        public float x;
        public float y;
        public float z;
        public float w;
        public SerializableQuaternion(float xParam, float yParam, float zParam, float wParam)
        {
            x = xParam;
            y = yParam;
            z = zParam;
            w = wParam;
        }
        public SerializableQuaternion(string str) => this = Serializator.DeserializeObject(str);

        public override string ToString()//Заменяем стандартную функцию, чтобы при выводе не было никаких отличий от обычного кватерниона.
        {
            return String.Format("[{0}, {1}, {2}, {3}]", x, y, z, w);
        }
        //создаем возможность неявного приведения типов, изучить позже подробнее.
        public static implicit operator Quaternion(SerializableQuaternion value)
        {
            return new Quaternion(value.x, value.y, value.z, value.w);
        }
        public static implicit operator SerializableQuaternion(Quaternion value)
        {
            return new SerializableQuaternion(value.x, value.y, value.z, value.w);
        }
        public string Serialize()
        {
            return Serializator.SerializeObject(this);
        }
    }
    public static class Serializator
    {
        //образцы взяты на https://stackoverflow.com/
        public static string SerializeObject(object o)
        {
            if (!o.GetType().IsSerializable)
            {
                return null;
            }
            using (MemoryStream stream = new MemoryStream())//потом разобраться почему здесь using.
            {
                new BinaryFormatter().Serialize(stream, o);
                return Convert.ToBase64String(stream.ToArray());
            }
        }
        public static SerializableQuaternion DeserializeObject(string str)
        {
            byte[] bytes = Convert.FromBase64String(str);
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                return (SerializableQuaternion) new BinaryFormatter().Deserialize(stream);
            }
        }
    }
}

