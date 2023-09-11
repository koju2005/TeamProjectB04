using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{

    [SerializeField] private GameObject _paddle;
    [SerializeField] private GameObject _ball;

    private void Awake()
    {
        Instantiate(_paddle);
        Instantiate(_ball);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void gameStart()
    {

    }
}
