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
        var currentDir = Path.GetDirectoryName(a.Location);
        var easyLinkPath = "easylink.dll";
        var exportPath = Path.Combine(currentDir, easyLinkPath);

        if (File.Exists(exportPath))
            return;
            
        var resFilestream = a.GetManifestResourceStream("EasyLinkSDKCSharp.Dependencies.easylink.dll");

        if (resFilestream != null)
        {
            BinaryReader br = new BinaryReader(resFilestream);
            FileStream fs = new FileStream(exportPath, FileMode.Create); // Say
            BinaryWriter bw = new BinaryWriter(fs);
            byte[] ba = new byte[resFilestream.Length];
            resFilestream.Read(ba, 0, ba.Length);
            bw.Write(ba);
            br.Close();
            bw.Close();
            resFilestream.Close();
        }
        
    }

    public bool Connect()
    {
        return cl_connect();
    }

    public void Disconnect()
    {
        cl_disconnect();
    }

    public bool SetLeds(string[] data)
    {
        return cl_led(data);
    }

    public string GetVersion()
    {
        StringBuilder builder = new StringBuilder(20);
        cl_version(builder);
        return builder.ToString();
    }
    
    public string GetMcuVersion()
    {
        StringBuilder builder = new StringBuilder(100);
        cl_get_mcu_version(builder);
        return builder.ToString();
    }
    
    public string GetBleVersion()
    {
        StringBuilder builder = new StringBuilder(100);
        cl_get_ble_version(builder);
        return builder.ToString();
    }

    public int GetFileCount()
    {
        return cl_get_file_count();
    }

    public bool Beep(ushort freq, ushort duration)
    {
        return cl_beep(freq, duration);
    }

    public int GetBattery()
    {
        return cl_get_battery();
    }

    public void SetRealtimeCallback(cl_realtimeCallback callback)
    {
        cl_set_readtime_callback(callback);
    }

    public void SetRealtimeMode()
    {
        cl_switch_real_time_mode();
    }

    public void Dispose()
    {
        Disconnect();
    }
}