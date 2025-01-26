using ScriptableObjectArchitecture;

[System.Serializable]
public class GameStatusReference : BaseReference<GameStatus, GameStatusVariable>
{
    public GameStatusReference() : base() { }
    public GameStatusReference(GameStatus value) : base(value) { }
}