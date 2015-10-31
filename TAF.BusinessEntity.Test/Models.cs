namespace TAF.BusinessEntity.Test
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class User : EfBusiness<User>
    {
        public string Name
        {
            get; set;
        }
    }

    public class Student : EfBusiness<Student>
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