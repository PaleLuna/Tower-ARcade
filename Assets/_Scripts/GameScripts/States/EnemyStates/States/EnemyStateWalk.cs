using PaleLuna.Architecture.Controllers;
using PaleLuna.Architecture.GameComponent;
using Services;
using UnityEngine;

public class EnemyStateWalk : EnemyState, IFixedUpdatable
{
    private PathPoint _currentPathPoint;
    private Vector3 _currentDirection;

    public EnemyStateWalk(Enemy context, EnemyStateHolder hodler)
        : base(context, hodler) { }

    public override void StateStart()
    {
        ServiceManager
            .Instance.GlobalServices.Get<GameController>()
            .updatablesHolder.Registration(this);

        m_context._pathPointReachedEvent.AddListener(ChangeDirection);
    }

    public void FixedFrameRun()
    {
        m_context.rb.velocity = _currentDirection * m_context.enemyConf.speed;
    }

    private void ChangeDirection(PathPoint newPathPoint)
    {
        _currentPathPoint = newPathPoint.nextPoint;

        if (_currentPathPoint == null)
        {
            m_hodler.ChangeState<EnemyStateIdle>();
            return;
        }

        Vector3 targetPos = _currentPathPoint.transform.position;
        targetPos.y = m_context.transform.position.y;

        _currentDirection = (targetPos - m_context.transform.position).normalized;
    }

    public override void StateStop()
    {
        ServiceManager
            .Instance.GlobalServices.Get<GameController>()
            .updatablesHolder.UnRegistration(this);
        m_context._pathPointReachedEvent.RemoveListener(ChangeDirection);
    }
}
