using AutoFantasy.Scripts.Interfaces;
using System.Collections.Generic;
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

        public override void PerformSkill(List<ICombatController> team, List<ICombatController> targets)
        {
            base.PerformSkill(team, targets);

            LeanTween.delayedCall(_attackDelay.Value / 2, () =>
            {
                ICombatController selectedHeroController = team.Find(x => x.IsActive());

                if (selectedHeroController.GetGameObject().TryGetComponent(out IWeaponController weaponController))
                {
                    Transform spawnProjectileTransform = weaponController.GetWeaponTransform();
                    GameObject projectileGameObject = Instantiate(_projectilePrefab, spawnProjectileTransform.position, spawnProjectileTransform.rotation);

                    if (projectileGameObject.TryGetComponent(out IProjectile projectile))
                    {
                        ICombatController selectedEnemy = targets.Find(x => x.IsSelected());
                        projectile.Launch(selectedEnemy.GetImpactTransform(), _projectileTimeImpact);
                    }
                }
            });
        }
    }
}