using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[RequireComponent(typeof(PlayerSetups))]
public class Player : NetworkBehaviour {

    [SyncVar]
    bool _isDead = false;
    public bool isDead
    {
        get { return _isDead; }
        protected set { _isDead = value; }
    }

    [SerializeField]
    private int maxHealth = 100;

    [SyncVar]
    private int currentHealth;

    [SerializeField]
    private Behaviour[] disableOnDeath;
    private bool[] wasEnabled;

    [SerializeField]
    private GameObject[] disableGameObjectOnDeath;

    [SerializeField]
    private GameObject deathEffect;

    [SerializeField]
    private GameObject spwanEffect;

    public  void Setup()
    {
        wasEnabled = new bool[disableOnDeath.Length];
        for(int i=0; i < wasEnabled.Length; i++)
        {
            wasEnabled[i] = disableOnDeath[i].enabled;
        }

        SetDefaults(); 
    }
    [ClientRpc]
    public void RpcTakeDamage(int _amount)
    {
        if (isDead)
            return;
        currentHealth -= _amount;

        Debug.Log(transform.name + " now has " + currentHealth + " health. ");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;

        //disable component
        for(int i=0; i<disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }

        //disabale gameObject
        for (int i = 0; i < disableGameObjectOnDeath.Length; i++)
        {
            disableGameObjectOnDeath[i].SetActive(false);
        }

        //disable the collider
        Collider _col = GetComponent<Collider>();
        if (_col != null)
        {
            _col.enabled = true;
        }

        //spawn a death effet
        GameObject _gfxTns= (GameObject) Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(_gfxTns, 3f);

        //switch camera
        if (isLocalPlayer)
        {
            GameManager.instance.SetSceneCameraActive(true);
            GetComponent<PlayerSetups>().playerUIInstance.SetActive(false);
        }

        Debug.Log(transform.name + " is dead.");

        //call respawn method
        StartCoroutine(Respawn());
    }


   private  IEnumerator Respawn()
    {
        yield return new WaitForSeconds(GameManager.instance.matchSettings.respawnTime);

        Transform _spawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = _spawnPoint.position;
        transform.rotation = _spawnPoint.rotation;

        SetDefaults();
    }


   
    private void Update()
    {
        if (!isLocalPlayer)
            return;
        if (Input.GetKeyDown(KeyCode.K))
        {
            RpcTakeDamage(15662);
        }
    }
    

    public void SetDefaults()
    {
        isDead = false;
        currentHealth = maxHealth; 

        //Set Component active
        for(int i = 0;i <disableOnDeath.Length;i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];
        }

        //set active gameObject
        for (int i = 0; i < disableGameObjectOnDeath.Length; i++)
        {
            disableGameObjectOnDeath[i].SetActive(true);
        }

        //enable the collider
        Collider _col = GetComponent<Collider>();
        if (_col != null)
        {
            _col.enabled = true;
        }

        //switch camera
        if (isLocalPlayer)
        {
            GameManager.instance.SetSceneCameraActive(false);
            GetComponent<PlayerSetups>().playerUIInstance.SetActive(true);

        }

        //Create Spawn effect
        GameObject _gfxTns = (GameObject)Instantiate(spwanEffect, transform.position, Quaternion.identity);
        Destroy(_gfxTns, 3f);
    }

}
