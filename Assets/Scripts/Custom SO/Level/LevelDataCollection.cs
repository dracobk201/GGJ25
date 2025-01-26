using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [CreateAssetMenu(
        fileName = "LevelDataCollection.asset",
        menuName = SOArchitecture_Utility.COLLECTION_SUBMENU + "Other/LevelData",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_COLLECTIONS + 0)]
    public class LevelDataCollection : Collection<LevelDataReference>
    {
    }
}
