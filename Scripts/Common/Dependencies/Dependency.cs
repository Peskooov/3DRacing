using UnityEngine;

namespace Racing
{
    public abstract class Dependency : MonoBehaviour
    {
        protected virtual void BindAll(MonoBehaviour monoBehaviourInScene) { }

        protected void FindAllObjectToBind()
        {
            MonoBehaviour[] allMonoBehaviourInScene = FindObjectsOfType<MonoBehaviour>();

            for (int i = 0; i < allMonoBehaviourInScene.Length; i++)
            {
                BindAll(allMonoBehaviourInScene[i]);
            }
        }

        protected void Bind<T>(MonoBehaviour bindObject, MonoBehaviour target) where T : class
        {
            if (target is IDependency<T>) (target as IDependency<T>).Construct(bindObject as T);
        }
    }
}