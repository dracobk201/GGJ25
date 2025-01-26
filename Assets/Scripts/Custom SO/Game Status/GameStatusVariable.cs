using UnityEngine;
using UnityEngine.Events;
using ScriptableObjectArchitecture;

[System.Serializable]
public class GameStatusEvent : UnityEvent<GameStatus> { }
[CreateAssetMenu(
        fileName = "GameStatusVariable.asset",
        menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "Other/GameStatus")]
public sealed class GameStatusVariable : BaseVariable<GameStatus, GameStatusEvent>
{
}