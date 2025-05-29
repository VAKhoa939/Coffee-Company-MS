using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoffeeCompanyMS.Navigations;
using CoffeeCompanyMS.UC;
using CoffeeCompanyMS.Models;
using CoffeeCompanyMS.Forms.Authentication;

namespace CoffeeCompanyMS.UI
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        
        private void Main_Load(object sender, EventArgs e)
        {
            User user = UserSession.Instance.LoggedInUser;

            if (user == null) return;

            NavigationManager.Initialize(this.subNavPanel, this.pageAreaPanel);

            UserControl mainNav;

            switch (user.Role)
            {
                case "WarehouseManager":
                    mainNav = new WarehouseNavBar();
                    break;
                case "CompanyOwner":
                case "Admin":
                    mainNav = new CompanyOwnerNavBar();
                    break;
                default:
                    throw new Exception("Page Not Found");
            }
            mainNav.Dock = DockStyle.Fill;
            navBarPanel.Controls.Add(mainNav);
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            
            Application.Exit();
        }
    }
}
