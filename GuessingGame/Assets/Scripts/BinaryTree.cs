using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryTree : MonoBehaviour
{
    public TreeNode _root;

    public TreeNode CreateTree(TreeNode root, string line)
    {
        
        if (root != null)
        {
            root = CreateTree(root.left, line);
            root = CreateTree(root.right, line);
        }

        root.value = line;
        return root;
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
