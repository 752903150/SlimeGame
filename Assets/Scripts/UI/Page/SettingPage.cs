using UnityEngine;
using UnityEngine.UI;

public class SettingPage : MonoBehaviour
{
    private Button continueGame;
    private Button returnMain;

    private void Awake()
    {
        continueGame = transform.Find("Continue").GetComponent<Button>();
        returnMain = transform.Find("Return").GetComponent<Button>();
    }

    private void Start()
    {
        continueGame.onClick.AddListener(GameManager.Instance.ContinueGame);
    }
}
