using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnPool : MonoBehaviour
{
    public int columnPoolSize = 6;
    public GameObject columnPrefab;
    public float spawnRate = 4.0f;
    public float columnMin = -1f;
    public float columnMax = 3.5f;

    private GameObject[] columns;
    private Vector2 objectPoolPosition = new Vector2(-15f, -25f);
    private float timeSinceLastSpawned;
    private float spawnXPosition = 10f;
    private int currentColumn = 0;
    public float scrollSpeed = 0f;

	// Use this for initialization
	void Start ()
    {
        columns = new GameObject[columnPoolSize];
        for(int i = 0; i < columnPoolSize; i++)
        {
            columns[i] = (GameObject)Instantiate(columnPrefab, objectPoolPosition, Quaternion.identity);
        }
        scrollSpeed = GameControl.instance.scrollSpeed * -1;
    }
	
	// Update is called once per frame
	void Update ()
    {
        timeSinceLastSpawned += Time.deltaTime;

        if (spawnRate > 1.5f)
        {
            float tmpScrollSpeed = GameControl.instance.scrollSpeed * -1;
            if (this.scrollSpeed < tmpScrollSpeed)
            {
                this.scrollSpeed = tmpScrollSpeed;
                spawnRate -= 0.3f;
            }
        }
        
        if(!GameControl.instance.gameOver && timeSinceLastSpawned >= spawnRate)
        {
            timeSinceLastSpawned = 0;
            float spawnYPosition = Random.Range(columnMin, columnMax);
            columns[currentColumn].transform.position = new Vector2(spawnXPosition, spawnYPosition);
            currentColumn++;
            if(currentColumn >= columnPoolSize)
            {
                currentColumn = 0;
            }
        }
	}
}
