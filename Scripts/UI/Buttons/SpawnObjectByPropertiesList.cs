using UnityEngine;

namespace Racing
{
    public class SpawnObjectByPropertiesList : MonoBehaviour
    {
        [SerializeField] private Transform parent;
        [SerializeField] private GameObject prefab;
        [SerializeField] private ScriptableObject[] properties;

        [ContextMenu(nameof(SpawnInEditMode))]
        public void SpawnInEditMode()
        {
            if (Application.isPlaying) return;

            GameObject[] allObjects = new GameObject[parent.childCount];

            for (int i = 0; i < parent.childCount; i++)
            {
                allObjects[i] = parent.GetChild(i).gameObject;
            }

            for (int i = 0; i < allObjects.Length; i++)
            {
                DestroyImmediate(allObjects[i]);
            }

            for (int i = 0; i < properties.Length; i++)
            {
                GameObject obj = Instantiate(prefab, parent);
                IScriptableObjectProperty scriptableObjectProperty = obj.GetComponent<IScriptableObjectProperty>();
                scriptableObjectProperty.ApplyProperty(properties[i]);
            }
        }
    }
}