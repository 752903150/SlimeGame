using UnityEngine;
using Cinemachine;

public class MapManager : MonoBehaviour
{
    private CompositeCollider2D boundary;

    private void Awake()
    {
        boundary = transform.Find("Boundary").GetComponent<CompositeCollider2D>();
        GameManager.Instance.Cinemachine.GetComponent<CinemachineConfiner>().m_BoundingShape2D = boundary;
    }
}
