
namespace MultiplayCore
{
    using UnityEngine;

    public class ComponentExtensions 
    {
        //Public methods
        public static T GetComponentNoAlloc<T>(this Component component) where T : class
        {
            return GameObjectExtensions<T>.GetComponentNoAlloc(component.gameObject);
        }
    }
}