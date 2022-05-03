using System.Collections;

public abstract class State
{
    protected ShootHookSystem ShootHookSystem;

    public State(ShootHookSystem shootHookSystem)
    {
        ShootHookSystem = shootHookSystem;
    }

    public virtual IEnumerator Ready()
    {
        yield break;
    }

    public virtual IEnumerator Start()
    {
        yield break;
    }
    
    public virtual IEnumerator Shooting()
    {
        yield break;
    }
    
    public virtual IEnumerator Grappled()
    {
        yield break;
    }
    
    public virtual IEnumerator Returning()
    {
        yield break;
    }
}
