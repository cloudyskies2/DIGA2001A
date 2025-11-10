using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

/// Title: Let's Create: A sliding puzzle game in Unity
/// Author: Firnox
/// Date: 02 August 2022
/// Code version: Unity 2021.3.6f1
/// Availabiliy:https://www.youtube.com/watch?v=IgBjJ-bexeo
/// 

public class PuzzleManager2 : MonoBehaviour
{
    [SerializeField] private Transform gameTransform;
    [SerializeField] private Transform piecePrefab;

    private List<Transform> pieces;
    private int emptyLocation;
    private int size;
    private bool shuffling = false;

    public TextMeshProUGUI completionMessage;

    //public Animator sceneTransition;
    //public float transitionTime;


    //Create the game setup with size x size pieces.
    private void CreateGamePieces(float gapThickness)
    {
        //Width of each tile.
        float width = 1 / (float)size;

        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                Transform piece = Instantiate(piecePrefab, gameTransform);
                pieces.Add(piece);

                piece.localPosition = new Vector3(-1 + (2 * width * col) + width, +1 - (2 * width * row) - width, 0);
                piece.localScale = ((2 * width) - gapThickness) * Vector3.one;
                piece.name = $"{(row * size) + col}";

                //Empty space in the puzzle
                if ((row == size - 1) && (col == size - 1))
                {
                    emptyLocation = (size * size) - 1;
                    piece.gameObject.SetActive(false);
                }
                else
                {
                    //Map the UV coordinates appropriately, they are 0->1.
                    float gap = gapThickness / 2;
                    Mesh mesh = piece.GetComponent<MeshFilter>().mesh;

                    Vector2[] uv = new Vector2[4];

                    //UV coordinate order: (0, 1), (1, 1), (0, 0), (1, 0)
                    uv[0] = new Vector2((width * col) + gap, 1 - ((width * (row + 1)) - gap));
                    uv[1] = new Vector2((width * (col + 1)) - gap, 1 - ((width * (row + 1)) - gap));
                    uv[2] = new Vector2((width * col) + gap, 1 - ((width * row) + gap));
                    uv[3] = new Vector2((width * (col + 1)) - gap, 1 - ((width * row) + gap));

                    //Assign our new UVs to the mesh
                    mesh.uv = uv;

                }
            }
        }
    }

    void Start()
    {
        pieces = new List<Transform>();
        size = 3;
        CreateGamePieces(0.01f);
        //StartCoroutine(WaitShuffle(01f));
        RandomShuffle();
        completionMessage.gameObject.SetActive(false);

    }

    /*IEnumerator LoadLevel(int levelIndex)
    {
        sceneTransition.SetTrigger("Start");

        SceneManager.LoadScene(levelIndex);

        yield return new WaitForSeconds(1);
    }*/

    void Update()
    {
        //Check for completion
        if (!shuffling && CheckCompletion())
        {
            shuffling = true;
            completionMessage.gameObject.SetActive(true);
            //StartCoroutine(WaitShuffle(01f));          
        }

        /*if (CheckCompletion() == true)
        {
            completionMessage.gameObject.SetActive(true);
        }*/

        else if (CheckCompletion() == false)
        {
            completionMessage.gameObject.SetActive(false);
        }

        /*if (Input.GetKeyDown(KeyCode.N))
        {
            completionMessage.gameObject.SetActive(false);
            interactionPrompt.gameObject.SetActive(true);
        }*/

        if (Input.GetKeyDown(KeyCode.Q) && CheckCompletion() == true)
        {
            SceneManager.LoadScene(3);
            //DontDestroyOnLoad(this.gameObject);
            //LoadNextLevel();
        }
        //}

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                for (int i = 0; i < pieces.Count; i++)
                {
                    if (pieces[i] == hit.transform)
                    {
                        if (SwapIfValid(i, -size, size)) { break; }
                        if (SwapIfValid(i, +size, size)) { break; }
                        if (SwapIfValid(i, -1, 0)) { break; }
                        if (SwapIfValid(i, +1, size)) { break; }

                    }
                }
            }
        }
    }

    /*public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }*/

    private bool SwapIfValid(int i, int offset, int colCheck)
    {
        if (((i % size) != colCheck) && ((i + offset) == emptyLocation))
        {
            //Swap them in the game state
            (pieces[i], pieces[i + offset]) = (pieces[i + offset], pieces[i]);

            //Swap their transform
            (pieces[i].localPosition, pieces[i + offset].localPosition) = ((pieces[i + offset].localPosition, pieces[i].localPosition));

            //Update empty location
            emptyLocation = i;

            return true;
        }
        return false;
    }

    private bool CheckCompletion()
    {
        for (int i = 0; i < pieces.Count; i++)
        {
            if (pieces[i].name != $"{i}")
            {
                return false;
            }
        }
        return true;
    }

    //private IEnumerator WaitShuffle(float duration)
    //{
       // yield return new WaitForSeconds(duration);
        //Shuffle();
        //shuffling = false;
    //}

    private void RandomShuffle()
    {
        //yield return new WaitForSeconds(duration);
        Shuffle();
        shuffling = false;
    }

    //Brute force shuffling
    private void Shuffle()
    {
        int count = 0;
        int last = 0;

        while (count < (size * size * size))
        {
            //Pick a random location
            int random = Random.Range(0, size * size);

            //Cannot undo last move
            if (random == last) { continue; }
            last = emptyLocation;

            //Try surrounding spaces looking for valid move
            if (SwapIfValid(random, +size, size))
            {
                count++;
            }
            else if (SwapIfValid(random, +size, size))
            {
                count++;
            }
            else if (SwapIfValid(random, -1, 0))
            {
                count++;
            }
            else if (SwapIfValid(random, +1, size - 1))
            {
                count++;
            }
        }
    }
}
