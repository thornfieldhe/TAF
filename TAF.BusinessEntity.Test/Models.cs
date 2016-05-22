namespace TAF.BusinessEntity.Test
{
    public class User : BaseBusiness<User>
    {
        public string Name
        {
            get; set;
        }
    }

    public class Student : BaseBusiness<Student>
    {
        public int Age
        {
            get; set;
        }

        public bool Sex
        {
            get; set;
        }
    }
}