using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BoundaryTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 通过标签判断是否撞到球
        if (collision.gameObject.CompareTag("Ball"))
        {
            
            SceneManager.LoadScene("GameOverScene");
        }
    }
}
