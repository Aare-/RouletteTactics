using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Utils  {
    public static class GameObjectExtensions {
        public static void RemoveAllChildren(this GameObject obj, Func<GameObject, bool> filter = null) {
            if (obj.transform == null)
                return;

            int childs = obj.transform.childCount;
            for (int i = childs; i > 0; i--) {
                if (filter != null && !filter(obj.transform.GetChild(i - 1).gameObject))
                    continue;
                Object.DestroyImmediate(obj.transform.GetChild(i - 1).gameObject);
            }
        }
    }
}