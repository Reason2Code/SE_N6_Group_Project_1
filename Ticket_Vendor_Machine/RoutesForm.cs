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
using DAL;
using BLL;

namespace Ticket_Vendor_Machine
{
    public partial class RoutesForm : Form
    {
        public string routeType { get; set; }
        public Form modeSelectionForm { get; set; }
        public RouteService _routeService { get; set; }
        public RoutesForm(string routeType, Form prev)
        {
            InitializeComponent();

            label1.Text = $"Select a {routeType} route";
            this.routeType = routeType;
            modeSelectionForm = prev;
            _routeService = new RouteService();

            LoadDataToKiosk();
        }

        private void LoadDataToKiosk()
        {
            List<Routes> allData = _routeService.GetAllRoutes();

            routesList.Controls.Clear();
            routesList.RowCount = 0;
            routesList.RowStyles.Clear();

            foreach (Routes r in allData)
            {
                Color theme = (r.RouteType == "MRT") ? Color.MidnightBlue : Color.ForestGreen;

                // Use the plural class properties
                if ( (r.RouteType == "MRT") && (routeType == "metro") )
                {
                    AddRoute(r, theme);

                    continue;
                }
                else if ( (r.RouteType == "BUS") && (routeType == "bus") )
                {
                    AddRoute(r, theme);
                }
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            modeSelectionForm.Show();
            this.Close();
        }

        // Call this in your Form_Load or a "InitializeData" method
        private void LoadTestData()
        {
            routesList.Controls.Clear();
            routesList.RowCount = 0;
            routesList.RowStyles.Clear();
            // MRT Routes (Blue)
            //AddRoute("MRT-L1", "East-West Line", "Ben Thanh", "Suoi Tien", "25 mins", "15,000", Color.MidnightBlue);
            //AddRoute("MRT-L2", "North-South Line", "Ben Thanh", "Tham Luong", "35 mins", "20,000", Color.MidnightBlue);

            // Bus Routes (Green)
            //AddRoute("BUS-01", "City Center Loop", "Ham Nghi", "Cho Lon", "45 mins", "7,000", Color.ForestGreen);
            //AddRoute("BUS-152", "Airport Link", "Trung Son", "Tan Son Nhat", "50 mins", "10,000", Color.ForestGreen);
            //AddRoute("BUS-19", "Student Line", "Ben Thanh", "National University", "60 mins", "7,000", Color.ForestGreen);
        }

        Button selectedRouteButton = null;
        public Routes selectedRoute { get; set; }
        void AddRoute(Routes routeData, Color themeColor)
        {
            Button btn = new Button();

            // Formatting using the object properties
            btn.Text = $"{routeData.RouteCode}: {routeData.RouteName}\n" +
                       $"{routeData.OriginStation} → {routeData.DestinationStation}\n" +
                       $"{routeData.TicketPrice:N0} VND";

            btn.Tag = routeData; // THIS IS KEY: We hide the whole DB row inside the button

            // Styling
            btn.Height = 130;
            btn.Dock = DockStyle.Fill;
            btn.BackColor = Color.White;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = themeColor;
            btn.FlatAppearance.BorderSize = 2;
            btn.Font = new Font("Segoe UI", 11, FontStyle.Bold);

            // Selection Logic
            btn.Click += (s, e) => {
                // Reset previous selection
                if (selectedRouteButton != null)
                {
                    selectedRouteButton.BackColor = Color.White;
                    selectedRouteButton.ForeColor = Color.Black;
                }

                // Highlight new selection
                selectedRouteButton = btn;
                this.selectedRoute = (Routes)btn.Tag; // Record the selection

                btn.BackColor = themeColor;
                btn.ForeColor = Color.White;
            };

            routesList.RowCount++;
            routesList.RowStyles.Add(new RowStyle(SizeType.Absolute, 140F));
            routesList.Controls.Add(btn, 0, routesList.RowCount - 1);
        }

        private void RoutesForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void proceedButton_Click(object sender, EventArgs e)
        {
            if (selectedRoute == null)
            {
                MessageBox.Show("Please select a route first!");
                return;
            }

            // Open Payment Form and pass the selected data
            PaymentForm payment = new PaymentForm(selectedRoute, this);
            payment.Show();
            this.Hide();
        }
    }
}
