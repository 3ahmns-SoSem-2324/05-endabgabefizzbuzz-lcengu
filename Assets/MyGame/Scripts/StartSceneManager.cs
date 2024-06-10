using UnityEngine;

public class StartSceneManager : MonoBehaviour
{
    public GameObject startSceneUI;
    public GameObject gameUI;
    public KeyCode startGameKey = KeyCode.S; 
    public KeyCode quitGameKey = KeyCode.Escape;

    void Start()
    {
        ShowStartScene();
    }
    void Update()
    {
        if (Input.GetKeyDown(startGameKey))
        {
            StartGame();
        }

        if (Input.GetKeyDown(quitGameKey))
        {
            QuitGame();
        }
    }
    public void StartGame()
    {
        startSceneUI.SetActive(false);
        gameUI.SetActive(true);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    void ShowStartScene()
    {
        startSceneUI.SetActive(true);
        gameUI.SetActive(false);
    }
}