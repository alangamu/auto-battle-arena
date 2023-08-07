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
        private GameEvent _heroHitTargetEvent;
        [SerializeField]
        private GameEvent _enemyHitTargetEvent;
        [SerializeField]
        private GameEvent _heroShootEvent;
        [SerializeField]
        private GameEvent _enemyShootEvent;
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

        private bool _isCriticalHit = false;
        private ICombatController _target;
        private ICombatController _attacker;

        private void OnEnable()
        {
            _heroAttackEvent.OnRaise += HeroAttack;
            _enemyAttackEvent.OnRaise += EnemyAttack;
            _heroHitTargetEvent.OnRaise += HeroHitTarget;
            _heroShootEvent.OnRaise += HeroShootProjectile;
            _enemyShootEvent.OnRaise += EnemyShootProjectile;
            _enemyHitTargetEvent.OnRaise += EnemyHitTarget;
        }

        private void OnDisable()
        {
            _heroAttackEvent.OnRaise -= HeroAttack;
            _enemyAttackEvent.OnRaise -= EnemyAttack;
            _heroHitTargetEvent.OnRaise -= HeroHitTarget;
            _heroShootEvent.OnRaise -= HeroShootProjectile;
            _enemyShootEvent.OnRaise -= EnemyShootProjectile;
            _enemyHitTargetEvent.OnRaise -= EnemyHitTarget;
        }

        private void EnemyHitTarget()
        {
            HitTarget(_attacker, _target);
        }

        private void HeroHitTarget()
        {
            HitTarget(_attacker, _target);
        }

        private void EnemyShootProjectile()
        {
            IWeaponController weaponController = _attacker.GetGameObject().GetComponentInChildren<IWeaponController>();
            if (weaponController != null)
            {
                ShootProjectile(weaponController, _attacker, _target);
            }
        }

        private void HeroShootProjectile()
        {
            if (_attacker.GetGameObject().TryGetComponent(out IWeaponController weaponController))
            {
                ShootProjectile(weaponController, _attacker, _target);
            }
        }

        private void ShootProjectile(IWeaponController weaponController, ICombatController attackerCombatController, ICombatController targetCombatController)
        {
            Transform spawnProjectileTransform = weaponController.GetWeaponTransform();
            GameObject projectileGameObject = Instantiate(weaponController.GetWeapon().ProjectilePrefab, spawnProjectileTransform.position, spawnProjectileTransform.rotation);

            if (projectileGameObject.TryGetComponent(out IProjectile projectile))
            {
                projectile.Launch(targetCombatController.GetImpactTransform(), _projectileTimeImpact);
            }

            LeanTween.delayedCall(_projectileTimeImpact, () => 
            {
                HitTarget(attackerCombatController, targetCombatController);
            });
        }

        private void HitTarget(ICombatController attackerCombatController, ICombatController targetCombatController)
        {
            int damagePoints = GetDamagePoints(attackerCombatController, targetCombatController);
            targetCombatController.GettingDamage(damagePoints, _isCriticalHit);
        }

        private void EnemyAttack()
        {
            _target = _heroCombatRuntimeSet.GetSelectedHero();
            _attacker = _enemyCombatRuntimeSet.GetActiveHero();

            if (_target == null)
            {
                _target = _heroCombatRuntimeSet.GetRandomHero();
            }

            IWeaponController weaponController = _attacker.GetGameObject().GetComponentInChildren<IWeaponController>();
            if (weaponController != null)
            {
                Attack(_attacker, _target, weaponController.GetWeapon());
            }
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
            _target = _enemyCombatRuntimeSet.GetSelectedHero();
            _attacker = _heroCombatRuntimeSet.GetActiveHero();

            if (_target == null )
            {
                _target = _enemyCombatRuntimeSet.GetRandomHero();
            }

            var heroInventory = _attacker.GetHero().HeroInventory;
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

            Attack(_attacker, _target, weapon);
        }

        private int GetDamagePoints(ICombatController attackerCombatController, ICombatController targetCombatController)
        {
            CombatStats attackerCombatStats = attackerCombatController.GetCombatStats();
            CombatStats targetCombatStats = targetCombatController.GetCombatStats();
            int heroDamage = (int)(attackPowerStat.BaseValue + (attackPowerStat.MultiplierFactor * attackerCombatStats.StatCount(attackPowerStat.StatId)));
            int targetDefense = heroDamage * targetCombatStats.StatCount(defenseStat.StatId) / 10;
            float criticalMult = 1.0f;
            int randomCritical = Random.Range(1, 100);
            int criticalPoints = (int)(criticalChanceStat.BaseValue + (attackerCombatStats.StatCount(criticalChanceStat.StatId) * criticalChanceStat.MultiplierFactor));

            _isCriticalHit = false;

            if (randomCritical < criticalPoints)
            {
                _isCriticalHit = true;
                criticalMult = criticalPowerStat.BaseValue + (criticalPowerStat.MultiplierFactor * attackerCombatStats.StatCount(criticalPowerStat.StatId));
            }

            int totalDamage = (int)(heroDamage * criticalMult) - targetDefense;

            return totalDamage < 0 ? 0 : totalDamage;
        }
    }
}