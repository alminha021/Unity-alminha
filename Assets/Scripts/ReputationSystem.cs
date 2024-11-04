public class ReputationSystem
{
    public int Reputation { get; private set; }

    public ReputationSystem()
    {
        Reputation = 30;
    }

    public void UpdateReputation(bool successfulTreatment)
    {
        if (successfulTreatment)
        {
            Reputation += 1;
        }
        else
        {
            Reputation -= 1;
        }
    }
}