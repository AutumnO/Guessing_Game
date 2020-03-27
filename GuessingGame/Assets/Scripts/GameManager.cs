using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public void LoadTree(string file_name)
    {
        BinaryTree guessing_tree = gameObject.AddComponent<BinaryTree>();
        StreamReader reader = new StreamReader(file_name, Encoding.Default);
        guessing_tree.CreateTree(guessing_tree._root, reader);
        reader.Close();
    }

    public void yes()
    {
        Debug.Log("Clicked Yes");
    }
    public void no()
    {
        Debug.Log("Clicked No");
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadTree("AnimalTree.txt");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
