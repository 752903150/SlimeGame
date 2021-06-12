using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAnimation : MonoBehaviour
{
    GameObject parent;
    Player player;
    Animator animator;
    SpriteRenderer spriteRenderer;
    public bool isAttackAnimation = false;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        parent = transform.parent.gameObject;
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        transform.localPosition = parent.transform.localPosition;
        if (player.roleMoveManager.attacktDireaction == 1)
        {
            animator.SetBool("isLeftAttack", true);
            
            isAttackAnimation = true;
        }
        else if (player.roleMoveManager.attacktDireaction == 2)
        {

            animator.SetBool("isLeftAttack", true);
            isAttackAnimation = true;
        }
        if(player.roleMoveManager.leftMove || player.roleMoveManager.leftFastMove|| player.roleMoveManager.leftInjured)
        {
            transform.localScale = new Vector3(1f,1f,1f);
        }
        else if(player.roleMoveManager.rightMove || player.roleMoveManager.rightFastMove|| player.roleMoveManager.rightInjured)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        animator.SetBool("isSplit", player.roleMoveManager.isSplit);
        animator.SetBool("isJump", player.roleMoveManager.jump);
        animator.SetBool("isLeftMove", player.roleMoveManager.leftMove || player.roleMoveManager.leftFastMove|| player.roleMoveManager.rightMove || player.roleMoveManager.rightFastMove);
        //animator.SetBool("isRightMove", player.roleMoveManager.rightMove || player.roleMoveManager.rightFastMove);
        animator.SetBool("LeftInjured", player.roleMoveManager.leftInjured|| player.roleMoveManager.rightInjured);
        //animator.SetBool("RightInjured", player.roleMoveManager.rightInjured);
        animator.SetBool("isClimb", player.roleMoveManager.ClimbAnimation);
        animator.SetBool("isDead", player.roleMoveManager.isDead);
        animator.SetBool("isStop", player.roleMoveManager.stop);
        if (isAttackAnimation)
        {
            StartCoroutine(StopMove());
        }
    }

    public void SetColor(Color color)
    {
        spriteRenderer.color = Element._Elemet.GetColor(color);
    }
    public IEnumerator StopMove()
    {
        yield return new WaitForSeconds(0.5f);//1秒攻击间隔
        player.roleMoveManager.attacktDireaction = 0;
        animator.SetBool("isLeftAttack", false);
        animator.SetBool("isRightAttack", false);
        isAttackAnimation = false ;
    }
    /*public IEnumerator StopSplit()
    {
        yield return new WaitForSeconds(1f);
        player.Split(false);
    }*/
}
