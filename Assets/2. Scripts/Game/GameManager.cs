using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject confirmPanel;           // Ȯ�� �г�
    [SerializeField] private GameObject signinPanel;            // �α��� �г�
    [SerializeField] private GameObject signupPanel;            // �г�

    // Main Scene���� ������ ���� Ÿ��
    private Constants.GameType _gameType;

    // Panel�� ���� ���� Canvas ����
    private Canvas _canvas;

    // Game Logic
    private GameLogic _gameLogic;

    // Game ���� UI�� ����ϴ� ��ü
    private GameUIController _gameUIController;

    private void Start()
    {
        var sid = PlayerPrefs.GetString("sid");
        if(string.IsNullOrEmpty(sid))
        {
            OpenSigninPanel();
        }
    }

    /// <summary>
    /// Main���� Game Scene���� ��ȯ�� ȣ��� �޼���
    /// </summary>
    public void ChangeToGameScene(Constants.GameType gameType)
    {
        _gameType = gameType;
        SceneManager.LoadScene("Game");
    }

    /// <summary>
    /// Game���� Main Scene���� ��ȯ�� ȣ��� �޼���
    /// </summary>
    public void ChangeToMainScene()
    {
        SceneManager.LoadScene("Main");
    }

    /// <summary>
    /// Confirm Panel�� ���� �޼���
    /// </summary>
    /// <param name="message"></param>
    public void OpenConfirmPanel(string message,
        ConfirmPanelController.OnConfirmButtonClicked onConfirmButtonClicked)
    {
        if (_canvas != null)
        {
            var confirmPanelObject = Instantiate(confirmPanel, _canvas.transform);
            confirmPanelObject.GetComponent<ConfirmPanelController>()
                .Show(message, onConfirmButtonClicked);
        }
    }

    /// <summary>
    /// �α��� �˾� ǥ��
    /// </summary>
    public void OpenSigninPanel()
    {
        if (_canvas != null)
        {
            var signinPanelObject = Instantiate(signinPanel, _canvas.transform);
            signinPanelObject.GetComponent<SigninPanelController>().Show();
        }
    }
    public void OpenSignupPanel()
    {
        if (_canvas != null)
        {
            var signupPanelObject = Instantiate(signinPanel, _canvas.transform);
            signupPanelObject.GetComponent<SignupPanelController>().Show();
        }
    }
    /// <summary>
    /// Game Scene���� ���� ǥ���ϴ� UI�� �����ϴ� �Լ�
    /// </summary>
    /// <param name="gameTurnPanelType">ǥ���� Turn ����</param>
    public void SetGameTurnPanel(GameUIController.GameTurnPanelType gameTurnPanelType)
    {
        _gameUIController.SetGameTurnPanel(gameTurnPanelType);
    }

    protected override void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        _canvas = FindFirstObjectByType<Canvas>();

        if (scene.name == "Game")
        {
            // Block �ʱ�ȭ
            var blockController = FindFirstObjectByType<BlockController>();
            if (blockController != null)
            {
                blockController.InitBlocks();
            }

            // Game UI Controller �Ҵ� �� �ʱ�ȭ
            _gameUIController = FindFirstObjectByType<GameUIController>();
            if (_gameUIController != null)
            {
                _gameUIController.SetGameTurnPanel(GameUIController.GameTurnPanelType.None);
            }

            // GameLogic ����
            _gameLogic = new GameLogic(blockController, _gameType);
        }
    }
}