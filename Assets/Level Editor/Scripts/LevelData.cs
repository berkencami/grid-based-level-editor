using System;
using UnityEngine;

namespace LevelEditor
{
    [Serializable]
    public class LevelData
    {
        [SerializeField] public int row;
        [SerializeField] public int column;
        [SerializeField] public string itemID;
    }
}