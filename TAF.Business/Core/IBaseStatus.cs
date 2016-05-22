namespace TAF
{
    public interface IBaseStatus
    {
        bool IsClean
        {
            get;
        }

        bool IsDirty
        {
            get;
        }

        bool IsDelete
        {
            get;
        }

        bool IsNew
        {
            get;
        }

        void MarkClean();

        void MarkDirty();

        void MarkNew();

        void MarkDelete();
    }
}
