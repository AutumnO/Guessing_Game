using System.Text;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public BinaryTree _guessing_tree;
    public Text _text;
    public char _prev;
    public string _animal;
    public string _question;
    private InputField _input_field;
    private Button _restart;
    private Button _yes;
    private Button _no;
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

    public IEnumerator Guess(bool answer)  //child is the location the new question should be relative to previous question
    {
        _yes.interactable = false;
        _no.interactable = false;

        if (!answer)
        {
            // CPU LOSE CASE
            _input_field.interactable = true;

            UpdateText("You WON!!! What was your animal?");
            yield return StartCoroutine(GetData("animal"));

            UpdateText("What is a question that could be used to identify your animal?");
            yield return StartCoroutine(GetData("question"));

            TreeNode previous = _guessing_tree._current;
            string line;
            string file = "";

            StreamReader reader = new StreamReader(_file_name);
            while (reader.EndOfStream == false)
            {
                line = reader.ReadLine();                   //read line
                if (file != "")                             //add new lines in between
                    file += "\n";
                if (line == previous.value && _question != "" && _animal != "")                    //if line is the previous preorder node
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

            UpdateText("Thanks for improving the guesser! Would you like to try again?");
            _restart.interactable = true;
        }
        else
        {
            // CPU WIN CASE
            UpdateText("I Guessed Correct!! Would you like to try again?");
            _restart.interactable = true;
        }
    }

    private IEnumerator WaitForInput(KeyCode key)
    {
        yield return new WaitUntil(() => Input.GetKeyDown(key));
    }
    
    private IEnumerator GetData(string type)
    {
        yield return StartCoroutine(WaitForInput(KeyCode.Return));
        if (type == "animal")
            _animal = _input_field.text;
        else if (type == "question")
            _question = _input_field.text;
        _input_field.text = "";
    }

    public void yes()
    {
        if (_guessing_tree._current.value[0] == '*')    //if leaf node
        {
            if (_prev == 'Y')                           //if previous answer was yes, new question is right child
                StartCoroutine(Guess(true));
            else                                        //if prev answer was no, new question is left child
                StartCoroutine(Guess(true));        
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
                StartCoroutine(Guess(false));
            else                                        //if previous answer was no, new question is left child
                StartCoroutine(Guess(false));
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
        _yes = GameObject.Find("YesButton").GetComponent<Button>();
        _yes.interactable = true;
        _no = GameObject.Find("NoButton").GetComponent<Button>();
        _no.interactable = true;
        _restart = GameObject.Find("RestartButton").GetComponent<Button>();
        _restart.interactable = false;
        _input_field = GameObject.Find("InputElement").GetComponent<InputField>();
        _input_field.interactable = false;
        _guessing_tree = LoadTree();
        UpdateText(_guessing_tree._root.value);
    }
}
