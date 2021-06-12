using UnityEngine;

public interface AttackInterface
{
    void InitObject(GameObject gameObject , RoleAttribute roleAttributes);//初始化对象
    void RoleNormalAttack(AttackModel attackModel,bool forward);//人物普通攻击接口
}