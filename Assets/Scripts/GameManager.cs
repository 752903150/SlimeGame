using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : MonoSingleton<GameManager>
{
    private GameObject mainCamera;
    private GameObject cinemachine;

    private GameObject canvas;
    private GameObject eventSystem;
    private GameObject settingPage;
    private GameObject hpBar;

    private GameObject player;

    public GameObject Cinemachine => cinemachine;

    private new void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && settingPage!=null) PauseGame();
    }

    public void StartGame()
    {
        //同步加载
        SceneManager.LoadScene("Level_00");
        InitGame();
    }

    public void ExitGame()
    {
        //开发者模式
        UnityEditor.EditorApplication.isPlaying = false;
        //打包之后
        //Application.Quit();
    }

    private void InitGame()
    {
        InitCamera();
        InitUI();
        InitPlayer();
    }

    private void InitCamera()
    {
        mainCamera = Instantiate(Resources.Load("Camera/MainCamera") as GameObject);
        cinemachine = Instantiate(Resources.Load("Camera/Cinemachine") as GameObject);
        mainCamera.name = "MainCamera";
        cinemachine.name = "Cinemachine";
        DontDestroyOnLoad(mainCamera);
        DontDestroyOnLoad(cinemachine);
    }

    private void InitUI()
    {
        InitCanvas();
        InitSettingPage();
        InitPlayerPanel();
    }

    private void InitCanvas()
    {
        canvas = Instantiate(Resources.Load("UI/Canvas") as GameObject);
        canvas.name = "Canvas";
        eventSystem = GameObject.Find("EventSystem");
        DontDestroyOnLoad(canvas);
        DontDestroyOnLoad(eventSystem);
    }

    private void InitSettingPage()
    {
        settingPage = Instantiate(Resources.Load("UI/SettingPage") as GameObject,canvas.transform);
        settingPage.name = "SettingPage";
        settingPage.SetActive(false);
    }

    private void InitPlayerPanel()
    {
        hpBar = Instantiate(Resources.Load("UI/HpBar") as GameObject, canvas.transform);
        hpBar.name = "HpBar";
    }

    private void InitPlayer()
    {
        player = Instantiate(Resources.Load("Player/Slime") as GameObject);
        player.name = "Slime";
        player.GetComponent<SlimeRole>().BindHpBar(hpBar.GetComponent<HpBar>());
        cinemachine.GetComponent<CinemachineVirtualCamera>().Follow = player.transform;
        DontDestroyOnLoad(player);
    }

    private bool isPausing = false;
    public void PauseGame()
    {
        if (isPausing) return;

        settingPage.SetActive(true);
        Time.timeScale = 0;
        isPausing = true;
    }

    public void ContinueGame()
    {
        if (!isPausing) return;

        settingPage.SetActive(false);
        Time.timeScale = 1;
        isPausing = false;
    }
}
