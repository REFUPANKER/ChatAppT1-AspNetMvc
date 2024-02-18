namespace ChatApp.DBM
{
	public class Pool
	{
		//public static Random rnd = new Random();
		//public static string CurrentUrl { get => "https://localhost:7154"; }

		//public static Core dbm = new Core();

		//public static int uid = rnd.Next(1, 20); 

		public static List<string> AddedHubs = new List<string>();
		public delegate void AddHub(string hubName);
		public static event AddHub? OnHubAdded;

		public static void AddNewHub(string hubName)
		{
			AddedHubs.Add(hubName);
			OnHubAdded?.Invoke(hubName);
		}
    }
}
