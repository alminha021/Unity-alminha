using System.Collections.Generic;
using UnityEngine;

public class PatientConditionLoader : MonoBehaviour
{
    public PatientCondition[] patientConditions;

    void Start()
    {
        LoadPatientConditions();
    }

    private void LoadPatientConditions()
    {
        // Load JSON file
        TextAsset jsonFile = Resources.Load<TextAsset>("PatientConditions"); // Place JSON in a Resources folder
        if (jsonFile != null)
        {
            patientConditions = JsonUtility.FromJson<PatientConditionList>(jsonFile.text).conditions;
        }
        else
        {
            Debug.LogError("PatientConditions.json file not found!");
        }
    }
}

// Helper class for JSON parsing
[System.Serializable]
public class PatientConditionList
{
    public PatientCondition[] conditions;
}
