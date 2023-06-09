using UnityEngine;

public class TankEnemy : BaseEnemy
{
    private float _nextTimeAttack;

    private void Update()
    {
        if (!_isAttacking)
            return;

        if (_enemyState == EnemyState.Move)
        {
            if(!_navMesh.hasPath) return;
            if (_navMesh.remainingDistance < 1f)
            {
                _navMesh.isStopped = true;
                _enemyState = EnemyState.Attack;
            }
        }
        else
        {
            Attack();
        }
    }

    private void Attack()
    {
        if(_mainBuilding)
            _mainBuilding.GetDamage(Damage);

        Death(false);
    }
}
