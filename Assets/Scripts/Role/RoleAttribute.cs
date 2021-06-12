using UnityEngine;

public class RoleAttribute : CharacterAttributes
{
    public float speedFast = 3f;//快速移动速度
    public float jumpHeight = 1f;//跳跃的最大高度
    public int jumpTimes = 2;//跳跃的最大次数
    public float moveRate = 2f;//移动倍率
    public float jumpRate = 5f;//跳跃倍率
    public float accelerateSpeed = 2.5f;//加速度
    public float chargeJumpPower = 2f;//蓄力跳
    public float jumpStartSpeed = 5f;//初始速度
    public float jumpContinueMax = 45f;//跳跃持续最大时间
    public float jumpAdd = 1f;//跳跃持续增量
    public float downMaxSpeed = 5f;//下落最大速度
    public float mass = 1f;//重力
    public float SwordAdd = 1f;//剑击持续增量
    public float SwordContinueMax = 45f;//剑击持续最大时间
}
