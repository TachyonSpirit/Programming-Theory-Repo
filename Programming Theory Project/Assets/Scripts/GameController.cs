using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Player player;
    public Camera gameCamera;
    public GameObject[] gameBlockPrefabs;
    public Text fuelCounterText;
    public Text scoreText;

    // Below is an example of encapsulation
    // Here we're ensuring that other classes can't pass an incorrect value for the fuel amount
    private int m_fuelCounter; // This is the encapsulated/better-secured variable
    public int fuelCounter // This is the variable accessible to the outside "world"
    {
        get { return m_fuelCounter; } // getter returns backing field
        set {
            if (value < 0)
            {
                m_fuelCounter = 0;
                Debug.LogError("You can't set a negative fuel amount - used 0 instead.");                
            }
            else
            {
                m_fuelCounter = value; // original setter now in if/else statement
            }
        } // setter uses backing field
    }

    private float gameBlockPointer;
    private float safeArea = 111;
    private float score;
    private bool isGameOver; // initially the value will be false

    // Start is called before the first frame update
    void Start()
    {
        fuelCounter = 110;
        score = 0;
        fuelCounterText.text = "Fuel: " + m_fuelCounter + "%";
        scoreText.text = "Score: " + Mathf.FloorToInt(score + player.transform.position.z + 110);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null) // If != null means there's still a player around!
        {
            scoreText.text = "Score: " + Mathf.FloorToInt(score + player.transform.position.z + 110);

            while (gameBlockPointer < player.transform.position.z + safeArea)
            {
                fuelCounter -= 10;
                fuelCounterText.text = "Fuel: " + m_fuelCounter + "%";

                int blockIndex = Random.Range(0,gameBlockPrefabs.Length);

                if (gameBlockPointer <= 0)
                {
                    blockIndex = 0;
                }

                GameObject gameBlockObject = GameObject.Instantiate<GameObject>(gameBlockPrefabs[blockIndex]);
                // We need the spawned object to be under the GameController object!
                gameBlockObject.transform.SetParent(this.transform);

                GameBlockController block = gameBlockObject.GetComponent<GameBlockController>();

                gameBlockObject.transform.position = new Vector3(
                    0,
                    0,
                    gameBlockPointer + block.size / 2
                );
                gameBlockPointer += block.size;
            }

            gameCamera.transform.position = new Vector3(
                gameCamera.transform.position.x,
                gameCamera.transform.position.y,
                player.transform.position.z -50 // To initially position the camera 50 units behind the player.
            );
//            Debug.Log("New camera position for z" + player.transform.position.z);
        } else // so there's no more player
        {
            if (!isGameOver)
            {
                isGameOver = true;
                scoreText.text += "\nPress R to restart!";
            }
        }

        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
