using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombies : MonoBehaviour
{
    public List<Enemy> Enemies;
    
    public void Reset()
    {
        foreach(Enemy enemy in Enemies)
            enemy.Reset();
    }
}
