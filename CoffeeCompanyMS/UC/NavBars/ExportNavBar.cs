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
using CoffeeCompanyMS.UC.Pages.Export;
using CoffeeCompanyMS.UC.Pages.Import;
using CoffeeCompanyMS.UC.Pages.Storage;

namespace CoffeeCompanyMS.UC
{
    public partial class ExportNavBar : UserControl
    {
        private Button[] _buttons;
        private ExportOrdersPage _exportOrderPages;
        private RecurringExportOrdersPage _recurringExportOrdersPage;

        public ExportNavBar()
        {
            InitializeComponent();
            _buttons = new Button[] {
                btnExportOrders,
                btnOrderDetails,
                btnRecurringOrders
            };
        }

        private void LoadPages()
        {
            _exportOrderPages = new ExportOrdersPage();
            _recurringExportOrdersPage = new RecurringExportOrdersPage();

            _exportOrderPages.MoveToDetaisPage += DoubleClick_ToDetailsPage;
            _recurringExportOrdersPage.MoveToDetaisPage += DoubleClick_ToDetailsPage;
        }

        private void ExportNavBar_Load(object sender, EventArgs e)
        {
            LoadPages();
            NavigationManager.ShowPage(_exportOrderPages);
        }

        private void btnExportOrders_Click(object sender, EventArgs e)
        {
            resetStyle();
            NavButtonStyleUtils.SetChosenButtonStyle(btnExportOrders);
            NavigationManager.ShowPage(_exportOrderPages);
        }

        private void btnRecurringOrders_Click(object sender, EventArgs e)
        {
            resetStyle();
            NavButtonStyleUtils.SetChosenButtonStyle(btnRecurringOrders);
            NavigationManager.ShowPage(_recurringExportOrdersPage);
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
            NavigationManager.ShowPage(new ExportOrderDetailsPage(orderID));
        }
    }
}
