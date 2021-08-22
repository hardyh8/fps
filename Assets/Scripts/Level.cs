using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Zombies zombieses;
    
    public void Reset()
    {
        zombieses.Reset();
    }
}
