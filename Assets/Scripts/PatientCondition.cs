using System.Collections.Generic;

[System.Serializable]
public class PatientCondition
{
    public int age;
    public string sex;
    public string disease;
    public List<string> symptoms;
    public List<string> treatment;
}
