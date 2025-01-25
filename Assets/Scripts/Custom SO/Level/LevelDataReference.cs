using ScriptableObjectArchitecture;

[System.Serializable]
public class LevelDataReference : BaseReference<LevelData, LevelDataVariable>
{
    public LevelDataReference() : base() { }
    public LevelDataReference(LevelData value) : base(value) { }
}