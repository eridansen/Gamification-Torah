using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance { get; private set; }

    private readonly List<ArrowScript> arrows = new List<ArrowScript>();

    [SerializeField] ArrowSpawner arrowSpawner;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;                                                                        // Alles nur provisorisch, sodass nichts schiefgeht
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        arrowSpawner.createArrow(1f, 2f, 45f, 1);
        arrowSpawner.createArrow(-1f, -1f, 25f, 2);
    }


    public void RegisterArrow(ArrowScript arrow)
    {
        if (arrow == null) return;

        if (!arrows.Contains(arrow))                                                       // Der Arrow wird aufgenommen in meiner Liste
            arrows.Add(arrow);
    }

    public void UnregisterArrow(ArrowScript arrow)
    {
        if (arrow == null) return;                                                         // Löscht den arrow aus der liste upon destruction
        arrows.Remove(arrow);
    }

    public int GetArrowID(ArrowScript arrow)
    {
        return arrow.gameObject.GetInstanceID();                                           // Das gibt mir die ID des arrows (um sie zu unterscheiden)
    }

    public void ChangeScene(int sc)
    {
        foreach (ArrowScript arrow in arrows)
        {
            if (arrow != null)                                                             // Zerstört alle Arrows
                Destroy(arrow.gameObject);
        }

        arrows.Clear();                                                                    // Löscht alle aus liste

        UnityEngine.SceneManagement.SceneManager.LoadScene(sc);                            // Lädt dann theoretisch die scene (kommt noch)
        Debug.Log("Scene die du abrufst: " + sc);
    }

}
