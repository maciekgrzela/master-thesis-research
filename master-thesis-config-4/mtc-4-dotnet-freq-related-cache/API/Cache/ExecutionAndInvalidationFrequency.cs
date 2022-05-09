namespace API.Cache
{
    public class ExecutionAndInvalidationFrequency
    {
        public string EndpointName { get; set; }
        public int ExecutionCount { get; set; }
        public int InvalidatorExecutionCount { get; set; }
    }
}