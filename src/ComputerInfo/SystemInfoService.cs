using System;
using System.Linq;
using System.Management;
using System.Text;
using System.Collections.Generic;



namespace ComputerInfo
{
    public class SystemInfoService
    {
        public string GetCpuInfo()
        {
            try
            {
                using var searcher = new ManagementObjectSearcher("select Name from Win32_Processor");
                var cpuNames = searcher.Get().Cast<ManagementObject>().Select(mo => mo["Name"].ToString().Trim()).ToList();
                return string.Join(", ", cpuNames);
            }
            catch (Exception ex)
            {
                return $"CPU Info Error: {ex.Message}";
            }
        }

        public string GetRamInfo()
        {
            try
            {
                using var searcher = new ManagementObjectSearcher("select TotalVisibleMemorySize, FreePhysicalMemory from Win32_OperatingSystem");
                var os = searcher.Get().Cast<ManagementObject>().FirstOrDefault();
                if (os != null)
                {
                    double totalKb = Convert.ToDouble(os["TotalVisibleMemorySize"]);
                    double freeKb = Convert.ToDouble(os["FreePhysicalMemory"]);
                    return $"Total: {Math.Round(totalKb / 1024 / 1024, 2)} GB | Free: {Math.Round(freeKb / 1024 / 1024, 2)} GB";
                }
                return "RAM Info Not Available";
            }
            catch (Exception ex)
            {
                return $"RAM Info Error: {ex.Message}";
            }
        }

        public string GetDiskInfo()
        {
            try
            {
                using var searcher = new ManagementObjectSearcher("select Size, FreeSpace, Name from Win32_LogicalDisk where DriveType=3");
                var sb = new StringBuilder();
                foreach (var disk in searcher.Get().Cast<ManagementObject>())
                {
                    var name = disk["Name"];
                    var size = Convert.ToDouble(disk["Size"]) / 1024 / 1024 / 1024;
                    var free = Convert.ToDouble(disk["FreeSpace"]) / 1024 / 1024 / 1024;
                    sb.AppendLine($"{name}: {Math.Round(free, 1)} GB free / {Math.Round(size, 1)} GB");
                }
                return sb.ToString().Trim();
            }
            catch (Exception ex)
            {
                return $"Disk Info Error: {ex.Message}";
            }
        }

        public string GetOperatingSystemInfo()
        {
            try
            {
                using var searcher = new ManagementObjectSearcher("select Caption, Version from Win32_OperatingSystem");
                var os = searcher.Get().Cast<ManagementObject>().FirstOrDefault();
                return os != null ? $"{os["Caption"]} (v{os["Version"]})" : "OS Info Not Available";
            }
            catch (Exception ex)
            {
                return $"OS Info Error: {ex.Message}";
            }
        }

        public string GetLastBootTime()
        {
            try
            {
                using var searcher = new ManagementObjectSearcher("SELECT LastBootUpTime FROM Win32_OperatingSystem");
                var mo = searcher.Get().Cast<ManagementObject>().FirstOrDefault();
                if (mo != null)
                {
                    var raw = mo["LastBootUpTime"].ToString();
                    var parsed = ManagementDateTimeConverter.ToDateTime(raw);
                    return parsed.ToString("g");
                }
                return "Boot Time Not Available";
            }
            catch (Exception ex)
            {
                return $"Boot Time Error: {ex.Message}";
            }
        }

        public List<string> GetInstalledApplications()
        {
            var apps = new List<string>();
            try
            {
                using var searcher = new ManagementObjectSearcher(
                    "SELECT Name, Version, InstallDate FROM Win32_Product");
                foreach (ManagementObject mo in searcher.Get())
                {
                    string name = mo["Name"]?.ToString() ?? "Unknown";
                    string version = mo["Version"]?.ToString() ?? "Unknown";
                    string date = mo["InstallDate"]?.ToString();
                    string dateFormatted = date != null && date.Length >= 8
                        ? $"{date.Substring(0, 4)}-{date.Substring(4, 2)}-{date.Substring(6, 2)}"
                        : "Unknown";
                    apps.Add($"{name}|{version}|{dateFormatted}");
                }
            }
            catch (Exception ex)
            {
                apps.Add($"App List Error: {ex.Message}|||");
            }
            return apps;
        }

        public string GetLatestWindowsUpdate()
        {
            try
            {
                using var searcher = new ManagementObjectSearcher(
                    "SELECT HotFixID, InstalledOn FROM Win32_QuickFixEngineering");
                DateTime latestDate = DateTime.MinValue;
                string latestId = null;
                foreach (ManagementObject mo in searcher.Get())
                {
                    string dateStr = mo["InstalledOn"]?.ToString();
                    if (DateTime.TryParse(dateStr, out var dt) && dt > latestDate)
                    {
                        latestDate = dt;
                        latestId = mo["HotFixID"]?.ToString();
                    }
                }
                return latestId != null ? $"{latestId} installed on {latestDate:g}" : "No updates found";
            }
            catch (Exception ex)
            {
                return $"Update Info Error: {ex.Message}";
            }
        }

        public string GetSerialNumber()
        {
            try
            {
                using var searcher = new ManagementObjectSearcher("SELECT SerialNumber FROM Win32_BIOS");
                var mo = searcher.Get().Cast<ManagementObject>().FirstOrDefault();
                return mo?["SerialNumber"]?.ToString() ?? "Unknown";
            }
            catch (Exception ex)
            {
                return $"Serial Number Error: {ex.Message}";
            }
        }

        public string GetFirewallProfile()
        {
            try
            {
                var process = new System.Diagnostics.Process
                {
                    StartInfo = new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = "netsh",
                        Arguments = "advfirewall show allprofiles state",
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
                };
                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                if (output.Contains("State ON"))
                    return "Windows Firewall: ON";
                if (output.Contains("State OFF"))
                    return "Windows Firewall: OFF";

                return "Firewall status unknown";
            }
            catch (Exception ex)
            {
                return $"Firewall Error: {ex.Message}";
            }
        }

        public string GetSystemName()
        {
            try
            {
                return Environment.MachineName;
            }
            catch (Exception ex)
            {
                return $"System Name Error: {ex.Message}";
            }
        }

        public string GetManufacturer()
        {
            try
            {
                using var searcher = new ManagementObjectSearcher("SELECT Manufacturer FROM Win32_ComputerSystem");
                foreach (ManagementObject obj in searcher.Get())
                    return obj["Manufacturer"]?.ToString() ?? "Unknown";
            }
            catch (Exception ex)
            {
                return $"Manufacturer Error: {ex.Message}";
            }
            return "Unknown";
        }

        public string GetModel()
        {
            try
            {
                using var searcher = new ManagementObjectSearcher("SELECT Model FROM Win32_ComputerSystem");
                foreach (ManagementObject obj in searcher.Get())
                    return obj["Model"]?.ToString() ?? "Unknown";
            }
            catch (Exception ex)
            {
                return $"Model Error: {ex.Message}";
            }
            return "Unknown";
        }
    }
}
