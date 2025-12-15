namespace CVRX.Mods.Movements
{
    internal class UpdateModule
    {
        public static void Update()
        {
            SpinBot.Update();
            Flight.Update();
            JetPack.Update();
            SpeedHack.Update();
            SeatOnHead.Update();
            RayCastTP.Update();
        }
    }
}
