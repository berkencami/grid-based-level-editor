using UnityEngine;
using LevelEditor;
using UnityEngine.Assertions;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField] public Level level;

    private void Start()
    {
        Assert.IsNotNull(level);
        LevelLoader.LoadLevel(level);
    }
}