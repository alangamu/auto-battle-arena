using AutoFantasy.Scripts.Interfaces;
using AutoFantasy.Scripts.ScriptableObjects;
using AutoFantasy.Scripts.ScriptableObjects.Events;
using AutoFantasy.Scripts.ScriptableObjects.Items;
using AutoFantasy.Scripts.ScriptableObjects.MovementTypes;
using AutoFantasy.Scripts.ScriptableObjects.Sets;
using AutoFantasy.Scripts.ScriptableObjects.Variables;
using UnityEngine;

namespace AutoFantasy.Scripts.Battle
{
    public class BattleAttack : MonoBehaviour
    {
        [SerializeField]
        private GameEvent _heroAttackEvent;
        [SerializeField]
        private GameEvent _enemyAttackEvent;
        [SerializeField]
        private GameEvent _hitTargetEvent;
        [SerializeField]
        private GameEvent _shootEvent;
        [SerializeField]
        private HeroCombatRuntimeSet _heroCombatRuntimeSet;
        [SerializeField]
        private HeroCombatRuntimeSet _enemyCombatRuntimeSet;
        [SerializeField]
        private FloatVariable _attackDelayVariable;

        [SerializeField]
        private HeroStatSO attackPowerStat;
        [SerializeField]
        private HeroStatSO defenseStat;
        [SerializeField]
        private HeroStatSO criticalChanceStat;
        [SerializeField]
        private HeroStatSO criticalPowerStat;

        [SerializeField]
        private WeaponTypeSO unarmed;
        [SerializeField]
        private ItemTypeSO weaponType;

        [SerializeField]
        private ItemDatabase databaseItem;

        [SerializeField]
        private float _projectileTimeImpact;

        private ICombatController _activeEnemy;
        private ICombatController _activeHero;

        private bool _isCriticalHit = false;

        private void OnEnable()
        {
            _heroAttackEvent.OnRaise += HeroAttack;
            _enemyAttackEvent.OnRaise += EnemyAttack;
            _hitTargetEvent.OnRaise += HitTarget;
            _shootEvent.OnRaise += ShootProjectile;
        }

        private void OnDisable()
        {
            _heroAttackEvent.OnRaise -= HeroAttack;
            _enemyAttackEvent.OnRaise -= EnemyAttack;
            _hitTargetEvent.OnRaise -= HitTarget;
            _shootEvent.OnRaise -= ShootProjectile;
        }

        private void ShootProjectile()
        {
            if (_activeHero.GetGameObject().TryGetComponent(out IWeaponController weaponController))
            {
                Transform spawnProjectileTransform = weaponController.GetWeaponTransform();
                GameObject projectileGameObject = Instantiate(weaponController.GetWeapon().ProjectilePrefab, spawnProjectileTransform.position, spawnProjectileTransform.rotation);

                if (projectileGameObject.TryGetComponent(out IProjectile projectile))
                {
                    projectile.Launch(_activeEnemy.GetImpactTransform(), _projectileTimeImpact);
                }

                LeanTween.delayedCall(_projectileTimeImpact, () => { HitTarget(); });
            }

        }

        private void HitTarget()
        {
            int damagePoints = GetDamagePoints();
            _activeEnemy.GettingDamage(damagePoints, _isCriticalHit);
        }

        private void EnemyAttack()
        {
            _activeEnemy = _heroCombatRuntimeSet.GetSelectedHero();
            _activeHero = _enemyCombatRuntimeSet.GetActiveHero();
            Attack(_activeHero, _activeEnemy, null);
        }

        private void Attack(ICombatController attacker, ICombatController target, WeaponSO weapon)
        {
            float attackDelay = _attackDelayVariable.Value;
            Transform targetTransform = target.GetImpactTransform();

            MovementTypeSO movementType = weapon == null ? unarmed.AttackMovementType : weapon.WeaponType.AttackMovementType;

            GameObject attackerGameObject = attacker.GetGameObject();

            movementType.PerformMovement(attackerGameObject.transform, targetTransform, attackDelay, () =>
            {
                if (attackerGameObject.TryGetComponent(out IAnimationController animationController))
                {
                    animationController.Attack();
                }
            });
        }

        private void HeroAttack()
        {
            _activeEnemy = _enemyCombatRuntimeSet.GetSelectedHero();
            _activeHero = _heroCombatRuntimeSet.GetActiveHero();

            var heroInventory = _activeHero.GetHero().ThisHeroData.HeroInventory;
            WeaponSO weapon = null;
            foreach (var item in heroInventory)
            {
                var itemInventory = databaseItem.GetItem(item);
                if (itemInventory.ItemType == weaponType)
                {
                    weapon = (WeaponSO)itemInventory;
                    continue;
                }
            }

            Attack(_activeHero, _activeEnemy, weapon);
        }

        private int GetDamagePoints()
        {
            CombatStats heroCombatStats = _activeHero.GetCombatStats();
            int heroDamage = (int)(attackPowerStat.BaseValue + (attackPowerStat.MultiplierFactor * heroCombatStats.StatCount(attackPowerStat.StatId)));
            int targetDefense = (int)(defenseStat.BaseValue + (defenseStat.MultiplierFactor * _activeEnemy.GetCombatStats().StatCount(defenseStat.StatId)));
            float criticalMult = 1.0f;
            int randomCritical = Random.Range(1, 100);
            int criticalPoints = (int)(criticalChanceStat.BaseValue + (heroCombatStats.StatCount(criticalChanceStat.StatId) * criticalChanceStat.MultiplierFactor));

            _isCriticalHit = false;
            if (randomCritical < criticalPoints)
            {
                _isCriticalHit = true;
                criticalMult = criticalPowerStat.BaseValue + (criticalPowerStat.MultiplierFactor * heroCombatStats.StatCount(criticalPowerStat.StatId));
            }

            int totalDamage = (int)(heroDamage * criticalMult) - targetDefense;

            return totalDamage < 0 ? 0 : totalDamage;
        }
    }
}