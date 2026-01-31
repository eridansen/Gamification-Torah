using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Scene Data")]
public class SceneData : ScriptableObject
{
    public int sceneId;
    public string displayName;

    [Header("Arrows")]
    public List<ArrowSpawnData> arrows = new List<ArrowSpawnData>();

    [Header("Placeholders (later)")]
    public List<ChestSpawnData> chests = new List<ChestSpawnData>();
    public List<DoorSpawnData> doors = new List<DoorSpawnData>();
}

[Serializable]
public class ArrowSpawnData
{
    public Vector2 position;
    public float rotationDegrees;
    public int sceneToCall;
}


[Serializable]
public class ChestSpawnData
{                                                                          //Placeholders, also ignorieren für den moment
    public Vector2 position;
    public int chestTypeId;
}

[Serializable]
public class DoorSpawnData
{
    public Vector2 position;
    public int doorTypeId;
    public bool locked;
}
