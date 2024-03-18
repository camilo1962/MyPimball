using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;




public class Cambiar : MonoBehaviour
{
    public int score;
    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = new Vector3(-22f,1.2f, 11f);
        ScoreManager.instance.AddScore(score);
        FindObjectOfType<AudioManager>().Play("Tele");

    }
}
