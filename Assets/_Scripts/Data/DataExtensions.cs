using UnityEngine;

namespace _Scripts.Data
{
    public static class DataExtensions
    {
        public static string ToJson(this object obj) => 
            JsonUtility.ToJson(obj); 
        public static T ToDeserialzed<T>(this string json) => 
            JsonUtility.FromJson<T>(json);
    }
}