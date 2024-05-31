using System.Linq;
using UnityEngine;

namespace LevelEditor
{
    public static class LevelLoader
    {
        public static void LoadLevel(Level level)
        {
            LevelData[] data = JsonExtension.GetFromJson<LevelData>(level.data);
            foreach (LevelData dataItem in data)
            {
                LevelData ıtem = dataItem;
                Item dataPaletteItem = level.itemPalette.items.FirstOrDefault(fd => fd.id == ıtem.itemID);

                if (dataPaletteItem != null)
                {
                    Vector3 position = new Vector3(dataItem.row, dataItem.column, 0.0f);
                    Vector3 offset = Vector3.zero;
                    offset += new Vector3(-(float)(level.row - 1) / 2, (float)(level.column - 1) / 2, 0.0f);
                    InstantiateItem(dataPaletteItem.prefab, position, offset);
                }
            }
        }

        private static void InstantiateItem(GameObject prefab, Vector3 position, Vector3 offset)
        {
            GameObject instance = Object.Instantiate(prefab);
            float x = position.x + offset.x;
            float y = -position.y + offset.y;
            float z = offset.z;
            instance.transform.position = new Vector3(x, y, z);
            instance.transform.localScale = Vector3.one;
        }
    }
}