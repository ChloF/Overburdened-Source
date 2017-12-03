using UnityEngine;

public class CheckpointFlag : MonoBehaviour
{
    SpawnPointManager spm; 

    private void Start()
    {
        spm = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpawnPointManager>(); //Search for a reference to the Spawn Point Manager
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Player")) //If the player hits the flag
        {
            spm.SetSpawnPoint(transform.position + (Vector3.up * 2), (int)collision.transform.GetComponent<Player>().coins); //Set the spawnpoint to just above this flag
        }
    }
}
