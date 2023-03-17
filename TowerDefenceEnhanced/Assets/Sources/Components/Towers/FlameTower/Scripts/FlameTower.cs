using System;
using UnityEngine;

public class FlameTower : BaseTower, IUpgradable
{
    [SerializeField] private ParticleSystem[] _fireEffectLevel1;
    [SerializeField] private ParticleSystem[] _fireEffectLevel2;
    [SerializeField] private ParticleSystem[] _fireEffectLevel3;

    private ParticleSystem[] _currentFireEffects;

    private void Start()
    {
        SetCurrentFireEffect();
    }

    public void Upgrade()
    {
        Level++;
        UpdateStats();
        ChangeLevelModel();

        SetCurrentFireEffect();
    }

    private void SetCurrentFireEffect()
    {
        if (Level == 0)
            _currentFireEffects = _fireEffectLevel1;
        else if (Level == 1)
            _currentFireEffects = _fireEffectLevel2;
        else
            _currentFireEffects = _fireEffectLevel3;
    }

    private void Update()
    {
        if (AvailableEnemies.Count == 0 || !IsActive)
        {
            SetFireParticles(false);
            return;
        }
        BaseEnemy[] targets = new BaseEnemy[3];
        
        targets[0] = AvailableEnemies[0];
        if (targets[0] == null)
        {
            AvailableEnemies.RemoveAt(0);
            return;
        }

        try{
            targets[1] = AvailableEnemies[1];
        }catch(ArgumentOutOfRangeException){
            targets[1]=null;
        }

        try{
            targets[2] = AvailableEnemies[2];
        }catch(ArgumentOutOfRangeException){
            targets[2]=null;
        }
        
        

        Vector3 toEnemyVector = targets[0].transform.position - transform.position;
        toEnemyVector.y = 0;
        _rotateElement.right = toEnemyVector;
        Attack(targets);
        
    }

    private void Attack(BaseEnemy[] targets)
    {
        SetFireParticles(true);
        targets[0].GetDamage(BaseDamage * Time.deltaTime);
        if(targets[1]!=null) targets[1].GetDamage(BaseDamage * Time.deltaTime);
        if(targets[2]!=null) targets[2].GetDamage(BaseDamage * Time.deltaTime);
    }

    private void SetFireParticles(bool isPlaying)
    {
        if(isPlaying)
        {
            for(int i = 0; i < _currentFireEffects.Length; i++)
            {
                if (!_currentFireEffects[i].isPlaying)
                    _currentFireEffects[i].Play();
            }
        }
        else
        {
            for (int i = 0; i < _currentFireEffects.Length; i++)
            {
                _currentFireEffects[i].Stop();
            }
        }
    }
}
