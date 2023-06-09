using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemySystem : MonoBehaviour
{
    public List<BaseEnemy> Enemies { get; private set; }

    [SerializeField] private Transform _firstSpawnPosition;
    [SerializeField] private Transform _secondSpawnPosition;
    [SerializeField] private MainBuilding _mainBuilding;
    [SerializeField] private EnemiesLibrary _enemiesLibrary;
  
    



    private void Awake()
    {
        Enemies = new List<BaseEnemy>();
    }

    private void Start()
    {
        SceneEventSystem.Instance.EnemyDied += OnEnemyDied;
    }

    private void OnDestroy()
    {
        SceneEventSystem.Instance.EnemyDied -= OnEnemyDied;
    }

    private void OnEnemyDied(BaseEnemy enemy, bool giveReward)
    {
        if (Enemies.Contains(enemy))
        {
            Enemies.Remove(enemy);
        }
    }

    public List<EnemySaveInfo> GetSaveInfo()
    {
        List<EnemySaveInfo> saveInfo = new List<EnemySaveInfo>();

        foreach (BaseEnemy enemy in Enemies)
        {
            saveInfo.Add(enemy.GetSaveInfo());
        }

        return saveInfo;
    }

    public void SpawnWaveUnit(WaveData waveData, float tickTime)
    {
        StartCoroutine(SpawnEnemiesInTime(waveData, tickTime));
    }

    private IEnumerator SpawnEnemiesInTime(WaveData waveData, float tickTime)
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(tickTime);
        Vector3[] points = new Vector3[2];
        points[0] = _firstSpawnPosition.position;
        points[1] = _secondSpawnPosition.position;

        foreach (WaveData.WaveEnemyData waveEnemyData in waveData.Enemies)
        {
            GameObject enemyPrefab = waveEnemyData.EnemyData.Prefab;
            for (int i = 0; i < waveEnemyData.Count; i++)
            {
                int index = i % 2;
                GameObject enemy = Instantiate(enemyPrefab, points[index], Quaternion.identity);
                BaseEnemy baseEnemy = enemy.GetComponent<BaseEnemy>();

                baseEnemy.Initialize(waveEnemyData.EnemyData, _mainBuilding, waveEnemyData.EnemyData.Health);
                Enemies.Add(baseEnemy);
                yield return waitForSeconds;
            }
        }
    }

    public void LoadEnemies(List<EnemySaveInfo> saveEnemies){
        Debug.Log("Loading Enemies...");
        Dictionary<string, EnemyData> _enemiesMap = new Dictionary<string, EnemyData>();
        foreach(EnemyData enemyData in _enemiesLibrary.enemies){
            _enemiesMap.Add(enemyData.Name, enemyData);
        }
        Debug.Log("Enemies added to the map");
        foreach(EnemySaveInfo savedEnemy in saveEnemies){
            Debug.Log("Start of foreach");
            GameObject enemyPrefab =_enemiesMap[savedEnemy.Name].Prefab;
            Debug.Log("Error happened?");
            GameObject enemy = Instantiate(enemyPrefab, savedEnemy.Position, Quaternion.identity);
            BaseEnemy baseEnemy = enemy.GetComponent<BaseEnemy>();

            baseEnemy.Initialize(_enemiesMap[savedEnemy.Name], _mainBuilding, savedEnemy.Health);
            Enemies.Add(baseEnemy);

        }
        Debug.Log("Enemies initialized");
    }
}
