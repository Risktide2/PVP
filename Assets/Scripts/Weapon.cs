using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WebSocketSharp;

public class Weapon : MonoBehaviour
{
    public int damage;

    public Camera cam;

    public float fireRate;

    [Header("VFX")]
    public GameObject hitVFX;

    private float nextFire;

    [Header("Ammo")]
    public int mag = 5;

    public int ammo = 30;
    public int magAmmo = 30;

    [Header("UI")]
    public TextMeshProUGUI magText;
    public TextMeshProUGUI ammoText;

    [Header("Animation")]
    public Animator animator;

    public AnimationClip reload;


    bool isreloading;

    void Start()
    {
        magText.text = mag.ToString();
        ammoText.text = ammo + "/" + magAmmo;
    }


    // Update is called once per frame
    void Update()
    {
        if (nextFire > 0)
            nextFire -= Time.deltaTime;

        if (Input.GetButton("Fire1") && nextFire <= 0 && ammo > 0 && !isreloading)
        {
            nextFire = 1 / fireRate;

            ammo--;

            magText.text = mag.ToString();
            ammoText.text = ammo + "/" + magAmmo;

            Fire();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }    
       void Reload()
        {
        Debug.Log("reload started");
            isreloading = true;
            animator.SetTrigger("reload");
        
            if (mag> 0)
            {
                mag--;

                ammo = magAmmo;
            }


            magText.text = mag.ToString();
        ammoText.text = ammo + "/" + magAmmo;

        }


    public void FinishReload()
    {
        isreloading = false;
        Debug.Log("finished reload");
    }

    void Fire()
        {
            Ray ray = new Ray(cam.transform.position, cam.transform.forward);

            RaycastHit hit;

            if (Physics.Raycast(ray.origin, ray.direction, out hit, 100f))
            {
                PhotonNetwork.Instantiate(hitVFX.name, hit.point, Quaternion.identity);


                if (hit.transform.gameObject.GetComponent<health>())
                {
                    hit.transform.gameObject.GetComponent<PhotonView>().RPC("TakeDamage", RpcTarget.All, damage);
                }
            }
        }


    
}
