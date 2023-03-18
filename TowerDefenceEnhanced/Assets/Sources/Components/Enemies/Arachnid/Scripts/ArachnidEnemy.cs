using System.Collections;
using UnityEngine;

public class ArachnidEnemy : BaseEnemy
{
    
    private Animator _animator;
    private void Start(){
        _animator = GetComponent<Animator>();
    }
    
    private void Update()
    {
        if(!_isAttacking)
            return;

        if (_enemyState == EnemyState.Move)
        {
            _animator.SetBool("isMoving", true);
            if (_navMesh.remainingDistance < 5)
            {
                _navMesh.isStopped = true;
                
                _enemyState = EnemyState.Attack;
            }
        }
        else
        {
            _animator.SetBool("isMoving", false);
            Attack();
        }

    }

    private void Attack()
    {
        if (_mainBuilding)
            _mainBuilding.GetDamage(Damage* Time.deltaTime * _attackRate);
    } 
}