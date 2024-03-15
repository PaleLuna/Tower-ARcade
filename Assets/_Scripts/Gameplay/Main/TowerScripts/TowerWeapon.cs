using System.Collections;
using UnityEngine;

public abstract class TowerWeapon
{
    protected readonly AmmunitionHolder m_context;
    protected TowerConf m_towerConf;

    private bool _readyToFire = true;

    protected bool m_readyToFire => _readyToFire;

    public TowerWeapon(AmmunitionHolder context, TowerConf towerConf)
    {
        m_context = context;
        m_towerConf = towerConf;
    }

    public abstract void Fire(Enemy target, Vector3 muzzelPos);

    protected Vector3 PredictTheTargetPosition(Enemy target, Vector3 muzzelPos)
    {
        Transform targetTransform = target.transform;

        Vector3 displacement = targetTransform.position - m_context.transform.TransformDirection(muzzelPos);
        float distance = displacement.magnitude;

        // ����� ������ ������� �� ����
        float timeToTarget = distance / m_towerConf.initialSpeed;

        // ������� ��������� ����
        Vector3 futurePosition = targetTransform.position + target.rb.velocity * timeToTarget;

        // ����������� � �������� ��������� ����
        Vector3 direction = futurePosition - m_context.transform.TransformDirection(muzzelPos);

        return direction.normalized;
    }

    protected IEnumerator CoolDown()
    {
        _readyToFire = false;

        yield return new WaitForSeconds(60F / m_towerConf.shotsPerMinute);

        _readyToFire = true;
    }
}
