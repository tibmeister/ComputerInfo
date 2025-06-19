using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputerInfo
{
    public partial class MainForm : Form
    {
        private readonly SystemInfoService _systemInfoService = new();
        private readonly NetworkInfoService _networkInfoService = new();
        private int sortColumn = -1;
        private SortOrder sortOrder = SortOrder.None;

        public MainForm()
        {
            InitializeComponent();

            lstApps.ColumnClick += new ColumnClickEventHandler(lstApps_ColumnClick);

            InitializePlaceholders();
        }

        protected override async void OnShown(EventArgs e)
        {
            base.OnShown(e);
            lblStatus.Text = "Loading data, please wait...";
            await LoadAllInfoAsync();
            lblStatus.Text = "Load complete.";
        }

        private void InitializePlaceholders()
        {
            lblManufacturerVal.Text = "Loading...";
            lblModelVal.Text = "Loading...";
            lblSystemNameVal.Text = "Loading...";
            lblSerialVal.Text = "Loading...";
            lblOsVal.Text = "Loading...";
            lblAdapterVal.Text = "Loading...";
            lblIpVal.Text = "Loading...";
            lblFirewallVal.Text = "Loading...";
            lblCpuVal.Text = "Loading...";
            lblRamVal.Text = "Loading...";
            lblDiskVal.Text = "Loading...";
            lblBootVal.Text = "Loading...";
            lblUpdateVal.Text = "Loading...";
            lstApps.Items.Clear();
            lblStatus.Text = "";
        }

        private Task LoadAllInfoAsync()
        {
            return Task.Run(() =>
            {
                var manufacturer = _systemInfoService.GetManufacturer();
                var model = _systemInfoService.GetModel();
                var systemName = _systemInfoService.GetSystemName();
                var serial = _systemInfoService.GetSerialNumber();
                var os = _systemInfoService.GetOperatingSystemInfo();
                var adapter = _networkInfoService.GetNetworkAdapterInfo();
                var ip = _networkInfoService.GetIpAddress();
                var firewall = _systemInfoService.GetFirewallProfile();
                var cpu = _systemInfoService.GetCpuInfo();
                var ram = _systemInfoService.GetRamInfo();
                var disk = _systemInfoService.GetDiskInfo();
                var boot = _systemInfoService.GetLastBootTime();
                var update = _systemInfoService.GetLatestWindowsUpdate();
                var apps = _systemInfoService.GetInstalledApplications();

                Invoke(new Action(() =>
                {
                    lblManufacturerVal.Text = manufacturer;
                    lblModelVal.Text = model;
                    lblSystemNameVal.Text = systemName;
                    lblSerialVal.Text = serial;
                    lblOsVal.Text = os;
                    lblAdapterVal.Text = adapter;
                    lblIpVal.Text = ip;
                    lblFirewallVal.Text = firewall;
                    lblCpuVal.Text = cpu;
                    lblRamVal.Text = ram;
                    lblDiskVal.Text = disk;
                    lblBootVal.Text = boot;
                    lblUpdateVal.Text = update;

                    lstApps.Items.Clear();
                    foreach (var app in apps)
                    {
                        var parts = app.Split('|');
                        if (parts.Length == 3)
                            lstApps.Items.Add(new ListViewItem(parts));
                    }
                }));
            });
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "Refreshing data, please wait...";
            _ = LoadAllInfoAsync().ContinueWith(t => Invoke(() => lblStatus.Text = "Refresh complete."));
        }

        private void lstApps_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == sortColumn)
            {
                sortOrder = (sortOrder == SortOrder.Ascending) ? SortOrder.Descending : SortOrder.Ascending;
            }
            else
            {
                sortColumn = e.Column;
                sortOrder = SortOrder.Ascending;
            }

            lstApps.ListViewItemSorter = new ListViewItemComparer(e.Column, sortOrder);
        }
    }

    class ListViewItemComparer : System.Collections.IComparer
    {
        private int col;
        private SortOrder order;

        public ListViewItemComparer(int column, SortOrder sortOrder)
        {
            col = column;
            order = sortOrder;
        }

        public int Compare(object x, object y)
        {
            string itemX = ((ListViewItem)x).SubItems[col].Text;
            string itemY = ((ListViewItem)y).SubItems[col].Text;

            int result = string.Compare(itemX, itemY);
            return order == SortOrder.Ascending ? result : -result;
        }
    }

}
