using UnityEngine;

public class Hit : InternalEvent
{
    public int damage;
}

public class Target : InternalEvent
{
    public Actor target;
}

public class StatData : InternalData
{
    public int hp;
}

public class VisualData : InternalData
{
    public MeshRenderer renderer;
}