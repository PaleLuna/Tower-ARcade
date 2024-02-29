using PaleLuna.Architecture.Services;
using Services;
using UnityEngine;

public class MainPanelBtnScripts : MonoBehaviour
{
    public void LoadCurrentLevel()
    {
        SceneLoaderService sceneManager =
            ServiceManager.Instance.GlobalServices.Get<SceneLoaderService>();

        sceneManager.LoadScene(2);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
