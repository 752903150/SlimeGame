using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSwordAttack : MonoBehaviour
{
    GameObject parent;
    Player player;
    Animator animator;
    Vector3 offset;
    bool isAttackOK = false;//是否攻击命中
    bool isAttack = false;//是否正在攻击
    bool isPickOut = false;//是否是挑击
    bool attackToward = false;//false向左攻击
    // Start is called before the first frame update
    void Awake()
    {
        offset = new Vector3(0f,0f,0f);
        animator = GetComponent<Animator>();
        parent = transform.parent.gameObject;

    }
    private void Start()
    {
        if (parent == null)
        {
            parent = GameObject.Find("Slime");
        }
        player = parent.GetComponent<SlimeRole>().player;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.roleMoveManager.attacktDireaction == 3)//左普通攻击
        {
            isAttack = true;
            attackToward = false;
            offset.x = -0.6f;
            offset.y = 0.2f;
            animator.SetBool("SwordLeft", true);
            StartCoroutine(StopMove("SwordLeft"));
        }
        else if (player.roleMoveManager.attacktDireaction == 4)//右普通攻击
        {
            isAttack = true;
            attackToward = true;
            offset.x = 0.6f;
            offset.y = 0.2f;
            animator.SetBool("SwordRight", true);
            StartCoroutine(StopMove("SwordRight"));
        }
        else if(player.roleMoveManager.attacktDireaction == 5)//左挑击
        {
            isPickOut = true;
            attackToward = false;
            offset.x = -0.6f;
            offset.y = 0.1f;
            animator.SetBool("SwordUpLeft", true);
            StartCoroutine(StopMove("SwordUpLeft"));
        }
        else if(player.roleMoveManager.attacktDireaction == 6)//右挑击
        {
            isPickOut = true;
            attackToward = true;
            offset.x = 0.6f;
            offset.y = 0.1f;
            animator.SetBool("SwordUpRight", true);
            StartCoroutine(StopMove("SwordUpRight"));
        }
        else
        {
            animator.SetBool("SwordUpLeft", false);
            animator.SetBool("SwordUpRight", false);
            animator.SetBool("SwordLeft", false);
            animator.SetBool("SwordRight", false);
        }
        transform.localPosition = parent.transform.localPosition + offset;
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag=="Monster"&&!isAttackOK)//未进入冷却且发动了攻击
        {
            if(isAttack)//普通攻击
            {
                collision.gameObject.GetComponent<NormalSlime>().Injured(attackToward,InjuredModel.Normal);
            }
            else if(isPickOut)//挑击
            {
                collision.gameObject.GetComponent<NormalSlime>().Injured(attackToward, InjuredModel.SwordPickOut);
            }
            isAttackOK = true;
            StartCoroutine(AttackCD());
        }
            
    }
    public IEnumerator StopMove(string msg)
    {
        yield return new WaitForSeconds(0.5f);//1秒攻击间隔
        player.roleMoveManager.attacktDireaction = 0;
        animator.SetBool(msg, false);
        isAttack = false;
        isPickOut = false;
    }
    public IEnumerator AttackCD()
    {
        yield return new WaitForSeconds(0.5f);//1秒攻击间隔
        isAttackOK = false;
    }
}
