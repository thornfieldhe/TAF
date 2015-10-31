namespace TAF.Core
{
    public interface IDbAction
    {
        int Create();

        int Save();

        int Delete();
    }
}