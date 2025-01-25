using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [System.Serializable]
    [CreateAssetMenu(
        fileName = "LevelDataGameEvent.asset",
        menuName = SOArchitecture_Utility.GAME_EVENT + "LevelData",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_EVENTS + 2)]
    public sealed class LevelDataGameEvent : GameEventBase<LevelData>
    {
    }
}