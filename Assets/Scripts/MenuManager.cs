using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    int currentSceneIndex;
    public GameObject optionsCanvas;
    public GameObject loadingBar;
    public Image bar;

    List<AsyncOperation> scenesToLoad = new List<AsyncOperation>();
    public void StartGame()
    {
        //HideOptions();
        //ShowLoadingScreen();
        scenesToLoad.Add(SceneManager.LoadSceneAsync("Sala108"));
        scenesToLoad.Add(SceneManager.LoadSceneAsync("Korytarz", LoadSceneMode.Additive));
        scenesToLoad.Add(SceneManager.LoadSceneAsync("Sala106", LoadSceneMode.Additive));
        //StartCoroutine(LoadingScreen());
    }

    IEnumerator LoadingScreen()
    {
        float totalProgress = 0;
        for (int i = 0; i < scenesToLoad.Count; i++)
        {
            while (!scenesToLoad[i].isDone)
            {
                totalProgress += scenesToLoad[i].progress;
                bar.fillAmount = totalProgress / scenesToLoad.Count;
                yield return null;
            }
        }




    }

    public void LoadNextScene()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    public void RestartScene()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void doExitGame()
    {
        Application.Quit();
    }

    public void HideOptions()
    {
        optionsCanvas.SetActive(false);
    }

    public void ShowLoadingScreen()
    {
        loadingBar.SetActive(true);
    }
}
