using System.Collections.Generic;
using UnityEngine;

public class HospitalManager : MonoBehaviour
{
    public ReputationSystem reputationSystem;
    public Queue<Patient> patientQueue;

    public Dictionary<string, int> medicineStock;

    private void Start()
    {
        reputationSystem = new ReputationSystem();
        patientQueue = new Queue<Patient>();

        medicineStock = new Dictionary<string, int>
        {
            {"Vacina", 3},
            {"Antibiótico", 3},
            {"Analgésico", 3}
        };
    }

    public void ProcessTurn()
    {
        if (patientQueue.Count > 0)
        {
            Patient patient = patientQueue.Dequeue();
            bool success = TreatPatient(patient);
            reputationSystem.UpdateReputation(success);
        }
    }

    private bool TreatPatient(Patient patient)
    {
        // Dummy treatment logic
        if (medicineStock.ContainsKey(patient.RequiredMedicine) && medicineStock[patient.RequiredMedicine] > 0)
        {
            medicineStock[patient.RequiredMedicine]--;
            return true; // Successful treatment
        }
        return false; // Failed treatment
    }
}
