using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class health : MonoBehaviour
{
    public int healtH;
    public bool isLocalPlayer;

    [Header("UI")]
    public TextMeshProUGUI healthtext;

    [PunRPC]
    public void TakeDamage(int _damage)
    {
        healtH -= _damage;

        healthtext.text = healtH.ToString();

        if (healtH <= 0)
        {
            if (isLocalPlayer) 
                RoomManager.Instance.Spawnplayer();

            Destroy(gameObject);
        }
    }
}
