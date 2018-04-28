using System.ComponentModel;
using System.Windows.Input;
using Common.Common.Services;
using PatientManagement.Model;
using PatientManagement.ViewModel.Service.Messaging;
using PatientManagement.ViewModel.Services;

namespace PatientManagement.ViewModel
{
    public class AddonChargeViewModel
    {

        //public AddonChargeViewModel()
        //{
        //    this.SelectedAddonCharge = AddonChargeService.GetNewAddonCharge();


        //}

        //private void OnAdjustmentReceived(Adjustment adjustment)
        //{
        //    AddonChargeService.AssociateAdjustmentToCharge(this.SelectedAddonCharge, adjustment);
        //}

        //public event PropertyChangedEventHandler PropertyChanged;



        //private void RaisePropertyChanged(string propertyName)
        //{
        //    this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}

        //private bool CanAddAddon(object obj)
        //{
        //    return !string.IsNullOrEmpty(this.SelectedAddonCharge.ProcedureCode)
        //    && (this.SelectedAddonCharge.ChargeCost > 0);
        //}
    }
}