using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public BinaryTree LoadTree(string file_name)
    {
        BinaryTree guessing_tree = gameObject.AddComponent<BinaryTree>();
        StreamReader reader = new StreamReader(file_name, Encoding.Default);
        guessing_tree.CreateTree(guessing_tree._root, reader);
        reader.Close();
        return guessing_tree;
    }

    public bool UpdateText(string text)
    {
        bool leaf = false;
        if (text[0] == '*')
        {
            string new_text = text.Substring(1);
            text = ("Is it a " + new_text);
            leaf = true;
        }

        Text question = gameObject.AddComponent <Text>();

        GameObject newText = new GameObject(text.Replace(" ", "-"), typeof(RectTransform));
        var newTextComp = newText.AddComponent<Text>();

        newTextComp.text = text;
        newTextComp.alignment = TextAnchor.MiddleCenter;
        newTextComp.fontSize = 20;

        newText.transform.SetParent(transform);

        return leaf;
    }

    public void Guess()
    {

    }

    public void yes()
    {
        Debug.Log("Clicked Yes");
        BinaryTree guessing_tree = gameObject.GetComponent<BinaryTree>();
        TreeNode new_node = guessing_tree.TraverseTree(guessing_tree._current, true);
        bool leaf = UpdateText(new_node.value);
        if (leaf)
            // ***CALL END FUNCTION***
    }
    public void no()
    {
        Debug.Log("Clicked No");
        BinaryTree guessing_tree = gameObject.GetComponent<BinaryTree>();
        TreeNode new_node = guessing_tree.TraverseTree(guessing_tree._current, false);
        bool leaf = UpdateText(new_node.value);
        if (leaf)
             // ***CALL END FUNCTION***
    }

    // Start is called before the first frame update
    void Start()
    {
        BinaryTree guessing_tree = gameObject.AddComponent<BinaryTree>();
        guessing_tree = LoadTree("AnimalTree.txt");
        guessing_tree._current = guessing_tree._root;
        UpdateText(guessing_tree._root.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
