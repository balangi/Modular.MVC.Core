namespace ExtraDepenencyTest
{
    public class AnotherTestService : IAnotherTestService
    {
        public string Test()
        {
            return "Other service outside modules";
        }
    }
}
