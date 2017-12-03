using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().GiveCoin(); //Increase the player's coin count by one
            Destroy(this.gameObject);
        }
    }
}
