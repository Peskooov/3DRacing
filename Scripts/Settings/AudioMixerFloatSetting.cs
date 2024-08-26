using UnityEngine;
using UnityEngine.Audio;

namespace Racing
{
    [CreateAssetMenu]
    public class AudioMixerFloatSetting : Setting
    {
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private string nameParametr;

        [SerializeField] private float minRealValue;
        [SerializeField] private float maxRealValue;

        [SerializeField] private float vitrualStep;
        [SerializeField] private float minVirtialValue;
        [SerializeField] private float maxVirtialValue;

        private float currentValue = 0;

        public override bool isMinValue { get => currentValue == minRealValue; }
        public override bool isMaxValue { get => currentValue == maxRealValue; }

        private void AddValue(float value)
        {
            currentValue += value;
            currentValue = Mathf.Clamp(currentValue, minRealValue, maxRealValue);
        }

        public override void SetNextValue()
        {
            AddValue(Mathf.Abs(maxRealValue - minRealValue) / vitrualStep);
        }
        public override void SetPreviousValue()
        {
            AddValue(-Mathf.Abs(maxRealValue - minRealValue) / vitrualStep);
        }

        public override object GetValue()
        {
            return currentValue;
        }

        public override string GetStringValue()
        {
            return Mathf.Lerp(minVirtialValue, maxVirtialValue, (currentValue - minRealValue) / (maxRealValue - minRealValue)).ToString();
        }

        public override void Apply()
        {
            audioMixer.SetFloat(nameParametr, currentValue);

            Save();
        }

        public override void Load()
        {
            currentValue = PlayerPrefs.GetFloat(title, 0);
        }

        private void Save()
        {
            PlayerPrefs.SetFloat(title, currentValue);
        }
    }
}