using System.IO;
using UnityEngine;

public class BinaryTree : MonoBehaviour
{
    public TreeNode _root;
    public TreeNode _current;

    public void CreateTree(StreamReader reader)
    {
        /*reader is called every recursion, need to separate them somehow if using recursion?
            can't debug because unity freezes when it runs >:(
    */
        string line = "";
        TreeNode root = _root;
        using (reader)
        {   
            while (reader.EndOfStream == false)
            {
                line = reader.ReadLine();
                if (line != "/end/")
                {
                    TreeNode new_node = gameObject.AddComponent<TreeNode>();
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

    }

    public TreeNode TraverseTree(TreeNode previous, bool answer)
    {
        if (answer)
        {
            _current = previous.right;
            return previous.right;
        }
        else
        {
            _current = previous.left;
            return previous.left;
        }
    }

    public TreeNode LocateEnd(TreeNode end_node)
    {
        if (end_node.right != null)
        {
            return end_node = LocateEnd(end_node.right);
        }
        else if (end_node.left != null)
        {
            return end_node = LocateEnd(end_node.left);
        }
        else
            return end_node;
    }
}
