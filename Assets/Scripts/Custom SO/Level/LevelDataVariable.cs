using UnityEngine;
using UnityEngine.Events;
using ScriptableObjectArchitecture;

[System.Serializable]
public class LevelDataEvent : UnityEvent<LevelData> { }
[CreateAssetMenu(
        fileName = "LevelDataVariable.asset",
        menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "BubbleMadness/LevelData")]
public sealed class LevelDataVariable : BaseVariable<LevelData, LevelDataEvent>
{
}