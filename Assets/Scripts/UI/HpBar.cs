using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    private int maxHp = 1;
    public int MaxHp
    {
        get => maxHp;
        set
        {
            if (value < 0) value = 0;
            maxHp = value;
        }
    }
    private int hp = 1;
    public int Hp
    {
        get => hp;
        set
        {
            if (value < 0) value = 0;
            if (value > maxHp) value = maxHp;
            hp = value;
        }
    }

    [SerializeField] private Sprite full;
    [SerializeField] private Sprite empty;
    private List<Image> hearts = new List<Image>();
    private Image heart;

    private void Awake()
    {
        heart = transform.Find("HeartPrefab").GetComponent<Image>();
    }

    public void Init(int maxHp)
    {
        MaxHp = maxHp;
        hp = MaxHp;
        heart.gameObject.SetActive(false);
        for (int i = 0; i < MaxHp; ++i)
        {
            Image image = Instantiate(heart, transform);
            image.name = "Heart" + i;
            image.gameObject.SetActive(true);
            hearts.Add(image);
        }
        Refresh();
    }

    public void OnHpChanged(int change)
    {
        Hp += change;
        Refresh();
    }

    private void Refresh()
    {
        for (int i = 0; i < MaxHp; ++i)
        {
            if (i >= Hp)
                hearts[i].sprite = empty;
            else
                hearts[i].sprite = full;
        }
    }
}
