using System.Text;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public BinaryTree _guessing_tree;
    public Text _text;
    public char _prev;
    public string _animal;
    public string _question;
    private InputField _input_field;
    public string _file_name = "AnimalTree.txt";

    public BinaryTree LoadTree()
    {
        StreamReader reader = new StreamReader(_file_name, Encoding.Default);
        _guessing_tree.CreateTree(reader);
        reader.Close();
        _guessing_tree._current = _guessing_tree._root;
        return _guessing_tree;
    }

    public void UpdateText(string text)
    {
        if (text[0] == '*')
        {
            string new_text = text.Substring(1);
            text = ("Is it a " + new_text);
        }
        _text.text = text;
    }

    public void GetInput(string text)
    {
        _input_field.text = "";
    }

    public void Guess(bool answer)  //child is the location the new question should be relative to previous question
    {
        if (!answer)
        {
            // CPU LOSE CASE
            _input_field.interactable = true;
            UpdateText("You WON!!! What was your animal?");
            _input_field.onEndEdit.AddListener(GetAnimal);

            UpdateText("What is a question that could be used to identify your animal?");
            _input_field.onEndEdit.AddListener(GetQuestion);

            TreeNode previous = _guessing_tree._current;
            string line;
            string file = "";

            StreamReader reader = new StreamReader(_file_name);
            while (reader.EndOfStream == false)
            {
                line = reader.ReadLine();                   //read line
                if (file != "")                             //add new lines in between
                    file += "\n";
                if (line == previous.value)                    //if line is the previous preorder node
                {
                    file += _question;
                    file += "\n" + line;
                    file += "\n" + _animal;
                }
                else
                    file += line;
            }
            reader.Close();

            StreamWriter writer = new StreamWriter(_file_name);
            writer.Write(file);
            writer.Close();
        }
        else
        {
            // CPU WIN CASE
            UpdateText("I Guessed Correct!! Would you like to try again?");
        }
    }

    public void GetAnimal(string text)
    {
        _animal = text;
    }

    public void GetQuestion(string text)
    {
        _question = text;
    }

    public void yes()
    {
        if (_guessing_tree._current.value[0] == '*')    //if leaf node
        {
            if (_prev == 'Y')                           //if previous answer was yes, new question is right child
                Guess(true);
            else                                        //if prev answer was no, new question is left child
                Guess(true);        
        }
        else
        {
            TreeNode previous = _guessing_tree._current;
            TreeNode new_node = _guessing_tree.TraverseTree(_guessing_tree._current, true);
            if (new_node != null)
                UpdateText(new_node.value);
        }
        _prev = 'Y';
    }
    public void no()
    {
        if (_guessing_tree._current.value[0] == '*')    //if leaf node
        {
            if (_prev == 'Y')                           //if previous answer was yes, new question is right child
                Guess(false);
            else                                        //if previous answer was no, new question is left child
                Guess(false);
        }
        else
        {
            TreeNode previous = _guessing_tree._current;
            TreeNode new_node = _guessing_tree.TraverseTree(_guessing_tree._current, false);

            if (new_node != null)
                UpdateText(new_node.value);
        }
        _prev = 'N';
    }

    // Start is called before the first frame update
    void Start()
    {
        _input_field = GameObject.Find("InputElement").GetComponent<InputField>();
        _input_field.interactable = false;
        _guessing_tree = LoadTree();
        UpdateText(_guessing_tree._root.value);
    }
}
