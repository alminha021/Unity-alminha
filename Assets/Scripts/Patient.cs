public class Patient
{
    public string Disease { get; private set; }
    public string RequiredMedicine { get; private set; }

    public Patient(string disease, string requiredMedicine)
    {
        Disease = disease;
        RequiredMedicine = requiredMedicine;
    }
}
