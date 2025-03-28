using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public string gameSceneName = "GameScene"; // Tên scene chính của game
    public AudioSource backgroundMusic; // Nhạc nền
    public Button startButton, muteButton; // Các nút UI
    public Slider volumeSlider; // Thanh trượt âm lượng

    private bool isMuted = false;

    void Start()
    {
        startButton.onClick.AddListener(StartGame);
        muteButton.onClick.AddListener(ToggleMute);

        // Gán sự kiện thay đổi âm lượng khi kéo thanh trượt
        volumeSlider.onValueChanged.AddListener(ChangeVolume);

        // Đặt giá trị mặc định của slider
        volumeSlider.value = backgroundMusic.volume;
    }

    void StartGame()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    void ToggleMute()
    {
        isMuted = !isMuted;
        backgroundMusic.mute = isMuted;
    }

    void ChangeVolume(float value)
    {
        backgroundMusic.volume = value; // Cập nhật âm lượng theo slider
    }
}
