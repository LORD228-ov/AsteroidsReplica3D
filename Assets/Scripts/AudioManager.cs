
using UnityEngine;
using UnityEngine.SceneManagement;


public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    [SerializeField] private AudioClip[] tracks;
    void Start()
    {
        //checking scene name

        string currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "StartMenu")
        {
            int randomNumber = Random.Range(3, 5);
            SwitchTrack(randomNumber);
        }
    }
    public void SwitchTrack(int nummer)
    {
        // set a soundtrack
        audioSource.clip = tracks[nummer];
        audioSource.Play();
    }
    
}
