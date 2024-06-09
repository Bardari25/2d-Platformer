using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void GoToMap1()
    {
        SceneManager.LoadScene("Map1");
    }
}
