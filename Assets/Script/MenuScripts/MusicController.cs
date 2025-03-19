using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    public Sprite soundOnSprite;  // Ảnh khi bật âm thanh
    public Sprite soundOffSprite; // Ảnh khi tắt âm thanh
    private Image buttonImage;
    [SerializeField]
    private bool isSoundOn = true; // Trạng thái mặc định
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buttonImage = GetComponent<Image>(); // Lấy component Image của Button
        UpdateButtonImage();
    }

    public void ToggleSound()
    {
        isSoundOn = !isSoundOn; // Đảo trạng thái âm thanh
        UpdateButtonImage();

        // Xử lý bật/tắt âm thanh
        if (isSoundOn)
        {
            // Bật âm thanh
           // AudioListener.volume = 1f;
        }
        else
        {
            // Tắt âm thanh
           // AudioListener.volume = 0f;
        }
    }

    private void UpdateButtonImage()
    {
        buttonImage.sprite = isSoundOn ? soundOnSprite : soundOffSprite;
    }
}
