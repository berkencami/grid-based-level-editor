using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    [CreateAssetMenu(fileName = "ItemPalette", menuName = "Level Editor/ItemPalette", order = 1)]
    public class ItemPalette : ScriptableObject
    {
        public List<Item> items;
    }
}