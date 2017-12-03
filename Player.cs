using System;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float moveSpeed; //Mow fast the player moves
    public float airMoveSpeed; //How fast the player moves in the air
    public float jumpForce; //How fast the player jumps

    public Text coinsText; //Text displaying how many coins the player has
    public GameObject coin; //Coin prefab to instantiate when respawning

    public bool IsGrounded //Readonly property to expose the value of CheckGrounded()
    {
        get
        {
            return CheckGrounded();
        }
    }

    public float groundCheckDistance; //How far to check when looking for the ground

    [Space(10)]

    public float coins; //How many coins the player has
    private Rigidbody2D rb; //A reference to the player's Rigidbody

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.mass = 1 + (coins * 0.25f); //Increase the mass with every coin

        if (CheckGrounded()) //If the player is touching the ground
        {
            rb.drag = 5; //Increase the drag to slow down quicker
            rb.AddForce(new Vector2(Input.GetAxis("Horizontal"), 0).normalized * moveSpeed); //Add force in the input direction

            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(new Vector2(0, jumpForce));
            }
        }
        else
        {
            rb.drag = 1; //Decrease the drag for more "floaty" air movement
            rb.AddForce(new Vector2(Input.GetAxis("Horizontal"), 0).normalized * airMoveSpeed); //Add force in the input direction

            if (transform.position.y < -25) //Stop the player falling into infinity
            {
                Respawn(); 
            }
        }

        coinsText.text = coins.ToString();
    }

    public void GiveCoin ()
    {
        coins++;
        GameObject.FindGameObjectWithTag("SoundEffects").GetComponents<AudioSource>()[0].Play(); //Play the coin pickup sound
    }

    private bool CheckGrounded ()
    {
        //Only check for objects in the environment layer
        int layer = 8;
        int layerMask = (1 << layer); 

        return Physics2D.Raycast(transform.position - (Vector3.up * 0.25f), Vector3.down, groundCheckDistance, layerMask); //Return whether or not the ray hits anything
    }

    public void Respawn ()
    {
        SpawnState spawnState = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpawnPointManager>().spawnState; //Get a reference to the current Respawn State

        coins = spawnState.coinCount; 
        transform.position = spawnState.pos;

        //Go through all the coins that were there when the player hit the checkpoint
        //If they don't exist anymore, instantiate a new one where they were
        foreach(GameObject _coin in spawnState.coins) 
        {
            if (!_coin) 
            {
                Instantiate(coin, spawnState.coinPositions[Array.IndexOf(spawnState.coins, _coin)], Quaternion.identity);
            }
        }

        
    }

    //Same as Respawn() but goes back to green flag
    public void Reset()
    {
        SpawnState spawnState = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpawnPointManager>().firstSpawnState;

        coins = spawnState.coinCount;
        transform.position = spawnState.pos;
        foreach (GameObject _coin in spawnState.coins)
        {
            if (!_coin)
            {
                Instantiate(coin, spawnState.coinPositions[Array.IndexOf(spawnState.coins, _coin)], Quaternion.identity);
            }
        }

        GameObject.FindGameObjectWithTag("GameController").GetComponent<SpawnPointManager>().SetSpawnPoint(spawnState); //Set the respawn point to the green flag
    }
}
