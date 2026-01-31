using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance { get; private set; }

    [Header("Spawners")]
    [SerializeField] private ArrowSpawner arrowSpawner;

    [Header("Scene Data Library")]
    [SerializeField] private List<SceneData> scenes = new List<SceneData>();

    private readonly List<ArrowScript> arrows = new List<ArrowScript>();
    private int currentSceneId = -1;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        ChangeScene(0);                                                                          //Ich starte einfach mal in scene 0
    }

    public void RegisterArrow(ArrowScript arrow)
    {
        if (arrow != null && !arrows.Contains(arrow))
            arrows.Add(arrow);
    }

    public void UnregisterArrow(ArrowScript arrow)
    {
        if (arrow != null)
            arrows.Remove(arrow);
    }

    public void ChangeScene(int sc)
    {
        
        for (int i = 0; i < arrows.Count; i++)
        {                                                                                        //Clear arrows
            if (arrows[i] != null)
                Destroy(arrows[i].gameObject);
        }
        arrows.Clear();

        
        SceneData data = GetSceneData(sc);                                                      //get data
        if (data == null)
        {
            Debug.LogError($"No SceneData found for sceneId={sc}. Create one and add it to SceneManager.scenes.");
            return;
        }

        currentSceneId = sc;

        
        if (arrowSpawner == null)
        {
            Debug.LogError("ArrowSpawner not assigned in SceneManager.");
            return;
        }

        foreach (var a in data.arrows)                                                        //spawn the arrows
        {
            arrowSpawner.createArrow(a.position.x, a.position.y, a.rotationDegrees, a.sceneToCall);
        }

        
        // SpawnChests(data.chests);
        // SpawnDoors(data.doors);

        Debug.Log($"Loaded scene data: {data.displayName} (id={data.sceneId})");
    }

    private SceneData GetSceneData(int id)
    {
        for (int i = 0; i < scenes.Count; i++)
        {
            if (scenes[i] != null && scenes[i].sceneId == id)
                return scenes[i];
        }
        return null;
    }
}
