using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Audio Sources")]
    public AudioSource musicSource; // Nhạc nền
    public AudioSource soundSource; // Âm thanh hiệu ứng

    private bool isMusicOn;
    private bool isSoundOn;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Giữ lại khi đổi màn
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        isMusicOn = PlayerPrefs.GetInt("Music", 1) == 1;
        isSoundOn = PlayerPrefs.GetInt("Sound", 1) == 1;

        ApplySettings();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ApplySettings(); // Cập nhật lại trạng thái khi đổi màn
    }

    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        PlayerPrefs.SetInt("Music", isMusicOn ? 1 : 0);
        PlayerPrefs.Save();
        ApplySettings();
    }

    public void ToggleSound()
    {
        isSoundOn = !isSoundOn;
        PlayerPrefs.SetInt("Sound", isSoundOn ? 1 : 0);
        PlayerPrefs.Save();
        ApplySettings();
    }

    private void ApplySettings()
    {
        if (musicSource != null)
        {
            musicSource.mute = !isMusicOn;
        }
        if (soundSource != null)
        {
            soundSource.mute = !isSoundOn;
        }
        AudioListener.volume = isSoundOn ? 1f : 0f;
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        if (isSoundOn && soundSource != null)
        {
            soundSource.PlayOneShot(clip);
        }
    }
}
