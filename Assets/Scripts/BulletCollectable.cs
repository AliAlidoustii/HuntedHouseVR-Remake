using UnityEngine;
using Photon.Pun;

public class BulletCollectable : MonoBehaviourPun
{
    private UIManager uiManager;

    private void Start()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (uiManager != null)
            {
                uiManager.AddBullet();
            }
            Destroy(gameObject);
        }
    }
}
