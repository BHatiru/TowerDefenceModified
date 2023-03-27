using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Enemies Library", menuName = "Data/Enemies Library")]
public class EnemiesLibrary : ScriptableObject
{
    public List<EnemyData> enemies;
}
