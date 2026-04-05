using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Application = System.Windows.Forms.Application;
using Button = System.Windows.Forms.Button;

namespace Ticket_Vendor_Machine
{
    public partial class RoutesForm : Form
    {
        public Form modeSelectionForm { get; set; }

        public RoutesForm(string routeType, Form prev)
        {
            InitializeComponent();

            label1.Text = $"Select a {routeType} route";
            modeSelectionForm = prev;

            LoadTestData();
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            modeSelectionForm.Show();
            this.Close();
        }

        Button selectedButton = null;

        // Call this in your Form_Load or a "InitializeData" method
        private void LoadTestData()
        {
            routesList.Controls.Clear();
            routesList.RowCount = 0;
            routesList.RowStyles.Clear();
            // MRT Routes (Blue)
            AddRoute("MRT-L1", "East-West Line", "Ben Thanh", "Suoi Tien", "25 mins", "15,000", Color.MidnightBlue);
            AddRoute("MRT-L2", "North-South Line", "Ben Thanh", "Tham Luong", "35 mins", "20,000", Color.MidnightBlue);

            // Bus Routes (Green)
            //AddRoute("BUS-01", "City Center Loop", "Ham Nghi", "Cho Lon", "45 mins", "7,000", Color.ForestGreen);
            //AddRoute("BUS-152", "Airport Link", "Trung Son", "Tan Son Nhat", "50 mins", "10,000", Color.ForestGreen);
            //AddRoute("BUS-19", "Student Line", "Ben Thanh", "National University", "60 mins", "7,000", Color.ForestGreen);
        }

        Button selectedRouteButton = null;
        string selectedRouteCode = "";
        void AddRoute(string code, string name, string origin, string destination, string duration, string price, Color themeColor)
        {
            Button btn = new Button();

            // FR1.2 Formatting: Code & Name | Origin -> Destination | Duration | Price
            btn.Text = $"{code}: {name}\n" +
                       $"{origin} → {destination}\n" +
                       $"{duration} | {price} VND";

            // Increase height to 120-140 to fit 3 lines of bold text comfortably
            btn.Height = 130;
            btn.Font = new Font("Segoe UI", 11, FontStyle.Bold); // Slightly smaller but Bold
            btn.TextAlign = ContentAlignment.MiddleCenter;

            // Kiosk Styling
            btn.Dock = DockStyle.Fill;
            btn.FlatStyle = FlatStyle.Flat;
            btn.BackColor = Color.White;
            btn.Margin = new Padding(10, 5, 25, 5);
            btn.Tag = themeColor; // Store for selection logic
            btn.FlatAppearance.BorderSize = 2;
            btn.FlatAppearance.BorderColor = themeColor;

            // Selection Logic (Single Selection)
            btn.Click += (s, e) => {
                if (selectedRouteButton != null)
                {
                    selectedRouteButton.BackColor = Color.White;
                    selectedRouteButton.ForeColor = Color.Black;
                }
                selectedRouteButton = btn;
                selectedRouteCode = code;
                btn.BackColor = (Color)btn.Tag;
                btn.ForeColor = Color.White;
            };

            // TableLayout Logic
            routesList.RowCount++;
            routesList.RowStyles.Add(new RowStyle(SizeType.Absolute, 140F)); // Row slightly taller than button
            routesList.Controls.Add(btn, 0, routesList.RowCount - 1);
        }

        private void RoutesForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
