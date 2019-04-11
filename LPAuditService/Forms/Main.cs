using LPAuditService.Bussisness;
using LPAuditService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LPAuditService
{

    public partial class MainForm : Form
    {
        private LayoutProcessContext db = new LayoutProcessContext();
        

        public MainForm()
        {
            InitializeComponent();
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            await TestConnection();
        }

        public async Task TestConnection()
        {
            try
            {
                await db.Database.Connection.OpenAsync();
                db.Database.Connection.Close();
                lblConnectionStatus.Text = "Connected to the DataBase";
                lblConnectionStatus.ForeColor = Color.Green;
                lblDbErrorDescription.Text = "";
            }
            catch (Exception e)
            {
                lblConnectionStatus.Text = "Disconnected from the DataBase";
                lblConnectionStatus.ForeColor = Color.Red;

                lblDbErrorDescription.Text = e.ToString();
            }
        }

        private async void timerTestConnection_Tick(object sender, EventArgs e)
        {
            await TestConnection();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                AuditsSearch auditsSearch = new AuditsSearch();
                var hilo = new Thread(new ThreadStart(() => auditsSearch.BuscarAuditoriasSinEventos()));
                hilo.Start();
            }
            catch (Exception ex)
            {
                lblDbErrorDescription.Text = ex.ToString();
            }
        }
    }
}
