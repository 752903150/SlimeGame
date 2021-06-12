using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpObject :MonoBehaviour
{
    //敌人的死亡动画
    public int maxHp;

    // Start is called before the first frame update
    void Start()
    {
        /*Debug.Log(gameObject.transform.parent.GetComponent<NormalSlime>() == null);
        Debug.Log(gameObject.transform.parent.GetComponent<NormalSlime>() == null);
        Debug.Log(gameObject.transform.parent.GetComponent<NormalSlime>() == null);*/
        maxHp = gameObject.transform.parent.GetComponent<NormalSlime>().enemy.monsterAttributes.maxHp;//属性初始化        
    }

    public void Injured()
    {
        Debug.Log("当前血量" + maxHp);
        maxHp -= 1;
        if(maxHp == 1)
        {
            Dead();
        }
    }

    public void Dead()
    {
        Debug.Log("死亡动画播放");
        this.transform.parent.GetComponentInChildren<NormalAnimation>().Dead();
        GetComponentInParent<NormalSlime>().Dead();
        
    }

}
