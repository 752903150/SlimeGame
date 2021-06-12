using UnityEngine;

public class CharacterAttributes
{
    public int maxHp = 5;//最大HP
    public int health = 5;//生命值
    public float speed = 2f;//移动速度
    public int id = 0;//编号
    public Color color = Color.Fire;
    public CharacterAttributes(int hp = 5,int health = 5,float speed = 2f,int id = 0)
    {
        this.maxHp = hp;
        this.health = health;
        this.speed = speed;
        this.id = id;
    }
}
