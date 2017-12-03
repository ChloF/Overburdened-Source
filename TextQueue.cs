using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextQueue : MonoBehaviour
{
    public Text[] texts;
    public SceneLoader sceneLoader;

	void Start ()
    {
        texts = transform.GetComponentsInChildren<Text>(); //Store a reference to all the texts to show

        StartCoroutine(ShowTexts());
    }

    IEnumerator ShowTexts ()
    {
        for (int i = 0; i < texts.Length; i++)
        {
            StartCoroutine(FadeInText(i)); //Fade in a text
            yield return new WaitForSeconds(2.5f); //Wait for the time it takes to fade in + 2 seconds to read
            StartCoroutine(FadeOutText(i)); //Fade it out
            yield return new WaitForSeconds(0.5f); //Wait for it to finish fading
        }
        sceneLoader.LoadLevel(1); //Load into the menu scene
        yield return null;
    }

	IEnumerator FadeInText (int textIndex)
    {
        Text text = texts[textIndex];

        while(text.color.a < 1) 
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime * 2)); //Increase the alpha a little so that the time it takes is 0.5 seconds
            yield return new WaitForEndOfFrame(); //Wait for the next frame to increase it some more
        }
        yield return null;
    }

    IEnumerator FadeOutText(int textIndex)
    {
        Text text = texts[textIndex];

        while (text.color.a > 0) 
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime * 2)); //Decrease the alpha a little so that the time it takes is 0.5 seconds
            yield return new WaitForEndOfFrame(); //Wait for the next frame to decrease it some more
        }
        yield return null;
    }
}
