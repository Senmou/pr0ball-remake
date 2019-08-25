using UnityEngine;

public static class Extensions {

    public static float Map(this float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public static T FindChild<T>(this Transform parent, string path) {
        string[] childrenNames = path.Split('/');
        int childCount = childrenNames.Length;

        Transform objToReturn = parent;
        for (int i = 0; i < childCount; i++) {
            objToReturn = objToReturn.transform.Find(childrenNames[i]);
        }

        return objToReturn.GetComponent<T>();
    }
}
