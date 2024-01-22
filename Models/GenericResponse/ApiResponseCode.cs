namespace BrewTask.Models.GenericResponse
{
    public class ApiResponseCode
    {
        public static string Default { get { return "00"; } }
        public static string Success { get { return "200"; } }
        public static string Fail { get { return "403"; } }
        public static string Exception { get { return "500"; } }
        public static string Validation { get { return "400"; } }
    }
}
