using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Racing
{
    public class UIRaceButton : UISelectableButton, IScriptableObjectProperty
    {
        [SerializeField] private RaceInfo raceInfo;
        [SerializeField] private Image icon;
        [SerializeField] private Text title;

        private void Start()
        {
            ApplyProperty(raceInfo);
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);

            if (raceInfo == null) return;

            SceneManager.LoadScene(raceInfo.SceneName);
        }

        public void ApplyProperty(ScriptableObject property)
        {
            if (property == null) return;
            if( property is RaceInfo == false) return;

            raceInfo = property as RaceInfo;

            icon.sprite = raceInfo.Icon;
            title.text = raceInfo.Title;
        }
    }
}