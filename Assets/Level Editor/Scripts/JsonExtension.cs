using System;
using UnityEngine;

namespace LevelEditor
{
    public static class JsonExtension
    {
        [Serializable]
        private class ItemList<T>
        {
            public T[] Items;
        }

        public static T[] GetFromJson<T>(string json)
        {
            return JsonUtility.FromJson<ItemList<T>>(json).Items;
        }

        public static string SetToJson<T>(T[] array)
        {
            return JsonUtility.ToJson(new ItemList<T> { Items = array });
        }
    }
}