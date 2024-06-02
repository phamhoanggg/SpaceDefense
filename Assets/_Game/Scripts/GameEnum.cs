
public enum ResourcesType
{
    Lead,
    Copper,
    Silver,
    Gold,
    Coal,
}

public enum PropsType
{
    Lead,
    Copper,
    Silver,
    Gold,
    Coal,
}

public enum GameLayer
{
    Default = 0,
    Construction = 6,
    Enemy = 9,
    Player_Bullet = 14,
    Enemy_Bullet = 15,
}

public enum ConveyorType
{
    None,
    Strait,
    Corner,
    Branch,
}

public enum EnemyType
{
    Pulsar,
    Elude,
    Quasar,
    Risso,
}

public enum SceneId
{
    None = -1,
    Load = 0,
    Menu,
    SelectLevel,
    GamePlay,
}
