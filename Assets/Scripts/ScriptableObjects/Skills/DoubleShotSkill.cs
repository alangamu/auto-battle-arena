using AutoFantasy.Scripts.Interfaces;
using UnityEngine;

namespace AutoFantasy.Scripts.ScriptableObjects.Skills
{
    [CreateAssetMenu(menuName = "Skills/Double Shot")]
    public class DoubleShotSkill : SkillSO
    {
        [SerializeField]
        private GameObject _projectilePrefab;

        public override void PerformSkill(ICombatController attacker, ICombatController target)
        {
            base.PerformSkill(attacker, target);
            if (attacker.GetGameObject().TryGetComponent(out IWeaponTransformProvider weaponTransform))
            {
                Transform spawnProjectileTransform = weaponTransform.GetWeaponTransform();
                GameObject projectileGameObject = Instantiate(_projectilePrefab, spawnProjectileTransform.position, spawnProjectileTransform.rotation);

                if (projectileGameObject.TryGetComponent(out IProjectile projectile))
                {
                    projectile.Launch(target.GetImpactTransform(), _attackDelay.Value / 2);
                }
            }
        }
    }
}