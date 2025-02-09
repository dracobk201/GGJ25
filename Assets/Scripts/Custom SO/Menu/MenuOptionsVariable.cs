using UnityEngine;
using UnityEngine.Events;
using ScriptableObjectArchitecture;

[System.Serializable]
public class MenuOptionsEvent : UnityEvent<MenuOptions> { }
[CreateAssetMenu(
        fileName = "MenuOptionsVariable.asset",
        menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "BubbleMadness/MenuOptions")]
public sealed class MenuOptionsVariable : BaseVariable<MenuOptions, MenuOptionsEvent>
{
}