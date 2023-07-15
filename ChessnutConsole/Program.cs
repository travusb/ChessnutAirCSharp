using EasyLinkSDKCSharp;

using ChessnutAirAPI api = new ChessnutAirAPI();
api.Connect();

var batteryLevel = api.GetBattery();
var fileCount = api.GetFileCount();
var version = api.GetVersion();
var mcuVersion = api.GetMcuVersion();
var bleVersion = api.GetBleVersion();

api.SetRealtimeCallback(RealtimeCallback);
api.SetRealtimeMode();

Console.WriteLine("Press enter to exit...");
Console.ReadLine();
Console.WriteLine($"Battery: {batteryLevel}");
Console.WriteLine($"Version: {version}");
Console.WriteLine($"File Count: {fileCount}");
Console.WriteLine($"MCU Version: {mcuVersion}");
Console.WriteLine($"BLE Version: {bleVersion}");

void RealtimeCallback(string fen, int length)
{
    Console.WriteLine(fen);
}