using System.Collections.Generic;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    public SpawnState firstSpawnState; //Start point to reset to
    public SpawnState spawnState; //Checkpoint to respawn to
    public Transform startFlag;

	void Start ()
    {
        SetSpawnPoint(startFlag.position + (Vector3.up * 2), 0); //Set the spawnpoint to just above the starting flag
        firstSpawnState = spawnState; //Assign this as the first spawn
	}

    public void SetSpawnPoint(Vector3 point, int coinCount)
    {
        GameObject[] coins = GameObject.FindGameObjectsWithTag("Coin"); //Search for all of the coins
        spawnState = new SpawnState(coinCount, point, coins);
    }

    //Second override to directly take a spawnstate
    public void SetSpawnPoint(SpawnState ss)
    {
        spawnState = ss; 
    }
}

[System.Serializable]
public class SpawnState
{
    public int coinCount; //The amount of coins the player had
    public GameObject[] coins; //The rest of the coins
    public Vector3[] coinPositions; //Where the other coins were
    public Vector3 pos; //Were to respawn to

    public SpawnState (int _coinCount, Vector3 _pos, GameObject[] _coins)
    {
        coinCount = _coinCount;
        pos = _pos;
        coins = _coins;

        List<Vector3> _coinPositions = new List<Vector3>();

        foreach(GameObject coin in _coins)
        {
            _coinPositions.Add(coin.transform.position); //Add the positions of all of the coins to a list
        }

        coinPositions = _coinPositions.ToArray();
    }
}
