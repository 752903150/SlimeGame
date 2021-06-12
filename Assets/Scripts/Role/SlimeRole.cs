using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SlimeRole : MonoBehaviour
{
    static int SubPlayerNum = 0;
    public GameObject SubPlayer;
    public Player player;
    public RoleAttribute roleAttributes;//人物属性
    public RoleMoveManager roleMoveManager;
    public Rigidbody2D test;
    public bool control=true;
    [SerializeField]
    //float jumpContinueMax = 2f;

    private HpBar hpBar;
    public HpBar HpBar
    {
        set
        {
            hpBar = value;
            if (hpBar != null) hpBar.Init(player.roleAttribute.maxHp);
        }
    }
    public void BindHpBar(HpBar bar)
    {
        HpBar = bar;
    }

    public void LearnSkill(Skill skill)//学习技能接口
    {
        player.LearnSkill(skill);
    }

    public void SetElement(Color color)//设置元素接口
    {
        player.SetElement(color);
    }

    public void Dead()//死亡接口
    {
        player.Dead();
    }

    public void ChangeMoveModel(MoveModel model)//用MoveModel.Normal  MoveModel.Climb进行转换
    {
        player.ChangeMoveModel(model);
    }
    void Awake()
    {
        InitializeInterface();
    }

    private void Start()
    {
        transform.DetachChildren();
        test = GetComponent<Rigidbody2D>();
        player.SetElement();
        PlayerManager._PlayerManager.AddPlayer(this);
    }

    void InitializeInterface()//初始化接口
    {
        player = new Player(this.gameObject);
        player.InitInterface();
        roleAttributes = player.roleAttribute;
    }
    void Update()
    {
        if (control)
        {
            if (Input.GetKeyDown(KeyCode.L))//分裂
            {
                if (SubPlayerNum == 0)
                {
                    SubPlayerNum++;
                    player.Split(true);
                    StartCoroutine(StopSplit());
                    
                }
            }
            if(Input.GetKeyDown(KeyCode.F1))
            {
                PlayerManager._PlayerManager.ChangePlayer();
            }
            player.UpdateMove();
            player.Attack();
        }
    }
    void FixedUpdate()
    {
        //if(control)
        player.FixedUpdateMove();
    }
    void OnCollisionEnter2D(Collision2D collisionInfo)
    {
        if (player.Injured(collisionInfo))
        {
            StartCoroutine(player.StopMove());
            hpBar.OnHpChanged(-1);
        }

    }
    public IEnumerator StopSplit()
    {
        Debug.Log("1");
        yield return new WaitForSeconds(1f);
        Debug.Log("2");
        player.Split(false);
        Vector3 postion = this.transform.localPosition + new Vector3(0.5f, 0f, 0f);
        Quaternion rota = new Quaternion();
        SubPlayer = Instantiate(Resources.Load("Player/Slime") as GameObject, postion, rota);
        PlayerManager._PlayerManager.AddPlayer(SubPlayer.GetComponent<SlimeRole>());
    }
}
