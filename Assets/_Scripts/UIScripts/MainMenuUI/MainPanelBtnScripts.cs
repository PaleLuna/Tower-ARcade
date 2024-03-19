using PaleLuna.Architecture.Services;
using Services;
using UnityEngine;

public class MainPanelBtnScripts : MonoBehaviour
{
    public void LoadCurrentLevel(int sceneIndex)
    {
        SceneLoaderService sceneManager =
            ServiceManager.Instance.GlobalServices.Get<SceneLoaderService>();

        sceneManager.LoadScene(sceneIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
