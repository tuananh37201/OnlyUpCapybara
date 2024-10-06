using Sirenix.OdinInspector;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObject/CheckpointData")]
public class CheckpointData : ScriptableObject
{
    [FoldoutGroup(BaseDataC.InforsGroup)]
    [HorizontalGroup(BaseDataC.IDGroup)]
    public string ID;

    [FoldoutGroup(BaseDataC.InforsGroup)]
    [HorizontalGroup(BaseDataC.NameGroup)]
    public string Name;

    [FoldoutGroup(BaseDataC.InforsGroup)]
    [HorizontalGroup(BaseDataC.DescriptionGroup)]
    public string Description;

    [FoldoutGroup(BaseDataC.InforsGroup)]
    public Sprite Avatar;

    [FoldoutGroup(BaseDataC.InforsGroup)]
    public int SortPriority;

    public bool HasSameID(BaseData data)
    {
        return ID.Equals(data.ID);
    }
    public bool HasSameID(string compareID)
    {
        return ID.Equals(compareID);
    }

#if UNITY_EDITOR
    [Button("Set Default")]
    [FoldoutGroup(BaseDataC.InforsGroup)]
    [HorizontalGroup(BaseDataC.IDGroup, 100)]
    public void SetID()
    {
        ID = Regex.Replace(name, @"[^0-9a-zA-Z]+", "_").Replace(" ", "_").ToLower();
        EditorUtility.SetDirty(this);
    }

    [Button("Set Default")]
    [FoldoutGroup(BaseDataC.InforsGroup)]
    [HorizontalGroup(BaseDataC.NameGroup, 100)]
    public void SetName()
    {
        Name = name;
        EditorUtility.SetDirty(this);
    }

    [Button("Set Default")]
    [FoldoutGroup(BaseDataC.InforsGroup)]
    [HorizontalGroup(BaseDataC.DescriptionGroup, 100)]
    public void SetDescriptions()
    {
        Description = "Desc: " + name;
        EditorUtility.SetDirty(this);
    }

    [Button("Set Default Infors")]
    [FoldoutGroup(BaseDataC.InforsGroup)]
    public void SetInfors()
    {
        SetName();
        SetID();
        SetDescriptions();
    }
#endif
}

public static class BaseDataC
{
    public const string NameGroup = InforsGroup + "/Name";

    public const string IDGroup = InforsGroup + "/ID";

    public const string DescriptionGroup = InforsGroup + "/Description";

    public const string InforsGroup = "Infors";
}