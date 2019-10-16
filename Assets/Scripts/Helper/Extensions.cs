﻿using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public static class Extensions {

    private enum Unit {
        k, m, M, b, B
    }

    public static string ToStringFormatted(this long number) {

        if (number < 1000)
            return number.ToString();

        return FormatNumber(number, 0);

        string FormatNumber(float n, int unitIndex) {

            float x = (Mathf.CeilToInt(n / 100f)) / 10f;

            if (x >= 100)
                x = (long)x;

            if (x >= 1000)
                return FormatNumber(x, ++unitIndex);

            return x.ToString() + Enum.GetName(typeof(Unit), unitIndex);
        }
    }

    public static float Map(this float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    public static T FindChild<T>(this Transform parent, string path) {
        string[] childrenNames = path.Split('/');
        int childCount = childrenNames.Length;

        Transform objToReturn = parent;
        for (int i = 0; i < childCount; i++) {
            try {
                objToReturn = objToReturn.Find(childrenNames[i]);
            } catch {
                Debug.LogError("Can't find child at path: " + path);
            }
        }

        return objToReturn.GetComponent<T>();
    }

    public static T Random<T>(this IEnumerable<T> enumerable) {
        if (enumerable == null)
            throw new ArgumentNullException(nameof(enumerable));

        var r = new System.Random();
        var list = enumerable as IList<T> ?? enumerable.ToList();
        return list.Count == 0 ? default : list[r.Next(0, list.Count)];
    }
}