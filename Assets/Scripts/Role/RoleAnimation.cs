using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleAnimation : MonoBehaviour
{
    private new Animation animation;
    // Use this for initialization
    void Start()
    {
        Debug.Log("动画初始化");
        animation = GetComponent<Animation>();//找到Animation组件
        foreach (AnimationState state in animation)
        {
            Debug.Log(state.name);
            Debug.Log("");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))//如果点击鼠标左键
        {
            //animation.Play();  //用于默认的动画
            //animation.Play("Donghua");   //动画瞬间变化
            animation.CrossFade("RightMove", 1f); //1s之后淡出其他动画，淡入Donghua动画
        }
        else if (Input.GetMouseButtonDown(1))//如果点击鼠标右键
        {
            //animation.Play("Donghua1");
            animation.CrossFade("Attack", 1f);
        }
        else if (Input.GetMouseButtonDown(2))
        {
            animation.Rewind("RightMove");  //把动画倒放
            //animation.Stop();  //停止播放动画  动画停止之后再播放动画是从头开始播放
        }
    }
}
