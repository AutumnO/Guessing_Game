using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public BinaryTree _guessing_tree;
    public BinaryTree LoadTree(string file_name, BinaryTree guessing_tree)
    {
        //BinaryTree guessing_tree = gameObject.GetComponent<BinaryTree>();
        StreamReader reader = new StreamReader(file_name, Encoding.Default);
        guessing_tree.CreateTree(reader);
        reader.Close();
        return guessing_tree;
    }

    public void UpdateText(string text)
    {
        if (text[0] == '*')
        {
            string new_text = text.Substring(1);
            text = ("Is it a " + new_text);
        }

        Text question = gameObject.AddComponent <Text>();
        //q = GameObject.Instantiate<Text>;

        GameObject newText = new GameObject(text.Replace(" ", "-"), typeof(RectTransform));
        var newTextComp = newText.AddComponent<Text>();

        newTextComp.text = text;
        newTextComp.alignment = TextAnchor.MiddleCenter;
        newTextComp.fontSize = 20;

        newText.transform.SetParent(transform);
    }

    public void Guess(bool answer)
    {
        if (!answer)
        {
            // CPU LOSE CASE
            UpdateText("You WON!!! What was your animal?");
            UpdateText("What is a question that could be used to identify your animal?");

        }
        else
        {
            // CPU WIN CASE
            UpdateText("You Lost :(");
        }
    }

    public void yes()
    {
        Debug.Log("Clicked Yes");
        BinaryTree guessing_tree = gameObject.GetComponent<BinaryTree>();
        if (guessing_tree._current.value[0] == '*')    //if leaf node
        {
            Guess(true);
        }
        TreeNode new_node = guessing_tree.TraverseTree(guessing_tree._current, true);
        UpdateText(new_node.value);
    }
    public void no(BinaryTree guessing_tree)
    {
        Debug.Log("Clicked No");
        //BinaryTree guessing_tree = gameObject.GetComponent<BinaryTree>();
        TreeNode babe = guessing_tree._root;
        if (guessing_tree._current.value[0] == '*')    //if leaf node
        {
            Guess(false);
        }
        TreeNode new_node = guessing_tree.TraverseTree(guessing_tree._current, false);
        UpdateText(new_node.value);
    }

    // Start is called before the first frame update
    void Start()
    {
        BinaryTree guessing_tree = null;
        GameObject.Instantiate<BinaryTree>(guessing_tree);
        guessing_tree = LoadTree("AnimalTree.txt", guessing_tree);

        /*BinaryTree guessing_tree = gameObject.AddComponent<BinaryTree>();
        guessing_tree = LoadTree("AnimalTree.txt");
        guessing_tree._current = guessing_tree._root;
        UpdateText(guessing_tree._root.value);*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
