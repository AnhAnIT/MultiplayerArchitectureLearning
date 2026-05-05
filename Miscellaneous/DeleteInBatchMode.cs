namespace MultiplayCore
{
    using UnityEngine;

    [DefaultExecutionOrder(-9999)]
    public class DeleteInBatchMode : MonoBehaviour
    {
        private void Awake()
        {
            if (Application.isBatchMode == true)
            {
                GameObject.DestroyImmediate(gameObject);
            }
        }
    }
}
