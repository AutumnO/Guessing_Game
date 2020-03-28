using UnityEngine;

public class TreeNode : MonoBehaviour
{
    public TreeNode left { get; set; }
    public TreeNode right { get; set; }
    public TreeNode parent { get; set; }
    public string value { get; set; }
}
