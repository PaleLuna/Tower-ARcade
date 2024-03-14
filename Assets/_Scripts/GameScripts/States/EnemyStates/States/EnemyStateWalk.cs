using PaleLuna.Architecture.GameComponent;

public class EnemyStateWalk : EnemyState, IFixedUpdatable
{
    public EnemyStateWalk(Enemy context)
        : base(context) { }

    public override void StateStart()
    {
    }
    public void FixedFrameRun()
    {
        throw new System.NotImplementedException();
    }

    public override void StateStop()
    {
    }
}
