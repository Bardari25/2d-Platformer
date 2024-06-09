using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.instance;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Coin collected by: " + other.gameObject.name);
            if (gameManager != null)
            {
                gameManager.CollectCoin();
            }
            else
            {
                Debug.LogError("GameManager instance is null!");
            }
            Destroy(gameObject);
        }
    }
}
