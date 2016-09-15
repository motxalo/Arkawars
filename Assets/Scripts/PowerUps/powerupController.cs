using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class powerupController  {

    private static List<GameObject> powerupPool;
    
    public static void AddPoweup(GameObject _powerup)
    {
        Init();
        powerupPool.Add(_powerup);
    }

    public static void KillPowerup(GameObject _powerup)
    {
        powerupPool.Remove(_powerup);
    }

    static void Init()
    {
        powerupPool = new List<GameObject>();        
    }
}
