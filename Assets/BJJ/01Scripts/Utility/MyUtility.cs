using UnityEngine;

public static class MyUtility
{
    public static Transform GetChildrenTrans(Transform root, string targetName, bool logIfNotFound = true)
    {
        foreach(Transform child in root)
        {
            if(child.name == targetName) return child;

            var result = GetChildrenTrans(child, targetName, false);
            if(result != null) return result;
        }

        if(logIfNotFound)
            Debug.Log($"MyUtility.cs - GetChildrenTrans {targetName} is Not Children {root.name}");
        return null;
    }
}
