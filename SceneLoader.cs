using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public int CurrentLevel //Store a readonly reference to the current scene index
    {
        get
        {
            return SceneManager.GetActiveScene().buildIndex;
        }
    }

    public Image screenCover; //Image to fade the screen to black

    private void Awake()
    {
        StartCoroutine(FadeIn()); //Fade in the level
    }

    public void LoadLevel (int x)
    {
        StartCoroutine(LoadScene(x));
    }

    IEnumerator LoadScene (int x)
    {
        screenCover.gameObject.SetActive(true);
        float i = 0;

        while (i <= 1) //Fade out to black
        {
            i+=Time.deltaTime;
            yield return new WaitForEndOfFrame();
            screenCover.color = new Color(screenCover.color.r, screenCover.color.g, screenCover.color.b, i);
        }

        SceneManager.LoadSceneAsync(x);
        yield return null;
    }

    IEnumerator FadeIn ()
    {
        float i = 1;

        while (i >= 0)
        {
            i -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
            screenCover.color = new Color(screenCover.color.r, screenCover.color.g, screenCover.color.b, i);
        }
        screenCover.gameObject.SetActive(false);
        yield return null;
    }
}
