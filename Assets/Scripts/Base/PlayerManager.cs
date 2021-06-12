using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager
{
    private static PlayerManager playerManager;
    List<SlimeRole> slimes;
    int index=0;
    private PlayerManager()
    {
        slimes = new List<SlimeRole>();
    }

    public void AddPlayer(SlimeRole slimeRole)
    {
        //Debug.Log(slimeRole == null);
        if(slimes.Count!=0)
            slimeRole.control = false;
        slimes.Add(slimeRole);
        //index = (index + 1) % slimes.Count;
    }

    public void ChangePlayer()
    {
        if(slimes.Count>=2)
        {
            slimes[index].control = false;
            index = (index + 1) % slimes.Count;
            slimes[index].control = true;
        }
    }

    public static PlayerManager _PlayerManager
    {
        get
        {
            if (playerManager == null)
            {
                playerManager = new PlayerManager();
                return playerManager;
            }
            else
            {
                return playerManager;
            }
        }
    }
}
