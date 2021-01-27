using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    
    //eventos
    public delegate void NotifyInitLevel();
    public delegate void NotifyLevelStarted();
    public delegate void NotifyLevelCleared();
    
    public event NotifyInitLevel InitLevel;
    public event NotifyLevelStarted LevelStarted;
    public event NotifyLevelCleared LevelCleared;
    
    public Transform rootLevel;
    public List<Transform> bricks;

    [Header("la Bola")] public GameObject bolaPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        if (rootLevel == null) return;
        var amount = rootLevel.childCount;

        if (amount < 1) return;
        for (int i = 0; i < amount; i++)
        {
            bricks.Add(rootLevel.GetChild(i));
        }
        InitLevel?.Invoke();
        
        //por ejemplo, que empiece la partida despues de 2 secs, una vez se ha mostrado el cutscene, etc...
        StartCoroutine(StartCurrentLevelAfterSecs(2));
    }

    //puede ser llamado desde el script del brick, el LevelManager, o el GameManager...
    //NOTA: CUANDO SOPORTEMOS BOSSES, HAY QUE MODIFICAR ESTA FUNCION
    public void RemoveBrick(Transform briki)
    {
        if (briki == null) return;
        bricks.RemoveAt(bricks.IndexOf(briki));

        if (bricks.Count == 0)
        {
            LevelCleared?.Invoke();
            //aqui falta recoger todos los puntos y stats del nivel conseguidos, acumula al Player.
        }
    }

    IEnumerator StartCurrentLevelAfterSecs(int secs)
    {
        yield return new WaitForSeconds(secs);
        LevelStarted?.Invoke();
        
        //instancio la bola en el nivel
        var mainBola = Instantiate(bolaPrefab);
    }


}
