using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSourceEnemy;
    [SerializeField] private AudioSource _audioSourceTower;
    [SerializeField] private AudioSource _audioSourceUpgrade;
    // Start is called before the first frame update
    void Start()
    {
        SceneEventSystem.Instance.EnemyDied += EnemyDied;
        SceneEventSystem.Instance.CellUsed += TowerBuild;
        SceneEventSystem.Instance.OnUpgradeButtonPressed+= OnUpgradeButtonPressed;
    }

    private void TowerBuild(TowerCell towerCell)
    {
        _audioSourceTower.PlayOneShot(_audioSourceTower.clip, 0.5f);
    }

    private void OnUpgradeButtonPressed()
    {
        _audioSourceUpgrade.PlayOneShot(_audioSourceUpgrade.clip, 0.5f);
    }

    private void EnemyDied(BaseEnemy enemy, bool giveReward)
    {
        _audioSourceEnemy.PlayOneShot(_audioSourceEnemy.clip, 0.25f);
    }

}
