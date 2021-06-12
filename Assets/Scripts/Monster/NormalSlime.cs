using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalSlime : MonoBehaviour
{
    public Enemy enemy;
    void Awake()
    {
        enemy = new Enemy(this.gameObject, GetComponentInChildren<HpObject>());
        enemy.InitInterface();//初始化接口
        //enemy.Updata();
        //Debug.Log(this.transform.localPosition);
    }
    void Start()
    {
        enemy.SetElement();


    }
    void Update()
    {   
        enemy.Updata();
        
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (enemy.Injured(collisionInfo))
        {
            InjureStop();
        }
            
    }

    public void Injured(bool forward,InjuredModel injuredModel)
    {
        enemy.Injured(forward, injuredModel);
        InjureStop();
    }

    public void SetElement(Color color)//设置元素接口
    {
        enemy.SetElement(color);
    }

    public void InjureStop()
    {
        StartCoroutine(enemy.StopMove(1f));
    }

    public void Dead()
    {
        enemy.monsterManager.isDead = true;
        StartCoroutine(LayDead());
    }

    IEnumerator LayDead()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
}
