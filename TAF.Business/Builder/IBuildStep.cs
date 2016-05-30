namespace TAF.Business
{
    using System.Reflection;

    public interface IBuildStep
    {
        int Sequence
        {
            get;
        }

        int Times
        {
            get;
        }

        MethodInfo Handler
        {
            get; set;
        }
    }
}