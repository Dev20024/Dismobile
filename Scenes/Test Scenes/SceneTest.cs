using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTest : MonoBehaviour
{
    [SerializeField] PlayerStateEvent changeScene;
    [SerializeField] InMainMenu newScene = new InMainMenu();
    // Start is called before the first frame update
    void Start()
    {
       StartCoroutine( testFunction());
    }
    

    IEnumerator testFunction() {
        yield return new WaitForSeconds(5);
        Debug.Log("firing change scene event");
        changeScene.Fire(newScene);
    }

    
}
