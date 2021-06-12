using UnityEngine;

public class SavePoint : MonoBehaviour
{
    private float CD = 5f;
    private float lastSaveTime = -5;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player") return;
        if (Time.time - lastSaveTime < CD) return;
        Debug.Log("保存!");
        animator.Play("Save");
        lastSaveTime = Time.time;
    }
}
