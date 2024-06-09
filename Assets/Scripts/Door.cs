using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private bool doorIsActive = false;
    private SpriteRenderer spriteRenderer;

    public string nextLevelName; // Geçiþ yapýlacak seviye adý

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false; // Kapýnýn baþlangýçta görünmez olmasýný saðla
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (doorIsActive && other.CompareTag("Player"))
        {
            Debug.Log("Player entered the door.");
            SceneManager.LoadScene(nextLevelName); // Belirtilen seviyeyi yükle
        }
    }

    public void ActivateDoor()
    {
        doorIsActive = true;
        spriteRenderer.enabled = true; // Kapýyý görünür hale getir
        Debug.Log("Door activated and is now visible.");
    }
}
