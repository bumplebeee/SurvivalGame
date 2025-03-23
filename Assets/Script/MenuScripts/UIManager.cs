using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image soundButton;
    public Image musicButton;
    public Sprite onSprite;
    public Sprite offSprite;

    void Start()
    {
        UpdateButtons();
    }

    public void ToggleSound()
    {
        AudioManager.instance.ToggleSound();
        UpdateButtons();
    }

    public void ToggleMusic()
    {
        AudioManager.instance.ToggleMusic();
        UpdateButtons();
    }

    private void UpdateButtons()
    {
        if (AudioManager.instance != null)
        {
            bool isMusicOn = PlayerPrefs.GetInt("Music", 1) == 1;
            bool isSoundOn = PlayerPrefs.GetInt("Sound", 1) == 1;

            musicButton.sprite = isMusicOn ? onSprite : offSprite;
            soundButton.sprite = isSoundOn ? onSprite : offSprite;
        }
    }
}
