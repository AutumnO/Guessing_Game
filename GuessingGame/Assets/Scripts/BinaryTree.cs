using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class BinaryTree : MonoBehaviour
{
    public TreeNode _root;
    public TreeNode _current;

    public void UpdateText(string text)
    {
        if (text[0] == '*')
        {
            string new_text = text.Substring(1);
            text = ("Is it a " + new_text);
        }

        Text question = gameObject.AddComponent<Text>();
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
        Debug.Log("Clicked Special Yes");
        TreeNode b = _current;
        var c = 0;
        if (_current.value[0] == '*')    //if leaf node
        {
            Guess(true);
        }
        TreeNode new_node = TraverseTree(_current, true);
        UpdateText(new_node.value);
    }

    public void CreateTree(StreamReader reader)
    {
        /*reader is called every recursion, need to separate them somehow if using recursion?
            can't debug because unity freezes when it runs >:(
    */
        string line;
        TreeNode root = _root;
        using (reader)
        {   
            
            while (reader.EndOfStream == false)
            {
                line = reader.ReadLine();
                TreeNode new_node = gameObject.AddComponent<TreeNode>();
                //GameObject.Instantiate <TreeNode>(new_node);
                new_node.value = line;
                bool added = false;

                while (added == false)
                {
                    if (root == null)
                    {
                        if (_root == root)
                        {
                            _root = new_node;
                            root = _root;
                        }
                        else
                            root = new_node;

                        added = true;
                    }
                    else if (root.left == null)
                    {
                        root.left = new_node;
                        root.left.parent = root;
                        added = true;
                        if (line[0] != '*')
                            root = root.left;
                    }
                    else if (root.right == null)
                    {
                        root.right = new_node;
                        root.right.parent = root;
                        added = true;
                        if (line[0] != '*')
                            root = root.right;
                    }
                    else
                    {
                        //move back up to root's parent
                        root = root.parent;
                    }
                }
            }
        }

    }

    public TreeNode TraverseTree(TreeNode prev_q, bool answer)
    {
        if (answer)
        {
            _current = prev_q.right;
            return prev_q.right;
        }
        else
        {
            _current = prev_q.left;
            return prev_q.left;
        }
    }


    public void Go()
    {
        //BinaryTree guessing_tree = gameObject.AddComponent<BinaryTree>();
        
        _current = _root;
        UpdateText(_root.value);
    }

    public bool Add(string value, string child, TreeNode root)
    {
        TreeNode parent = root;

        TreeNode new_node = new TreeNode();
        new_node.value = value;
        if (child == "left")
        {
            parent.left = new_node;
            return true;
        }
        else if (child == "right")
        {
            parent.right = new_node;
            return true;
        }
        else if (root == null)
        {
            this._root = new_node;
            return true;
        }
        else
            return false;
    }
}
