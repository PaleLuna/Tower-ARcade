using UnityEngine;

public class CanonWeapon : TowerWeapon
{
    public CanonWeapon(AmmunitionHolder context, TowerConf towerConf)
        : base(context, towerConf) { }

    public override void Fire(Enemy target, Vector3 muzzelPos) 
    {
        if (!m_context || !m_readyToFire) return;

        Shell shell = m_context.GetShell();

        Vector3 direction = PredictTheTargetPosition(target, muzzelPos);
        shell.transform.position = muzzelPos;

        shell.ThrowThis(direction, m_towerConf.initialSpeed, m_towerConf.damageByHit);

        m_readyToFire = false;
    }
}
