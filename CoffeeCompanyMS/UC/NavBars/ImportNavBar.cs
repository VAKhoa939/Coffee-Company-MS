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
using CoffeeCompanyMS.UC.NavBars;
using CoffeeCompanyMS.UC.Pages.Import;
using CoffeeCompanyMS.Forms.Authentication;

namespace CoffeeCompanyMS.UC
{
    public partial class ImportNavBar : UserControl
    {
        private Button[] _buttons;
        private ImportOrdersPage _importOrderPage;
        private RecurringImportOrdersPage _recurringImportOrdersPage;

        public ImportNavBar()
        {
            InitializeComponent();
            _buttons = new Button[] {
                btnImportOrders,
                btnOrderDetails,
                btnRecurringOrders
            };
        }

        private void LoadPages()
        {
            // Check permissions before loading pages
            var user = UserSession.Instance.LoggedInUser;
            if (user.Location == null)
            {
                // If the user has no location (not a CompanyOwner or Admin),
                // disable the button to
                // Reccuring Orders page and Create Import Order form
                btnRecurringOrders.Enabled = false;
            }

            _importOrderPage = new ImportOrdersPage();
            _recurringImportOrdersPage = new RecurringImportOrdersPage();

            _importOrderPage.MoveToDetaisPage += DoubleClick_ToDetailsPage;
            _recurringImportOrdersPage.MoveToDetaisPage += DoubleClick_ToDetailsPage;
        }

        private void ImportNavBar_Load(object sender, EventArgs e)
        {
            LoadPages();
            NavigationManager.ShowPage(_importOrderPage);
        }

        private void btnImportOrders_Click(object sender, EventArgs e)
        {
            resetStyle();
            NavButtonStyleUtils.SetChosenButtonStyle(btnImportOrders);
            NavigationManager.ShowPage(_importOrderPage);
        }

        private void btnRecurringOrders_Click(object sender, EventArgs e)
        {
            resetStyle();
            NavButtonStyleUtils.SetChosenButtonStyle(btnRecurringOrders);
            NavigationManager.ShowPage(_recurringImportOrdersPage);
        }

        private void resetStyle()
        {
            NavButtonStyleUtils.ResetButtonsStyle(_buttons);
        }

        private void pbReload_Click(object sender, EventArgs e)
        {
            LoadPages();
        }

        private void DoubleClick_ToDetailsPage(Guid orderID)
        {
            resetStyle();
            NavButtonStyleUtils.SetChosenButtonStyle(btnOrderDetails);
            NavigationManager.ShowPage(new ImportOrderDetailsPage(orderID));
        }
    }
}
