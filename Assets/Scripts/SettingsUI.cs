using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SettingsUI : MonoBehaviour
{

    AudioSource audioSource;

    GameObject blueBG;
    GameObject purpleBG;
    GameObject greenBG;
    GameObject blueberryBG;
    GameObject spaceBG;
    
    public TMP_Dropdown BG_Dropdown;

    public Toggle musicToggle;
    public Slider musicSlider;


    void Start()
    {
        audioSource = GameManager.Instance.MusicObject.GetComponent<AudioSource>();

        blueBG = GameManager.Instance.BlueBG;
        purpleBG = GameManager.Instance.purpleBG;
        greenBG = GameManager.Instance.GreenBG;
        blueberryBG = GameManager.Instance.BlueberryBG;
        spaceBG = GameManager.Instance.SpaceBG;

        BG_Dropdown.onValueChanged.AddListener(delegate { HandleBgDropdown(BG_Dropdown.value); });
        BG_Dropdown.value = GameManager.Instance.currentBgNo;

        musicToggle.isOn = GameManager.Instance.musicState;
    }
    /// <summary>
    /// Allows you to turn the music on and off.
    /// </summary>
    /// <param name="enabled">Music hides status.</param>
    public void musicOnOff(bool enabled)
    {
        GameManager.Instance.musicState = enabled;
        audioSource.enabled = enabled;
    }
    public void OnValueChanged()
    {
        audioSource.volume = musicSlider.value;
        if (audioSource.volume == 0)
            musicToggle.isOn = false;
        else
            musicToggle.isOn = true;

    }
    /// <summary>
    /// Allows to change the background.
    /// </summary>
    /// <param name="bgNo">Background number.</param>
    public void ChangeBG(int bgNo)
    {
        blueBG.SetActive(bgNo == 0);
        purpleBG.SetActive(bgNo == 1);
        greenBG.SetActive(bgNo == 2);
        blueberryBG.SetActive(bgNo == 3);
        spaceBG.SetActive(bgNo == 4);

        GameManager.Instance.currentBgNo = bgNo;
    }
    public void HandleBgDropdown(int val)
    {
        ChangeBG(val);
    }

}
