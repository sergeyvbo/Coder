namespace Coder.Scenarios
{
    public class ScenarioManager
    {
        public IScenario GetNextScenario()
        {
            return new CodingScenario();
        }
    }
}
