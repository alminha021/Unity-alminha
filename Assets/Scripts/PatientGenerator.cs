using UnityEngine;

public class PatientGenerator : MonoBehaviour
{
    public HospitalManager hospitalManager;

    private void Start()
    {
        InvokeRepeating("GeneratePatient", 1f, 5f); // Generates a patient every 5 seconds
    }

    void GeneratePatient()
    {
        string[] diseases = { "dor", "infecção", "sem imunidade" };
        string[] medicines = { "Vacina", "Antibiótico", "Painkiller" };

        int index = Random.Range(0, diseases.Length);
        Patient newPatient = new Patient(diseases[index], medicines[index]);
        hospitalManager.patientQueue.Enqueue(newPatient);

        Debug.Log("New patient generated with " + newPatient.Disease);
    }
}
