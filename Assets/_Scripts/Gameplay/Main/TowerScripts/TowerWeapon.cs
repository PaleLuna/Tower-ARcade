using System.Collections;
using PaleLuna.Architecture.Controllers;
using PaleLuna.Architecture.GameComponent;
using Services;
using UnityEngine;

public abstract class TowerWeapon : IUpdatable, IPausable
{
    protected readonly AmmunitionHolder m_context;

    private float _timeToShot;

    protected TowerConf m_towerConf;

    protected bool m_readyToFire = true;

    private float _currentTime = 0;


    public TowerWeapon(AmmunitionHolder context, TowerConf towerConf)
    {
        m_context = context;
        m_towerConf = towerConf;

        _timeToShot = 60 / m_towerConf.shotsPerMinute;

        UpdateRegistration();
        PausableRegistration();
    }

    public abstract void Fire(Enemy target, Vector3 muzzelPos);

    protected Vector3 PredictTheTargetPosition(Enemy target, Vector3 muzzelPos)
    {
        Transform targetTransform = target.transform;

        Vector3 displacement = targetTransform.position - m_context.transform.TransformDirection(muzzelPos);
        float distance = displacement.magnitude;

        float timeToTarget = distance / m_towerConf.initialSpeed;

        Vector3 futurePosition = targetTransform.position + target.rb.velocity * timeToTarget;

        Vector3 direction = futurePosition - m_context.transform.TransformDirection(muzzelPos);

        return direction.normalized;
    }

    public void EveryFrameRun()
    {
        if(m_readyToFire) return;

        _currentTime += Time.deltaTime;

        if(_currentTime >= _timeToShot)
        {
            m_readyToFire = true;
            _currentTime = 0;
        }
    }

    public void OnPause()
    {
        UpdateUnregistation();
    }

    public void OnResume()
    {
        UpdateRegistration();
    }

    private void PausableRegistration() =>
        ServiceManager.Instance.GlobalServices.Get<GameController>().pausablesHolder.Registration(this);

    private void UpdateRegistration() =>
        ServiceManager.Instance.GlobalServices.Get<GameController>().updatablesHolder.Registration(this);
    private void UpdateUnregistation() =>
        ServiceManager.Instance.GlobalServices.Get<GameController>().updatablesHolder.UnRegistration(this);
}
