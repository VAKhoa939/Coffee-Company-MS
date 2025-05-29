using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CoffeeCompanyMS.Forms.Authentication;
using CoffeeCompanyMS.Navigations;
using CoffeeCompanyMS.UC.Pages.Export;
using CoffeeCompanyMS.UC.Pages.Import;
using CoffeeCompanyMS.UC.Pages.Information;
using CoffeeCompanyMS.UC.Pages.Storage;

namespace CoffeeCompanyMS.UC
{
    public partial class CompanyOwnerNavBar : UserControl
    {
        public CompanyOwnerNavBar()
        {
            InitializeComponent();
        }

        private void CompanyOwnerNavBar_Load(object sender, EventArgs e)
        {
            NavigationManager.ShowSubNav(new StorageNavBar());
        }

        private void pbStorage_Click(object sender, EventArgs e)
        {
            if (lbMode.Text == "STORAGE") return;
            lbMode.Text = "STORAGE";
            NavigationManager.ShowSubNav(new StorageNavBar());
        }

        private void pbImport_Click(object sender, EventArgs e)
        {
            if (lbMode.Text == "IMPORT") return;
            lbMode.Text = "IMPORT";
            NavigationManager.ShowSubNav(new ImportNavBar());
        }

        private void pbExport_Click(object sender, EventArgs e)
        {
            if (lbMode.Text == "EXPORT") return;
            lbMode.Text = "EXPORT";
            NavigationManager.ShowSubNav(new ExportNavBar());
        }

        private void pbInformation_Click(object sender, EventArgs e)
        {
            if (lbMode.Text == "INFORMATION") return;
            lbMode.Text = "INFORMATION";
            NavigationManager.ShowSubNav(new InformationNavBar());
        }

        private void pbLogOut_Click(object sender, EventArgs e)
        {
            UserSession.Instance.End();
            Program.loginForm.Show();
            Program.mainForm.Hide();
        }
    }
}
