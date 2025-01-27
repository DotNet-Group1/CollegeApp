namespace CollegeApp.MyLogging
{
    public class LogToServerMemory: IMyLogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("Logto Server memory");
            //Write to server memory
        }
    }
}
