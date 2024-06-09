using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private bool doorIsActive = false;
    private SpriteRenderer spriteRenderer;

    public string nextLevelName; // Ge�i� yap�lacak seviye ad�

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false; // Kap�n�n ba�lang��ta g�r�nmez olmas�n� sa�la
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (doorIsActive && other.CompareTag("Player"))
        {
            Debug.Log("Player entered the door.");
            SceneManager.LoadScene(nextLevelName); // Belirtilen seviyeyi y�kle
        }
    }

    public void ActivateDoor()
    {
        doorIsActive = true;
        spriteRenderer.enabled = true; // Kap�y� g�r�n�r hale getir
        Debug.Log("Door activated and is now visible.");
    }
}
