using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryTree : MonoBehaviour
{
    public TreeNode _root;

    public bool Add(int value)
    {
        TreeNode previous = null;
        TreeNode current = this._root;

        while (current != null)
        {
            previous = current;
            if (value < current.value) //Is new node in left tree? 
                current = current.left;
            else if (value > current.value) //Is new node in right tree?
                current = current.right;
            else
            {
                //Exist same value
                return false;
            }
        }

        TreeNode newNode = new TreeNode();
        newNode.value = value;

        if (this._root == null)//Tree ise empty
            this._root = newNode;
        else
        {
            if (value < previous.value)
                previous.left = newNode;
            else
                previous.right = newNode;
        }

        return true;
    }
}
