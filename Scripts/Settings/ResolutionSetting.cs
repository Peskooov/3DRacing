using UnityEngine;
using UnityEngine.Audio;

namespace Racing
{
    [CreateAssetMenu]
    public class ResolutionSetting : Setting
    {
        [SerializeField]
        private Vector2Int[] avalibaleResolutions = new Vector2Int[]
        {
            new Vector2Int(800, 600),
            new Vector2Int(1280, 720),
            new Vector2Int(1600, 900),
            new Vector2Int(1920, 1080),
        };

        private int currentResolutionIndex;

        public override bool isMinValue { get => currentResolutionIndex == 0; }
        public override bool isMaxValue { get => currentResolutionIndex == avalibaleResolutions.Length - 1; }

        public override void SetNextValue()
        {
            if (isMaxValue == false)
                currentResolutionIndex++;
        }
        public override void SetPreviousValue()
        {
            if (isMinValue == false)
                currentResolutionIndex--;
        }

        public override object GetValue()
        {
            return avalibaleResolutions[currentResolutionIndex];
        }

        public override string GetStringValue()
        {
            return avalibaleResolutions[currentResolutionIndex].x + "x" + avalibaleResolutions[currentResolutionIndex].y;
        }

        public override void Apply()
        {
            Screen.SetResolution(avalibaleResolutions[currentResolutionIndex].x, avalibaleResolutions[currentResolutionIndex].y, true);

            Save();
        }

        public override void Load()
        {
            currentResolutionIndex = PlayerPrefs.GetInt(title, avalibaleResolutions.Length - 1);
        }

        private void Save()
        {
            PlayerPrefs.SetInt(title, currentResolutionIndex);
        }
    }
}