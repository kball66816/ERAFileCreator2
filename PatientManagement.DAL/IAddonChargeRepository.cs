using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PatientManagement.Model;

namespace PatientManagement.DAL
{
    public interface IAddonChargeRepository
    {
        void Add(AddonCharge charge);

        void Delete(AddonCharge charge);

        AddonCharge UpdateCharge(AddonCharge charge);

        ObservableCollection<AddonCharge> GetAllCharges();

        AddonCharge GetSelectedCharge(Guid id);

        IAdjustmentRepository AdjustmentRepository { get; set; }
    }
}
