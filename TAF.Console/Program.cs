namespace TAF.Console
{

    using TAF.Utility;
    class Program
    {
        static void Main(string[] args)
        {
            var a = Encrypt.DesEncrypt("hxh@ike-global.com");
            System.Console.Write(a);
            System.Console.Read();
        }
    }
}
