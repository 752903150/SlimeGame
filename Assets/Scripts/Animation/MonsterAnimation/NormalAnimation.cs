using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAnimation : MonoBehaviour
{
    Enemy enemy;
    Animator animator;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        enemy = GetComponentInParent<NormalSlime>().enemy;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {


        if (enemy.monsterManager.leftAttack || enemy.monsterManager.leftInjured)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (enemy.monsterManager.rightAttack || enemy.monsterManager.rightInjured)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        /*else if (transform.localScale.x != 1f)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }*/
        //animator.SetBool("RightAttack", enemy.monsterManager.rightAttack);
        animator.SetBool("LeftAttack", enemy.monsterManager.leftAttack|| enemy.monsterManager.rightAttack);
        animator.SetBool("LeftInjured", enemy.monsterManager.leftInjured|| enemy.monsterManager.rightInjured);
        //animator.SetBool("RightInjured", enemy.monsterManager.rightInjured);

        
    }
    public void SetColor(Color color)
    {
        spriteRenderer.color = Element._Elemet.GetColor(color);
    }
    public void Dead()
    {
        animator.SetBool("Dead", true);
        animator.SetBool("RightAttack", false);
        animator.SetBool("LeftAttack", false);
        animator.SetBool("LeftInjured", false);
        animator.SetBool("RightInjured", false);
    }
}
