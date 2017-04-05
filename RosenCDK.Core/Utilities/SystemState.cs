namespace RosenCDK.Utilities
{
    public class SystemState
    {
        private static bool systemState;
        public static void SetSystemState(bool value)
        {
            systemState = value;
        }
        public static bool GetSystemState()
        {
            return systemState;
        }
    }
}
