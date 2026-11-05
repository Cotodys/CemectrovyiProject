using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager_ : MonoBehaviour
{
    public static string SceneToLoad;

    public void SwitchScene(string targetScene)
    {
        SceneToLoad = targetScene;
        SceneManager.LoadScene("LoadingScene");
    }
    
}
