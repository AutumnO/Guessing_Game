using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public bool LoadTree(string file_name)
    {
        try
        {
            BinaryTree guessing_tree = new BinaryTree();
            TreeNode current = guessing_tree._root;
            string child = "left";
            char prev = '0';
            string line;
            StreamReader the_reader = new StreamReader(file_name, Encoding.Default);

            using (the_reader)      // while stuff to read
            {
                do
                {
                    line = the_reader.ReadLine();
                    if (line != null)
                    {
                        // deal with the stuff
                        current = guessing_tree.CreateTree(current, line);


                       /*
                        if (current == null)
                        {
                            guessing_tree._root.value = line;
                            current = guessing_tree._root;
                        }
                        else if (line[0] == '*')
                        {
                            if (prev == '*')
                                child = "right";
                            else
                                child = "left";

                            guessing_tree.Add(line, child, current);
                        }
                        else
                        {
                            guessing_tree.Add(line, child, current);
                            if (child == "left")
                                current = current.left;
                            else if (child == "right")
                                current = current.right;
                        }

                        prev = line[0];
                        */
                    }
                }

                while (line != null);
                {
                    the_reader.Close();
                    return true;
                }
            }

        }

        catch (Exception e)
        {
            Console.WriteLine("{0}\n", e.Message);
            return false;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
