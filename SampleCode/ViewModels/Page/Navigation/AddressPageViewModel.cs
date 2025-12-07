using CommunityToolkit.Mvvm.ComponentModel;
using SampleCode.Interfaces;
using SampleCode.ViewModels.Data.Navigation;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SampleCode.ViewModels.Page.Navigation
{
    public partial class AddressPageViewModel : PageViewModel, IPageViewModel<AddressViewModel>
    {
        [ObservableProperty]
        private ObservableCollection<AddressViewModel> _pageItemsList;

        [ObservableProperty]
        private ObservableCollection<StreetTypeViewModel> _streetTypes;

        [ObservableProperty]
        private ObservableCollection<SuburbViewModel> _suburbs;                      

        public AddressPageViewModel()
        {
            Debug.WriteLine("-- AddressPageViewModel Constructor--");
            PageItemsList = new ObservableCollection<AddressViewModel>();
            StreetTypes = new ObservableCollection<StreetTypeViewModel>();
            Suburbs = new ObservableCollection<SuburbViewModel>();                                   
        }

        public async Task LoadData()
        {
            Debug.WriteLine("-- LoadData --");
            
            PageItemsList.Clear();            
            PageItemsList = new ObservableCollection<AddressViewModel>(AddressViewModel.GetAll());
            Debug.WriteLine("Total Addresses found: " + PageItemsList.Count);            
            StreetTypes.Clear();
            StreetTypes = new ObservableCollection<StreetTypeViewModel>(StreetTypeViewModel.GetAll());
            
            Suburbs.Clear();
            Suburbs = new ObservableCollection<SuburbViewModel>(SuburbViewModel.GetAll());
        }        

        public async Task Add(AddressViewModel viewModel)
        {
            await viewModel.Add();
        }

        public async Task Update(AddressViewModel viewModel)
        {
            await viewModel.Update();
        }

        public Task Delete(AddressViewModel viewModel)
        {
            throw new System.NotImplementedException();
        }
    }
}