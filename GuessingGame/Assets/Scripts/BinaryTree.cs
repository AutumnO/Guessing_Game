﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BinaryTree : MonoBehaviour
{
    public TreeNode _root;

    public void CreateTree(TreeNode root, StreamReader reader)
    {
        /*reader is called every recursion, need to separate them somehow if using recursion?
            can't debug because unity freezes when it runs >:(
    */
        string line;
        using (reader)
        {   
            line = reader.ReadLine();
            while (line != null)
            {
                TreeNode new_node = gameObject.AddComponent <TreeNode>();
                new_node.value = line;

                if (root == null)
                {
                    root = new_node;
                }
                else if (root.left == null)
                {
                    root.left = new_node;
                    if (line[0] != '*')
                        root = root.left;
                }
                else if (root.right == null)
                {
                    root.right = new_node;
                    if (line[0] != '*')
                        root = root.right;
                }
                else
                {
                    //move back up to root's parent
                }
            }
        }

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

    public TreeNode preOrderTraversalMove(TreeNode root)
    {
        TreeNode current = root;

        if (current.left != null)
            return current.left;
        else if (current.right != null)
            return current.right;

        return root;
    }
}
