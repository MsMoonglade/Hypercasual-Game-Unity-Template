using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using System.Linq;
using System.Text;
using Lofelt.NiceVibrations;
using TMPro;

[RequireComponent(typeof(CharactersMover))]
[RequireComponent(typeof(CharacterShooter))]
[RequireComponent(typeof(CharacterUi))]
public class CharacterBehaviour : Characters
{
    public static CharacterBehaviour instance;

    [Header ("Local References")]
    public GameObject model;   
    public ParticleSystem powerUpParticle;
    public ParticleSystem powerDownParticle;

    [Header("Private Class References")]
    [HideInInspector]
    public CharactersMover characterMover;
    [HideInInspector]
    public CharacterShooter characterShooter;
    [HideInInspector]
    public CharacterUi characterUi;
    [HideInInspector]
    public PlayerInventory inventory;

    private void Awake()
    {
        instance = this;   
        //LoadPlayerValue();

        characterMover = transform.GetComponent<CharactersMover>();
        characterShooter = transform.GetComponent<CharacterShooter>();
        characterUi = transform.GetComponent<CharacterUi>();
        inventory = transform.GetComponent<PlayerInventory>();
    }

    private void Start()
    {
        CameraManager.Instance.SetCameraTarget(CameraManager.Instance.mainCamera, transform);

        CharactersManager.Instance.SetCurrentActiveCharacter(this);
    }

    private void OnEnable()
    {
        EventManager.StartListening(Events.playGame, OnPlayGame);
    }

    private void OnDisable()
    {
        EventManager.StopListening(Events.playGame, OnPlayGame);
    }

    protected override void OnTriggerEnter(Collider i_col)
    {
        base.OnTriggerEnter(i_col);

        if (i_col.transform.CompareTag("Wall"))
        {
            i_col.transform.GetComponent<WallBehaviour>().Take();
        }

        if (i_col.transform.CompareTag("Obstacles"))
        {
            characterMover.HitObstaclesFeedback();
        }

        if (i_col.transform.CompareTag("MidSegment"))
        {           
        }

        if (i_col.transform.CompareTag("EndGame"))
        {
            EnterEndGame();
        }
    }

    /// <summary>
    /// Caharacter Actually Start the end Game 
    /// </summary>
    private void EnterEndGame()
    {   
        EndGameBehaviour.Instance.StartEndGame();
    }   

    private void OnPlayGame(object sender)
    {
        characterShooter.StartShoot();
    }

    /*
    public void SavePlayerValue()
    {       
        int[] valueToSave = new int[editObjectParent.transform.childCount];
        Vector3[] posToSave = new Vector3[editObjectParent.transform.childCount];

        for(int i = 0; i < editObjectParent.transform.childCount; i++)
        {
            valueToSave[i] = editObjectParent.transform.GetChild(i).GetComponent<PlayerMoneyObject>().value;
            posToSave[i] = editObjectParent.transform.GetChild(i).transform.localPosition;
        }

        PlayerPrefs.DeleteKey("SavedValue");
        PlayerPrefs.SetString("SavedValue", string.Join("###", valueToSave));

        string posToSaveString = SerializeVector3Array(posToSave);
        PlayerPrefs.DeleteKey("SavedPos");
        PlayerPrefs.SetString("SavedPos", posToSaveString);

        LoadPlayerValue();
    }

    public void LoadPlayerValue()
    {
        int xQuantity = (int)(printerObject.transform.localScale.x / moneyDecalScaleX);
        int yQuantity = (int)(printerObject.transform.localScale.z / moneyDecalScaleY);

        gridElementPos.Clear();
        gridElementValue.Clear();

        if (PlayerPrefs.HasKey("SavedValue") && PlayerPrefs.HasKey("SavedPos"))
        {
            //LoadValue
            string[] tempValue = PlayerPrefs.GetString("SavedValue").Split(new[] { "###" }, StringSplitOptions.None);

            if (tempValue[0] != "")
            {
                if (tempValue.Length >= 1)
                    for (int i = 0; i < tempValue.Length; i++)
                    {
                        gridElementValue.Add(int.Parse(tempValue[i]));
                    }

                //LoadPos
                string posStringNotSplitted = PlayerPrefs.GetString("SavedPos");
                Vector3[] allPosSplitted = DeserializeVector3Array(posStringNotSplitted);
                if (allPosSplitted.Length >= 1)
                    for (int i = 0; i < allPosSplitted.Length; i++)
                    {
                        gridElementPos.Add(allPosSplitted[i]);
                    }
            }
        }

        else
        {
            Vector3 startPoint = new Vector3(          
                printerObject.transform.position.x - (printerObjectScale.x / 2) + (moneyDecalScaleX / 2),          
                0,          
                printerObject.transform.position.z - (printerObjectScale.y / 2) + (moneyDecalScaleY / 2));

            List<int> tempValue = new List<int>();
            List<float> tempX = new List<float>();
            List<float> tempZ = new List<float>();

            for (int i = 0; i < xQuantity; i++)
            {
                for (int j = 0; j < yQuantity; j++)
                {
                    tempValue.Add(1);
                    tempX.Add(startPoint.x);
                    tempZ.Add(startPoint.z);

                    startPoint += new Vector3(0, 0, moneyDecalScaleY);
                }

                startPoint += new Vector3(moneyDecalScaleX, 0, (-yQuantity * moneyDecalScaleY));
            }

            int[] valueToSave = new int[tempValue.Count];
            Vector3[] posToSave = new Vector3[tempX.Count];

            for (int i = 0; i < valueToSave.Length; i++)
            {
                valueToSave[i] = tempValue[i];
                gridElementValue.Add(tempValue[i]);

                posToSave[i] = new Vector3(tempX[i], -0.1f, tempZ[i]);
                gridElementPos.Add(posToSave[i]);
            }

            string posToSaveString = SerializeVector3Array(posToSave);          

            PlayerPrefs.SetString("SavedValue", string.Join("###", valueToSave));
            PlayerPrefs.SetString("SavedPos", posToSaveString);
        }
    }

    public static string SerializeVector3Array(Vector3[] aVectors)
    {
        StringBuilder sb = new StringBuilder();
        foreach (Vector3 v in aVectors)
        {
            sb.Append(v.x).Append(" ").Append(v.y).Append(" ").Append(v.z).Append("|");
        }
        if (sb.Length > 0) // remove last "|"
            sb.Remove(sb.Length - 1, 1);
        return sb.ToString();
    }
    public static Vector3[] DeserializeVector3Array(string aData)
    {
        string[] vectors = aData.Split('|');
        Vector3[] result = new Vector3[vectors.Length];
        for (int i = 0; i < vectors.Length; i++)
        {
            string[] values = vectors[i].Split(' ');
            if (values.Length != 3)
                throw new System.FormatException("component count mismatch. Expected 3 components but got " + values.Length);
            result[i] = new Vector3(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));
        }
        return result;
    }
    */
}