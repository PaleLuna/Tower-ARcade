using PaleLuna.Architecture.Controllers;
using PaleLuna.Architecture.GameComponent;
using Services;
using UnityEngine;

public class EnemyStateWalk : EnemyState, IFixedUpdatable, IPausable
{
    private PathPoint _currentPathPoint;
    private Vector3 _currentDirection;

    public EnemyStateWalk(Enemy context, EnemyStateHolder hodler)
        : base(context, hodler) { }

    public override void StateStart()
    {
        UpdateSubscribe();
        PausebleSubscribe();   

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
        UpdateUnsubscribe();
        PausebleUnsubscribe();
        m_context._pathPointReachedEvent.RemoveListener(ChangeDirection);
    }

    public void OnPause()
    {
        UpdateUnsubscribe();

        m_context.rb.velocity = Vector3.zero;
    }

    public void OnResume()
    {
        UpdateSubscribe();
    }

    #region  [ Subscribe methods ]

    private void PausebleSubscribe()
    {
        ServiceManager
            .Instance.GlobalServices.Get<GameController>()
            .pausablesHolder.Registration(this);
    }
    private void PausebleUnsubscribe()
    {
        ServiceManager
            .Instance.GlobalServices.Get<GameController>()
            .pausablesHolder.Unregistration(this);
    }


    private void UpdateSubscribe() 
    {
        ServiceManager
            .Instance.GlobalServices.Get<GameController>()
            .updatablesHolder.Registration(this);
    }
    private void UpdateUnsubscribe()
    {
        ServiceManager
            .Instance.GlobalServices.Get<GameController>()
            .updatablesHolder.UnRegistration(this);
    }
    #endregion
}
