using UnityEngine;

public class RoleMove : RoleMoveInterface
{
    Rigidbody2D rigidbody;
    Transform transform;
    RoleAttribute roleAttributes;//人物属性
    Vector3 moveVector;
    Vector3 resistanceWeight;//抵抗重力向量
    public override void InitObject(GameObject gameObject , RoleAttribute roleAttributes)
    {
        
        rigidbody = gameObject.GetComponent<Rigidbody2D>();
        transform = gameObject.transform;
        this.roleAttributes = roleAttributes;
        resistanceWeight = new Vector3(0f, rigidbody.mass*2, 0f);
    }

    public override void RoleNormalMove(MoveModel model)//普通移动代码
    {
        if (model == MoveModel.Left)
        {
            //Debug.Log("右移");
            RoleNormalRightMove();//左移
        }
        else if(model == MoveModel.Right)
        {
            //Debug.Log("左移");
            RoleNormalLeftMove();//右移
        }
        else if(model == MoveModel.Jump)
        {
            //Debug.Log("跳跃");
            RoleNormalJumpMove();
        }
        else if (model == MoveModel.Static)
        {
            //Debug.Log("静止");
            RoleStaticMove();
        }
        else
        {
            //Debug.Log("停止");
            RoleStopMove();//停止
        }
    }
    public override void RoleClimeMove(MoveModel model)
    {
        //攀爬
        if (model == MoveModel.Climb)
        {
            RoleClimbMove();
        }
        else if(model == MoveModel.Normal)
        {
            RoleClimbNoMove();
        }
    }
    public override void RoleFastMove(MoveModel model)
    {
        if (model == MoveModel.Left)
        {
            RoleFastRightMove();//左移
        }
        else if (model == MoveModel.Right)
        {
            RoleFastLeftMove();//右移
        }
        else
        {
            RoleStopMove();//停止
        }
    }
    
    /**普通移动***/
    public void RoleNormalRightMove()
    {
        if(rigidbody.velocity.x < roleAttributes.speed * roleAttributes.moveRate 
            && rigidbody.velocity.x > -1 * roleAttributes.speed * roleAttributes.moveRate)//未达到最大速度
        {
            moveVector.x = -1 * roleAttributes.speed * roleAttributes.moveRate * roleAttributes.accelerateSpeed;
            moveVector.y = 0f;
            moveVector.z = 0f;
            rigidbody.AddForce(moveVector);
        }
        
    }
    public void RoleNormalLeftMove()
    {
        if (rigidbody.velocity.x < roleAttributes.speed * roleAttributes.moveRate 
            && rigidbody.velocity.x > -1 * roleAttributes.speed * roleAttributes.moveRate)//未达到最大速度
        {
            moveVector.x = roleAttributes.moveRate * roleAttributes.moveRate * roleAttributes.accelerateSpeed;
            moveVector.y = 0f;
            moveVector.z = 0f;
            rigidbody.AddForce(moveVector);
        }
        
    }
    /****跳跃*****/
    public override void RoleJump()//持续跳跃
    {
        
        moveVector.x = 0f;
        moveVector.y = 2F;
        moveVector.z = 0f;
        rigidbody.AddForce(moveVector);
        //Debug.Log("持续跳跃");
    }
    public override void RoleStopJump()//停止跳跃
    {
        
        if (rigidbody.velocity.y > -1*roleAttributes.downMaxSpeed)
        {
            moveVector.x = 0f;
            moveVector.y = -3F;
            moveVector.z = 0f;
        }
        else
        {
            moveVector.x = 0f;
            moveVector.y = -1F;
            moveVector.z = 0f;
        }
        rigidbody.AddForce(moveVector);
        //Debug.Log("停止跳跃");

    }
    void RoleNormalJumpMove()
    {
        
        moveVector.x = rigidbody.velocity.x;
        moveVector.y = roleAttributes.jumpStartSpeed;
        moveVector.z = 0f;
        rigidbody.velocity = moveVector;
        //Debug.Log("普通跳跃");
    }

    /***快速移动***/
    void RoleFastRightMove()
    {
        if (rigidbody.velocity.x < roleAttributes.speedFast * roleAttributes.moveRate 
            && rigidbody.velocity.x > -1 * roleAttributes.speedFast * roleAttributes.moveRate)//未达到最大速度
        {
            moveVector.x = -1 * roleAttributes.speedFast * roleAttributes.moveRate * roleAttributes.accelerateSpeed;
            moveVector.y = 0f;
            moveVector.z = 0f;
            rigidbody.AddForce(moveVector);
        }
        
    }
    void RoleFastLeftMove()
    {
        if (rigidbody.velocity.x < roleAttributes.speedFast * roleAttributes.moveRate
            && rigidbody.velocity.x > -1 * roleAttributes.speedFast * roleAttributes.moveRate)//未达到最大速度
        {
            moveVector.x = roleAttributes.speedFast * roleAttributes.moveRate * roleAttributes.accelerateSpeed;
            moveVector.y = 0f;
            moveVector.z = 0f;
            rigidbody.AddForce(moveVector);
        }
        
    }
    /**攀爬悬浮**/
    void RoleClimbMove()
    {
        
    }
    /**停止攀爬悬浮**/
    void RoleClimbNoMove()
    {
        rigidbody.gravityScale = roleAttributes.mass;
        rigidbody.freezeRotation = true;
    }

    /***停止纵向移动***/
    void RoleStopMove()
    {
        rigidbody.velocity = new Vector3(0f, rigidbody.velocity.y ,0f);
    }

    /**停止所有移动**/
    void RoleStaticMove()
    {
        rigidbody.gravityScale = 0f;
        rigidbody.freezeRotation = false;
        rigidbody.bodyType = RigidbodyType2D.Kinematic;
        rigidbody.velocity = new Vector3(0f, 0f ,0f);
        //rigidbody.bodyType = RigidbodyType2D.Dynamic;
        //Debug.Log(rigidbody.velocity);
    }
}
