using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Cambiar1 : MonoBehaviour
{
    public int score;
    private void OnTriggerEnter(Collider other)
    {
        other.transform.position = new Vector3(18.5f,1.1f, -28.5f);
        ScoreManager.instance.AddScore(score);
        FindObjectOfType<AudioManager>().Play("Tele");
    }
}
