using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;

public class LoadingScreenController : MonoBehaviour
{
    
    [Header("UI Elements")]
    [SerializeField] private Slider progressBar;

    
    private void Start()
    {
        StartCoroutine(LoadTargetSceneAsync());
    }
    
    private IEnumerator LoadTargetSceneAsync()
    {
        string sceneName = SceneManager_.SceneToLoad;

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.value = progress;

            yield return null;
        }
    }

    
}




