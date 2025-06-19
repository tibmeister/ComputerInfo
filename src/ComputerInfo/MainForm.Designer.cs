using System.Drawing;
using System.Windows.Forms;

namespace ComputerInfo
{
    partial class MainForm
    {
        private Label lblManufacturer, lblManufacturerVal;
        private Label lblModel, lblModelVal;
        private Label lblSystemName, lblSystemNameVal;
        private Label lblSerial, lblSerialVal;
        private Label lblOs, lblOsVal;
        private Label lblAdapter, lblAdapterVal;
        private Label lblIp, lblIpVal;
        private Label lblFirewall, lblFirewallVal;
        private Label lblCpu, lblCpuVal;
        private Label lblRam, lblRamVal;
        private Label lblDisk, lblDiskVal;
        private Label lblBoot, lblBootVal;
        private Label lblUpdate, lblUpdateVal;
        private ListView lstApps;
        private Button btnRefresh;
        private Label lblStatus;
        private CheckBox chkDarkMode;

        private void InitializeComponent()
        {
            this.lblManufacturer = new Label();
            this.lblManufacturerVal = new Label();
            this.lblModel = new Label();
            this.lblModelVal = new Label();
            this.lblSystemName = new Label();
            this.lblSystemNameVal = new Label();
            this.lblSerial = new Label();
            this.lblSerialVal = new Label();
            this.lblOs = new Label();
            this.lblOsVal = new Label();
            this.lblAdapter = new Label();
            this.lblAdapterVal = new Label();
            this.lblIp = new Label();
            this.lblIpVal = new Label();
            this.lblFirewall = new Label();
            this.lblFirewallVal = new Label();
            this.lblCpu = new Label();
            this.lblCpuVal = new Label();
            this.lblRam = new Label();
            this.lblRamVal = new Label();
            this.lblDisk = new Label();
            this.lblDiskVal = new Label();
            this.lblBoot = new Label();
            this.lblBootVal = new Label();
            this.lblUpdate = new Label();
            this.lblUpdateVal = new Label();
            this.lstApps = new ListView();
            this.btnRefresh = new Button();
            this.lblStatus = new Label();
            chkDarkMode = new CheckBox();
            chkDarkMode.Text = "Dark Mode";
            chkDarkMode.AutoSize = true;
            chkDarkMode.Location = new Point(500, btnRefresh.Top + 5);
            chkDarkMode.CheckedChanged += new System.EventHandler(this.chkDarkMode_CheckedChanged);
            this.Controls.Add(chkDarkMode);


            Font labelFont = new Font("Segoe UI", 9F, FontStyle.Bold);
            int y = 10;
            int spacing = 40;

            void AddLabelPair(Label lbl, Label val, string text)
            {
                lbl.AutoSize = true;
                lbl.Font = labelFont;
                lbl.Text = text;
                lbl.Location = new Point(10, y);

                val.AutoSize = true;
                val.Location = new Point(180, y);
                y += spacing;

                this.Controls.Add(lbl);
                this.Controls.Add(val);
            }

            AddLabelPair(lblManufacturer, lblManufacturerVal, "Manufacturer:");
            AddLabelPair(lblModel, lblModelVal, "Model:");
            AddLabelPair(lblSystemName, lblSystemNameVal, "System Name:");
            AddLabelPair(lblSerial, lblSerialVal, "Serial Number:");
            AddLabelPair(lblOs, lblOsVal, "OS:");
            AddLabelPair(lblAdapter, lblAdapterVal, "Active NIC:");
            AddLabelPair(lblIp, lblIpVal, "IP Address:");
            AddLabelPair(lblFirewall, lblFirewallVal, "Firewall Profile:");
            AddLabelPair(lblCpu, lblCpuVal, "CPU:");
            AddLabelPair(lblRam, lblRamVal, "RAM:");
            AddLabelPair(lblDisk, lblDiskVal, "Disk:");
            y += 10; // extra vertical gap before next row
            AddLabelPair(lblBoot, lblBootVal, "Last Boot:");
            AddLabelPair(lblUpdate, lblUpdateVal, "Latest Update:");

            Label lblApps = new Label
            {
                Font = labelFont,
                Text = "Installed Applications:",
                Location = new Point(10, y),
                AutoSize = true
            };
            this.Controls.Add(lblApps);
            y += 25;

            lstApps.View = View.Details;
            lstApps.FullRowSelect = true;
            lstApps.Location = new Point(10, y);
            lstApps.Size = new Size(590, 200);
            lstApps.Columns.Add("Name", 280);
            lstApps.Columns.Add("Version", 120);
            lstApps.Columns.Add("Install Date", 120);
            this.Controls.Add(lstApps);
            y += 210;

            btnRefresh.Text = "Refresh";
            btnRefresh.Location = new Point(10, y);
            btnRefresh.Size = new Size(100, 30);
            btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            this.Controls.Add(btnRefresh);

            lblStatus.Font = new Font("Segoe UI", 8F, FontStyle.Italic);
            lblStatus.Location = new Point(120, y + 5);
            lblStatus.Size = new Size(400, 30);
            lblStatus.Text = "";
            this.Controls.Add(lblStatus);

            this.ClientSize = new Size(620, y + 60);
            this.Text = "ComputerInfo";
        }
    }
}
