using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class powerupController  {

    private static List<GameObject> powerupPool;
    public static int points = -1;

    public static void AddPoweup(GameObject _powerup)
    {
        if (points < 0)
            Init();
        powerupPool.Add(_powerup);
    }

    public static void KillPowerup(GameObject _powerup, int killer)
    {
        powerupPool.Remove(_powerup);
    }

    static void Init()
    {
        powerupPool = new List<GameObject>();
        points = 0;
    }
}
