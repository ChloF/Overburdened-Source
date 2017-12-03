using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.transform.CompareTag("Player"))
        {
            collision.collider.GetComponent<Player>().Respawn(); //Trigger the respawn event on the player
        }
    }
}
