using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour
{
    public int index;
    public string levelName;

    // OnTriggerEnter2D is called when another collider enters the trigger collider attached to this object
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Use either index or levelName to load the scene
            SceneManager.LoadScene(index);
            // SceneManager.LoadScene(levelName);
        }
    }
}