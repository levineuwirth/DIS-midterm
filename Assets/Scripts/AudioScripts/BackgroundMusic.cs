using UnityEngine;
using UnityEngine.Audio;

public class BackgroundMusic : MonoBehaviour
{

    public static BackgroundMusic instance;
    private AudioResource backgroundMusic;
    private void Awake() {
        if(instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
}
