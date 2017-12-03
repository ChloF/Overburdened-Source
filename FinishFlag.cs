using UnityEngine;

public class FinishFlag : MonoBehaviour
{
    private SceneLoader loader;

    private void Start()
    {
        loader = GameObject.FindGameObjectWithTag("GameController").GetComponent<SceneLoader>(); //Store a reference to the Scene Loader
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.CompareTag("Player"))
        {
            loader.LoadLevel(loader.CurrentLevel + 1); //Load the next level
        }
    }
}
