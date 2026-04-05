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
    public partial class TicketForm : Form
    {
        public string routeType { get; set; }
        public Form modeSelectionForm { get; set; }
        public TicketForm(Routes selectedRoute, Form prev)
        {
            InitializeComponent();

            modeSelectionForm = prev;

            InitializeTicketTable(selectedRoute, "1A");

            //LoadDataToKiosk();
        }

        //private void LoadDataToKiosk()
        //{
        //    List<Routes> allData = _routeService.GetAllRoutes();

        //    routesList.Controls.Clear();
        //    routesList.RowCount = 0;
        //    routesList.RowStyles.Clear();

        //    foreach (Routes r in allData)
        //    {
        //        Color theme = (r.RouteType == "MRT") ? Color.MidnightBlue : Color.ForestGreen;

        //        // Use the plural class properties
        //        if ( (r.RouteType == "MRT") && (routeType == "metro") )
        //        {
        //            AddRoute(r.RouteID, r.RouteCode, r.RouteName, r.OriginStation,
        //                 r.DestinationStation, r.EstimatedDuration,
        //                 r.TicketPrice.ToString("N0"), theme);

        //            continue;
        //        }
        //        else if ( (r.RouteType == "BUS") && (routeType == "bus") )
        //        {
        //            AddRoute(r.RouteID, r.RouteCode, r.RouteName, r.OriginStation,
        //                 r.DestinationStation, r.EstimatedDuration,
        //                 r.TicketPrice.ToString("N0"), theme);
        //        }
        //    }
        //}

        private void backButton_Click(object sender, EventArgs e)
        {
            modeSelectionForm.Show();
            this.Close();
        }

        private void InitializeTicketTable(Routes route, string ticketId)
        {
            // 1. Setup the Table Appearance
            tlpTicket.Controls.Clear();
            tlpTicket.RowCount = 6;
            tlpTicket.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single; // Creates the grid lines
            tlpTicket.BackColor = Color.White;

            // 2. Define the Data Rows
            string[,] ticketRows = {
        { "Ticket ID", ticketId },
        { "Route Code", route.RouteCode },
        { "From", route.OriginStation },
        { "To", route.DestinationStation },
        { "Duration", route.EstimatedDuration },
        { "Total Price", $"{route.TicketPrice:N0} VND" }
    };

            // 3. Loop and add Labels to the Table
            for (int i = 0; i < ticketRows.GetLength(0); i++)
            {
                // Left Column (Labels)
                Label lblKey = new Label
                {
                    Text = ticketRows[i, 0],
                    Font = new Font("Segoe UI", 10, FontStyle.Regular),
                    ForeColor = Color.Gray,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Dock = DockStyle.Fill,
                    Margin = new Padding(5)
                };

                // Right Column (Values)
                Label lblValue = new Label
                {
                    Text = ticketRows[i, 1],
                    Font = new Font("Segoe UI", 11, FontStyle.Bold),
                    ForeColor = Color.Black,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Dock = DockStyle.Fill,
                    Margin = new Padding(5)
                };

                tlpTicket.Controls.Add(lblKey, 0, i);
                tlpTicket.Controls.Add(lblValue, 1, i);
            }

            //// 4. Update Header Color based on Route Type
            //pnlTicketHeader.BackColor = (route.RouteType == "MRT") ? Color.MidnightBlue : Color.ForestGreen;
            //lblHeaderTitle.ForeColor = Color.White;
        }

        // Call this in your Form_Load or a "InitializeData" method
        //private void LoadTestData()
        //{
        //    routesList.Controls.Clear();
        //    routesList.RowCount = 0;
        //    routesList.RowStyles.Clear();
        //    // MRT Routes (Blue)
        //    //AddRoute("MRT-L1", "East-West Line", "Ben Thanh", "Suoi Tien", "25 mins", "15,000", Color.MidnightBlue);
        //    //AddRoute("MRT-L2", "North-South Line", "Ben Thanh", "Tham Luong", "35 mins", "20,000", Color.MidnightBlue);

        //    // Bus Routes (Green)
        //    //AddRoute("BUS-01", "City Center Loop", "Ham Nghi", "Cho Lon", "45 mins", "7,000", Color.ForestGreen);
        //    //AddRoute("BUS-152", "Airport Link", "Trung Son", "Tan Son Nhat", "50 mins", "10,000", Color.ForestGreen);
        //    //AddRoute("BUS-19", "Student Line", "Ben Thanh", "National University", "60 mins", "7,000", Color.ForestGreen);
        //}

        //Button selectedRouteButton = null;
        //string selectedRouteCode = "";
        //void AddRoute(int routeID, string code, string name, string origin, string destination, string duration, string price, Color themeColor)
        //{
        //    Button btn = new Button();

        //    // FR1.2 Formatting: Code & Name | Origin -> Destination | Duration | Price
        //    btn.Text = $"{code}: {name}\n" +
        //               $"{origin} → {destination}\n" +
        //               $"{duration} | {price} VND";

        //    // Increase height to 120-140 to fit 3 lines of bold text comfortably
        //    btn.Height = 130;
        //    btn.Font = new Font("Segoe UI", 11, FontStyle.Bold); // Slightly smaller but Bold
        //    btn.TextAlign = ContentAlignment.MiddleCenter;

        //    // Kiosk Styling
        //    btn.Dock = DockStyle.Fill;
        //    btn.FlatStyle = FlatStyle.Flat;
        //    btn.BackColor = Color.White;
        //    btn.Margin = new Padding(10, 5, 25, 5);
        //    btn.Tag = themeColor; // Store for selection logic
        //    btn.FlatAppearance.BorderSize = 2;
        //    btn.FlatAppearance.BorderColor = themeColor;

        //    // Selection Logic (Single Selection)
        //    btn.Click += (s, e) => {
        //        if (selectedRouteButton != null)
        //        {
        //            selectedRouteButton.BackColor = Color.White;
        //            selectedRouteButton.ForeColor = Color.Black;
        //        }
        //        selectedRouteButton = btn;
        //        selectedRouteCode = code;
        //        btn.BackColor = (Color)btn.Tag;
        //        btn.ForeColor = Color.White;
        //    };

        //    // TableLayout Logic
        //    routesList.RowCount++;
        //    routesList.RowStyles.Add(new RowStyle(SizeType.Absolute, 140F)); // Row slightly taller than button
        //    routesList.Controls.Add(btn, 0, routesList.RowCount - 1);
        //}

    }
}
