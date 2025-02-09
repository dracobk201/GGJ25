using ScriptableObjectArchitecture;

[System.Serializable]
public class MenuOptionsReference : BaseReference<MenuOptions, MenuOptionsVariable>
{
    public MenuOptionsReference() : base() { }
    public MenuOptionsReference(MenuOptions value) : base(value) { }
}