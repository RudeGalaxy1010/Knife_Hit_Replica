using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void ReloadCurrentScene(float time = 0)
    {
        var sceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(ReloadScene(sceneIndex, time));
    }

    private IEnumerator ReloadScene(int sceneIndex, float time)
    {
        yield return new WaitForSeconds(time);
        LoadScene(sceneIndex);
    }
}
