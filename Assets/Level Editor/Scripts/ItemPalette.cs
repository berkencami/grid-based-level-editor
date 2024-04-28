using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    [CreateAssetMenu(fileName = "Palette", menuName = "Level Builder/Palette", order = 1)]
    public class ItemPalette : ScriptableObject
    {
        public List<Item> items;
    }
}