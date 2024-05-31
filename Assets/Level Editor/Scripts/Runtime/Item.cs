using System;
using UnityEngine;

namespace LevelEditor
{
    [Serializable]
    public class Item
    {
        public string id;
        public GameObject prefab;
        public Texture icon;
    }
}
