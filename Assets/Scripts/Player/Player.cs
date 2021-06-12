using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : RoleBase
{
    int groundLayer = 1 << 8;//地面图层
    Vector3 checkVector;
    Vector3 checkVector2;
    GameObject player;//保存玩家
    SlimeAnimation slimeAnimation;//获取动画，用于动画同步
    MoveModel moveModel;
    Rigidbody2D test;
    public RoleMoveManager roleMoveManager;//控制表
    public RoleState roleState;//状态表
    public SkillManager skillManager; 

    public RoleAttribute roleAttribute;//玩家基础属性
    public RoleMoveInterface roleMoveInterface;
    public AttackInterface attackInterface;
    public InjuredInterface injuredInterface;
    public Player(GameObject player)
    {
        moveModel = MoveModel.Normal;
        this.player = player;
        test = player.GetComponent<Rigidbody2D>();
        skillManager = new SkillManager();
        roleAttribute = new RoleAttribute();
        roleMoveManager.AllInit(roleAttribute.jumpTimes);
        checkVector = new Vector3(-0.2f, -1f, 0f);
        checkVector2 = new Vector3(0.2f, -1f, 0f);
        slimeAnimation = player.GetComponentInChildren<SlimeAnimation>();
    }
    public override void SetMoveInterface()//玩家移动接口初始化
    {
        roleMoveInterface = new RoleMove();
        roleMoveInterface.InitObject(player, roleAttribute);
    }
    public override void SetAttackInterface()//玩家攻击接口初始化
    {
        attackInterface = new RoleAttack();
        attackInterface.InitObject(player, roleAttribute);
    }
    public override void SetInjuredInterface()//玩家受伤接口实例化
    {
        injuredInterface = new RoleInjured();
        injuredInterface.SetPlayer(player.GetComponent<Rigidbody2D>());
    }
    
    public void InitInterface()//初始化人物接口
    {
        SetMoveInterface();
        SetAttackInterface();
        SetInjuredInterface();
        skillManager.Learn(Skill.DoubleJump);//二段跳
        skillManager.Learn(Skill.SwordNormalAttack);//普通剑攻击
        skillManager.Learn(Skill.SwordPickOut);//挑击
        skillManager.Learn(Skill.SwordSprintAttack);//冲刺攻击
    }

    public void SetElement()//设置元素接口
    {
        slimeAnimation.SetColor(roleAttribute.color);
    }
    public void SetElement(Color color)//设置元素接口
    {
        roleAttribute.color = color;
        slimeAnimation.SetColor(color);
    }
    
    public void Split(bool flag)//分裂动画接口
    {
        roleMoveManager.isSplit = flag;        
    }
    public void LearnSkill(Skill skill)//学习技能接口
    {
        skillManager.Learn(skill);
    }

    public void ChangeMoveModel(MoveModel model)
    {
        if(model != moveModel)
        {
            if(model == MoveModel.Climb)//其他转换成爬行
            {
                
                moveModel = model;
                roleMoveManager.AllInit(roleAttribute.jumpTimes);
                roleMoveInterface.RoleNormalMove(MoveModel.Static);
                roleMoveManager.ClimbAnimation = true;
            }
            else if(model == MoveModel.Normal)//其他转换成普通移动
            {
                
                moveModel = model;
                roleMoveInterface.RoleClimeMove(MoveModel.Normal);
                roleMoveManager.AllInit(roleAttribute.jumpTimes);
                roleMoveManager.ClimbAnimation = false;
            }
        }
    }

    public void UpdateMove()//按键控制移动开关
    {
        //Debug.Log(roleMoveManager.ClimbAnimation);
        if(moveModel == MoveModel.Normal)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                ChangeMoveModel(MoveModel.Climb);
            }
            
            NormalMove();
            JumpMove();
        }
        else if(moveModel == MoveModel.Climb)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                ChangeMoveModel(MoveModel.Normal);
            }
            ClimbMove();
        }
        
    }
    
    public void FixedUpdateMove()
    {
        if (moveModel == MoveModel.Normal)
        {
            NormalFixedUpdateMove();
        }
        else if (moveModel == MoveModel.Climb)
        {
            ClimbFixedUpdateMove();
        }
    }
    
    public void ClimbFixedUpdateMove()//攀爬响应控制脚本
    {
        //roleMoveInterface.RoleClimeMove(MoveModel.Climb);
    }
    
    public void NormalFixedUpdateMove()//普通响应控制脚本
    {
        if (roleMoveManager.rightFastMove && !roleState.injured)
        {
            roleState.forword = true;
            roleMoveInterface.RoleFastMove(MoveModel.Right);
        }
        else if (roleMoveManager.leftFastMove && !roleState.injured)
        {
            roleState.forword = false;
            roleMoveInterface.RoleFastMove(MoveModel.Left);
        }
        if (roleMoveManager.rightMove && !roleState.injured)
        {
            roleState.forword = true;
            roleMoveInterface.RoleNormalMove(MoveModel.Right);
        }
        else if (roleMoveManager.leftMove && !roleState.injured)
        {
            roleState.forword = false;
            roleMoveInterface.RoleNormalMove(MoveModel.Left);
        }
        if (roleMoveManager.isStop() && roleState.stopTime == 0 && !roleState.injured)
        {
            roleMoveInterface.RoleNormalMove(MoveModel.Stop);
        }
    }

    public void Attack()//攻击接口
    {
        /*********攻击*********/
        if (Input.GetKeyDown(KeyCode.J))
        {
            
            if(!roleState.forword && roleMoveManager.attacktDireaction != 1)
            {
                roleMoveManager.attacktDireaction = 1;//左攻击
            }
            else if(roleState.forword&&roleMoveManager.attacktDireaction != 2)
            {
                roleMoveManager.attacktDireaction = 2;//右攻击
            }  
        }
        if (Input.GetKeyDown(KeyCode.K))
        {

            
        }
        if(Input.GetKey(KeyCode.K)&&skillManager.Islearn(Skill.SwordPickOut)&&!roleMoveManager.isPickOut)
        {
            roleState.SwordContinue += roleAttribute.SwordAdd;
            if(roleState.SwordContinue>=roleAttribute.SwordContinueMax)
            {
                //Debug.Log("蓄力完成");
                roleMoveManager.isPickOut = true;
            }
        }
        if(Input.GetKeyUp(KeyCode.K))
        {
            if (roleState.SwordContinue >= roleAttribute.SwordContinueMax && roleMoveManager.isPickOut)
            {
                if (!roleState.forword && roleMoveManager.attacktDireaction != 5)
                {
                    roleMoveManager.attacktDireaction = 5;//左剑挑击攻击
                }
                else if (roleState.forword && roleMoveManager.attacktDireaction != 6)
                {
                    roleMoveManager.attacktDireaction = 6;//右剑挑击攻击
                }
            }
            else
            {
                if (!roleState.forword && roleMoveManager.attacktDireaction != 3)
                {
                    roleMoveManager.attacktDireaction = 3;//左剑攻击
                }
                else if (roleState.forword && roleMoveManager.attacktDireaction != 4)
                {
                    roleMoveManager.attacktDireaction = 4;//右剑攻击
                }
            }
            roleMoveManager.isPickOut = false;
            roleState.SwordContinue = 0f;
        }
        if (slimeAnimation.isAttackAnimation)
        {

            attackInterface.RoleNormalAttack(AttackModel.Normal, roleState.forword);
        }
    }

    public void Dead()//死亡接口
    {
        roleMoveManager.isDead = true;
    }
    public bool Injured(Collision2D collisionInfo)//受伤接口
    {
        roleMoveManager.MoveInit();
        if (collisionInfo.collider.tag == "Monster")
        {
            roleState.injured = true;
            if (collisionInfo.gameObject.transform.localPosition.x > player.transform.localPosition.x)
            {
                roleMoveManager.rightInjured = false;
                roleMoveManager.leftInjured = true;
                injuredInterface.NormalInjured(InjuredModel.LeftInjured,InjuredModel.Normal);
            }
            else
            {
                roleMoveManager.rightInjured = true;
                roleMoveManager.leftInjured = false;
                injuredInterface.NormalInjured(InjuredModel.RightInjured, InjuredModel.Normal);
            }
            return true;
        }
        return false;
    }

    void ClimbMove()
    {
        if(!roleMoveManager.isClimb)
        {
            test.bodyType = RigidbodyType2D.Dynamic;
            test.velocity = new Vector3(0f, 0f, 0f);
            roleMoveManager.isClimb = true;
        }
    }
    
    void SkillMove()//技能按键响应
    {
        
    }
    void NormalMove()//普通移动，不包含跳跃
    {
        /*****移动部分****/
        if (Input.GetKey(KeyCode.A) && !roleState.injured)
        {

            roleMoveManager.rightMove = false;//右快速移动
            roleMoveManager.leftFastMove = false;//左快速移动
            roleMoveManager.rightFastMove = false;//右快速移动


            roleMoveManager.leftMove = true;
            roleMoveManager.stop = false;
        }
        else if (Input.GetKey(KeyCode.D) && !roleState.injured)
        {
            roleMoveManager.leftMove = false;//左快速移动

            roleMoveManager.leftFastMove = false;//左快速移动
            roleMoveManager.rightFastMove = false;//右快速移动


            roleMoveManager.rightMove = true;
            roleMoveManager.stop = false;
        }
        if (roleMoveManager.rightMove && Input.GetKeyDown(KeyCode.LeftShift) && !roleState.injured)
        {
            roleMoveManager.leftMove = false;//左快速移动
            roleMoveManager.rightMove = false;//右快速移动
            roleMoveManager.leftFastMove = false;//左快速移动

            roleMoveManager.rightFastMove = true;
            roleMoveManager.stop = false;
        }
        else if (roleMoveManager.leftMove && Input.GetKeyDown(KeyCode.LeftShift) && !roleState.injured)
        {
            roleMoveManager.leftMove = false;//左快速移动
            roleMoveManager.rightMove = false;//右快速移动
            roleMoveManager.rightFastMove = false;//右快速移动

            roleMoveManager.leftFastMove = true;
            roleMoveManager.stop = false;
        }
        if ((Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) && !roleState.injured)
        {
            roleMoveInterface.RoleNormalMove(MoveModel.Stop);
            roleMoveManager.leftMove = false;//左快速移动
            roleMoveManager.rightMove = false;//右快速移动
            roleMoveManager.leftFastMove = false;//左快速移动
            roleMoveManager.rightFastMove = false;//右快速移动

            roleMoveManager.stop = true;
            roleState.stopTime = 0;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            roleMoveManager.rightFastMove = false;
            roleMoveManager.leftFastMove = false;
        }
    }
    
    void JumpMove()//跳跃移动
    {
        if (Input.GetKeyDown(KeyCode.Space) && !roleState.injured)
        {
            roleState.jumpContinue = 0f;
            if (Physics2D.Raycast(player.transform.position, checkVector, 0.6f, groundLayer)||
                Physics2D.Raycast(player.transform.position, checkVector2, 0.6f, groundLayer))
            {

                roleMoveManager.jump = true;
                roleMoveManager.stop = false;
                roleMoveInterface.RoleNormalMove(MoveModel.Jump);
                roleState.jumpTimes = 1;
            }
            else if (roleState.jumpTimes < roleAttribute.jumpTimes)
            {

                roleMoveManager.jump = true;
                roleMoveManager.stop = false;
                roleMoveInterface.RoleNormalMove(MoveModel.Jump);
                roleState.jumpTimes++;
            }
        }
        if (Input.GetKey(KeyCode.Space) && !roleState.injured)
        {
            
            if (Physics2D.Raycast(player.transform.position, checkVector, 0.6f, groundLayer)||
                Physics2D.Raycast(player.transform.position, checkVector2, 0.6f, groundLayer))
            {
                roleState.onGround = true;
            }
            else
            {
                roleState.onGround = false;
            }
            if (roleState.jumpTimes <= roleAttribute.jumpTimes && roleState.jumpContinue < roleAttribute.jumpContinueMax && !roleState.onGround)
            {
                roleState.jumpContinue += roleAttribute.jumpAdd;
                roleMoveInterface.RoleJump();
            }
            else if (!roleState.onGround)
            {
                roleMoveManager.jump = false;
                roleMoveInterface.RoleStopJump();
                roleMoveManager.stop = true;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space) && !roleState.injured)
        {
            roleMoveManager.jump = false;
            roleState.jumpContinue = 0f;
            roleMoveManager.stop = true;
        }
        if (roleMoveManager.jump == false)
        {
            if (Physics2D.Raycast(player.transform.position, checkVector, 0.6f, groundLayer)||
                Physics2D.Raycast(player.transform.position, checkVector2, 0.6f, groundLayer))
            {
                roleState.onGround = true;
            }
            else
            {
                roleState.onGround = false;
            }
            if (!roleState.onGround)
                roleMoveInterface.RoleStopJump();
        }
    }

    public IEnumerator StopMove()
    {
        yield return new WaitForSeconds(1f);
        roleState.injured = false;
        roleMoveManager.rightInjured = false;
        roleMoveManager.leftInjured = false;
        roleMoveInterface.RoleNormalMove(MoveModel.Stop);
    }
}
