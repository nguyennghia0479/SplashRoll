using UnityEngine;

[CreateAssetMenu(fileName = "Level Database - ", menuName = "Scriptable Objects/LeveDatabaseSO")]
public class LevelDatabaseSO : ScriptableObject
{
    [SerializeField] private string levelDBName;
    [SerializeField] private LevelSO[] levelSOs;

    public string LevelDBName => levelDBName;
    public LevelSO[] LevelSOs => levelSOs;
}
