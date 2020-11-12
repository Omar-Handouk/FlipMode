using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ScenesHandler : MonoBehaviour
{
    public static ScenesHandler Instance;
    
    void Awake() {
        if (ScenesHandler.Instance == null) {
            ScenesHandler.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(this);
        }
    }
}
