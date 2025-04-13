using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//场景加载需要的命名空间
public class BacktoMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void StartNew()//关联场景加载
    {
        SceneManager.LoadScene(0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
