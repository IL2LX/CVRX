namespace CVRX.Mods.ESP
{
    internal class UpdateModule
    {
        public static void OnUpdate()
        {
            LineESP.UpdateLines();
            BoxESP.UpdateBox();
        }
    }
}