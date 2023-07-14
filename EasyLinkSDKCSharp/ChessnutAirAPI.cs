using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace EasyLinkSDKCSharp;

public class ChessnutAirAPI : IDisposable
{
    [DllImport( "easylink.dll" )]
    private static extern bool cl_connect();
    [DllImport( "easylink.dll" )]
    private static extern void cl_disconnect();
    [DllImport( "easylink.dll")]
    private static extern bool cl_led(string[] data);
    [DllImport( "easylink.dll" )]
    private static extern int cl_version(StringBuilder data);
    [DllImport( "easylink.dll")]
    private static extern int cl_get_mcu_version(StringBuilder data);
    [DllImport( "easylink.dll" )]
    private static extern int cl_get_ble_version(StringBuilder data);
    [DllImport( "easylink.dll" )]
    private static extern bool cl_beep(ushort frequency, ushort duration);
    [DllImport( "easylink.dll" )]
    private static extern int cl_get_battery();
    [DllImport( "easylink.dll" )]
    private static extern int cl_get_file_count();
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate void cl_realtimeCallback(string fen, int length);
    [DllImport( "easylink.dll")]
    private static extern void cl_set_readtime_callback([MarshalAs(UnmanagedType.FunctionPtr)] cl_realtimeCallback callback);
    [DllImport( "easylink.dll" )]
    private static extern bool cl_switch_real_time_mode();

    public ChessnutAirAPI()
    {
        Assembly a = Assembly.GetExecutingAssembly();
        var exportPath = Path.Combine(Path.GetDirectoryName(a.Location) ?? "", "easylink.dll");

        if (File.Exists(exportPath))
            return;
            
        using var resFilestream = a.GetManifestResourceStream("EasyLinkSDKCSharp.Dependencies.easylink.dll");

        if (resFilestream == null) return;
        
        using FileStream fs = new FileStream(exportPath, FileMode.CreateNew);
        for (int i = 0; i < resFilestream.Length; i++)
            fs.WriteByte((byte)resFilestream.ReadByte());
    }

    /// <summary>
    /// Connect to the chessboard with hid.
    /// If the device is not connected, it will automatically connect when the device is plugged in.
    /// </summary>
    /// <returns>True if connected, False otherwise.</returns>
    public bool Connect()
    {
        return cl_connect();
    }

    /// <summary>
    /// Disconnect from the chessboard.
    /// </summary>
    public void Disconnect()
    {
        cl_disconnect();
    }

    /// <summary>
    /// Set the LEDs base on contents of a string. "11111111" will turn on all lights in a row.
    /// "00000000" will turn off all lights in a row.
    /// </summary>
    /// <param name="data">Must be an array of length 8.</param>
    /// <returns>True if LEDs are set and false otherwise.</returns>
    public bool SetLeds(string[] data)
    {
        if (data.Length != 8)
            return false;
        
        return cl_led(data);
    }

    /// <summary>
    /// Get the version of the SDK.
    /// </summary>
    /// <returns>SDK version</returns>
    public string GetVersion()
    {
        StringBuilder builder = new StringBuilder(20);
        cl_version(builder);
        return builder.ToString();
    }
    
    /// <summary>
    /// Get the version of the MCU.
    /// </summary>
    /// <returns>MCU version</returns>
    public string GetMcuVersion()
    {
        StringBuilder builder = new StringBuilder(100);
        cl_get_mcu_version(builder);
        return builder.ToString();
    }
    
    /// <summary>
    /// Get the version of the BLE.
    /// </summary>
    /// <returns>BLE version</returns>
    public string GetBleVersion()
    {
        StringBuilder builder = new StringBuilder(100);
        cl_get_ble_version(builder);
        return builder.ToString();
    }

    /// <summary>
    /// Get the number of game files in internal storage.
    /// </summary>
    /// <returns>Game file count in internal storage.</returns>
    public int GetFileCount()
    {
        return cl_get_file_count();
    }

    /// <summary>
    /// Have the chessboard make an audible beep.
    /// </summary>
    /// <param name="freq">Default value is 1000.</param>
    /// <param name="duration">Default value is 200.</param>
    /// <returns>True is chessboard beeps, false otherwise.</returns>
    public bool Beep(ushort freq, ushort duration)
    {
        return cl_beep(freq, duration);
    }

    /// <summary>
    /// Get the battery level of the chessboard.
    /// </summary>
    /// <returns>Battery level as a percent.</returns>
    public int GetBattery()
    {
        return cl_get_battery();
    }

    /// <summary>
    /// Set the callback method for the realtime mode.
    /// </summary>
    /// <param name="callback">Method used by the realtime mode</param>
    public void SetRealtimeCallback(cl_realtimeCallback callback)
    {
        cl_set_readtime_callback(callback);
    }

    /// <summary>
    /// Set chessboard to realtime mode.
    /// </summary>
    public void SetRealtimeMode()
    {
        cl_switch_real_time_mode();
    }

    /// <summary>
    /// Dispose the API object.
    /// Disconnect will be called on Dispose.
    /// </summary>
    public void Dispose()
    {
        Disconnect();
    }
}