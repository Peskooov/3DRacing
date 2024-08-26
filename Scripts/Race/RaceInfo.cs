using UnityEngine;

namespace Racing
{
    [CreateAssetMenu]
    public class RaceInfo : ScriptableObject
    {
        [SerializeField] private string sceneName;
        [SerializeField] private Sprite icon;
        [SerializeField] private string title;

        public string SceneName => sceneName;
        public Sprite Icon => icon;
        public string Title => title;
    }
}