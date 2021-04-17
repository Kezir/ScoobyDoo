using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Additive_Loader : MonoBehaviour
{
    public CheckMethod checkMethod;
    public float loadRange;
    public GameObject player;
    private Transform playerPos;

    private bool isLoaded; // distance
    private bool shouldLoad; // trigger
    public enum CheckMethod{
        Distance,
        Trigger
    }
    void Start()
    {
        playerPos = player.transform;
        // sprawdzenie czy dana scena jest juz zaladowana - domyslnie, manu odpala 2 sceny ( SampleScene, Level0 ) 
        if(SceneManager.sceneCount > 0)
        {
            for(int i = 0; i< SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if(scene.name == gameObject.name)
                {
                    isLoaded = true;
                    break;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(checkMethod == CheckMethod.Distance)
        {
            CheckMethodByDistance();
        }
        else if(checkMethod == CheckMethod.Trigger)
        {
            CheckMethodByTrigger();
        }
    }

    private void CheckMethodByDistance()
    {
        if(Vector3.Distance(transform.position, playerPos.position) < loadRange)
        {
            Load();
        }
        else
        {
            Unload();
        }
    }

    private void CheckMethodByTrigger()
    {
        if (shouldLoad)
        {
            Load();
        }
        else
        {
            Unload();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            shouldLoad = true;
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shouldLoad = false;
        }
    }


    private void Load()
    {
        if(!isLoaded)
        {
            SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);
            isLoaded = true;
        }
    }

    private void Unload()
    {
        if (isLoaded)
        {
            SceneManager.UnloadSceneAsync(gameObject.name);
            isLoaded = false;
        }
    }

    // mo¿na usun¹c
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, loadRange);
    }
}
