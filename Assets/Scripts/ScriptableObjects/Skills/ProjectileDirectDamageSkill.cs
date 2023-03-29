using AutoFantasy.Scripts.Interfaces;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Skills
{
    [CreateAssetMenu(menuName = "Skills/Projectile Direct Damage Skill")]
    public class ProjectileDirectDamageSkill : DirectDamageSkill
    {
        [SerializeField]
        private GameObject _projectilePrefab;
        [SerializeField]
        private float _projectileTimeImpact;

        public override void PerformSkill(ICombatController attacker, ICombatController target)
        {
            base.PerformSkill(attacker, target);

            LeanTween.delayedCall(_attackDelay.Value / 2, () =>
            {
                if (attacker.GetGameObject().TryGetComponent(out IWeaponController weaponController))
                {
                    Transform spawnProjectileTransform = weaponController.GetWeaponTransform();
                    GameObject projectileGameObject = Instantiate(_projectilePrefab, spawnProjectileTransform.position, spawnProjectileTransform.rotation);

                    if (projectileGameObject.TryGetComponent(out IProjectile projectile))
                    {
                        projectile.Launch(target.GetImpactTransform(), _projectileTimeImpact);
                    }
                }
            });
        }
    }
}