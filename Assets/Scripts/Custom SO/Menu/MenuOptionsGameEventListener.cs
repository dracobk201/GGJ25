using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "MenuOptions Event Listener")]
    public sealed class MenuOptionsGameEventListener : BaseGameEventListener<MenuOptions, MenuOptionsGameEvent, MenuOptionsEvent>
    {
    }
}