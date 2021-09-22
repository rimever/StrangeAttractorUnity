using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 書籍の通りに書いたが、なぜか正しく動作しない
public class Turning : MonoBehaviour
{
    private float speed = 10f;

    private float th;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        th = (th + Time.deltaTime * speed) % 360f;
        transform.rotation = Quaternion.Euler(0, th, 0);
    }
}