using UCExtension.Audio;
using UnityEngine;
using UnityEngine.UI;

public class AudioSettingSlider : MonoBehaviour
{
    [SerializeField] AudioMixerGroupType mixerType;

    [SerializeField] Slider slider;

    private void Start()
    {
        slider.onValueChanged.AddListener(OnSliderValueChange);
        slider.value = UCAudioSettings.GetVolumn(mixerType);
    }

    void OnSliderValueChange(float value)
    {
        UCAudioSettings.SetVolumn(mixerType, value);
    }
}