using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public int activeSong;
    public int inactiveSong;

    private AudioSource[] songs;

    private void Awake()
    {
        songs = GetComponents<AudioSource>();   
    }

    private void Update()
    {
        activeSong = (SceneManager.GetActiveScene().buildIndex > 2) ? 0 : 1; //If we are in the main game, the active song is 0, otherwise it is 1
        inactiveSong = (SceneManager.GetActiveScene().buildIndex > 1) ? 1 : 0; //The inactive song is the opposite

        if (!songs[activeSong].isPlaying)
        {
            songs[activeSong].Play(); //Enable the active song
            songs[inactiveSong].Stop(); //Disable the inactive song
        }
        
    }
}
