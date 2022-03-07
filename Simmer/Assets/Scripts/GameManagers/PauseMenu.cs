using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Simmer.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    public static bool isPaused;

    [SerializeField] SceneData mainMenu;

    private SceneLoader _sceneLoader;
    // Start is called before the first frame update

    public void Construct(SceneLoader sceneLoader){
      _sceneLoader = sceneLoader;
      pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
      if(Input.GetKeyDown(KeyCode.Escape))
      {
        if(isPaused)
        {
          ResumeGame();
        }
        else
        {
          PauseGame();
        }
      }
    }

    public void PauseGame()
    {
      pauseMenu.SetActive(true);
      Time.timeScale = 0f;
      isPaused = true;
    }

    public void ResumeGame()
    {
      pauseMenu.SetActive(false);
      Time.timeScale = 1f;
      isPaused = false;
    }

    public void QuitGame()
    {
      _sceneLoader.OnSceneLoad.Invoke(mainMenu);
      ResumeGame();
      //Application.Quit();
    }

}
