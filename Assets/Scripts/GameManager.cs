using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TextMeshProUGUI coinCounterTMP;
    public TextMeshProUGUI messageTMP;
    public TextMeshProUGUI gameOverTMP;
    public Door door;
    public GameObject playerPrefab;
    private GameObject playerInstance;

    private int coinsCollected = 0;
    private int coinsRequired = 4;
    private bool hasKey = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            Debug.Log("GameManager instance set: " + gameObject.name);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            Debug.Log("Duplicate GameManager instance destroyed: " + gameObject.name);
        }
    }

    void Start()
    {
        AssignUIElements();
        UpdateCoinCounterUI();
        if (playerInstance == null)
        {
            CreatePlayer();
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AssignUIElements();
        CreatePlayer();
        coinsCollected = 0;
        hasKey = false;
        UpdateCoinCounterUI();
    }

    void AssignUIElements()
    {
        coinCounterTMP = GameObject.Find("CoinCounterTMP")?.GetComponent<TextMeshProUGUI>();
        messageTMP = GameObject.Find("MessageTMP")?.GetComponent<TextMeshProUGUI>();
        gameOverTMP = GameObject.Find("GameOverTMP")?.GetComponent<TextMeshProUGUI>();

        if (door == null)
        {
            door = FindObjectOfType<Door>();
        }

        if (coinCounterTMP == null)
        {
            Debug.LogError("CoinCounterTMP is null!");
        }

        if (messageTMP == null)
        {
            Debug.LogError("MessageTMP is null!");
        }

        if (gameOverTMP == null)
        {
            Debug.LogError("GameOverTMP is null!");
        }
    }

    void CreatePlayer()
    {
        if (playerInstance != null)
        {
            Destroy(playerInstance);
        }

        if (playerPrefab != null)
        {
            playerInstance = Instantiate(playerPrefab);
            Debug.Log("Player instantiated in new scene: " + SceneManager.GetActiveScene().name);
        }
        else
        {
            Debug.LogError("Player prefab is not assigned in GameManager.");
        }
    }

    public void CollectCoin()
    {
        coinsCollected++;
        Debug.Log("Coin collected. Total coins: " + coinsCollected);
        UpdateCoinCounterUI();

        if (coinsCollected >= coinsRequired)
        {
            ActivateDoor();
        }
    }

    public void UpdateCoinCounterUI()
    {
        if (coinCounterTMP != null)
        {
            coinCounterTMP.text = "Coins: " + coinsCollected + "/" + coinsRequired;
            Debug.Log("Coin counter UI updated. Text: " + coinCounterTMP.text);
        }
        else
        {
            Debug.LogError("coinCounterTMP is null!");
        }
    }

    void ActivateDoor()
    {
        if (door != null)
        {
            door.ActivateDoor();
            ShowMessage("Kapý Artýk Açýldý! Diðer Seviyeye Gidebilirsin.");
        }
        else
        {
            Debug.LogError("Door is not assigned in GameManager.");
        }
    }

    void ShowMessage(string message)
    {
        if (messageTMP != null)
        {
            messageTMP.text = message;
            messageTMP.enabled = true;
            Debug.Log("Message displayed: " + message);
        }
        else
        {
            Debug.LogError("messageTMP is null!");
        }
    }

    public void ShowGameOver()
    {
        if (gameOverTMP != null)
        {
            gameOverTMP.enabled = true;
            Debug.Log("Game Over message displayed.");
            Time.timeScale = 0; // Oyunu durdur
        }
        else
        {
            Debug.LogError("gameOverTMP is null!");
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }
}
