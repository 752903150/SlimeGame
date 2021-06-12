using UnityEngine;

public struct RoleState
{
    public int jumpTimes;//当前跳跃次数
    public int stopTime;
    public float jumpContinue;//当前跳跃持续时间
    public bool forword;//默认向左攻击
    public bool onGround;//是否在地面上
    public bool injured;//是否受伤
    public float SwordContinue;//剑击持续时间
    public void InitState()
    {
        jumpTimes = 0;//当前跳跃次数
        stopTime = 0;
        jumpContinue = 0;//当前跳跃持续时间
        forword = false;//默认向左攻击
        onGround = true;//是否在地面上
        injured = false;//是否受伤
    }
}
