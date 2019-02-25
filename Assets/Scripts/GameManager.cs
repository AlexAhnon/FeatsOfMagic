using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject characterObject;
    public Inventory inventory;
    public ItemGenerator itemGenerator;

    private void OnValidate() {
        inventory = GetComponent<Inventory>();
        itemGenerator = GetComponent<ItemGenerator>();
    }

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
        } else {
            Destroy(this);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
