using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "LevelData Event Listener")]
    public sealed class LevelDataGameEventListener : BaseGameEventListener<LevelData, LevelDataGameEvent, LevelDataEvent>
    {
    }
}