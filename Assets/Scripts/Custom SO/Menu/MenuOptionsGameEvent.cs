using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [System.Serializable]
    [CreateAssetMenu(
        fileName = "MenuOptionsGameEvent.asset",
        menuName = SOArchitecture_Utility.GAME_EVENT + "BubbleMadness/MenuOptions",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_EVENTS + 2)]
    public sealed class MenuOptionsGameEvent : GameEventBase<MenuOptions>
    {
    }
}