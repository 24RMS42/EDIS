using System;
using System.Threading.Tasks;
using EDIS.Core.Exceptions;
using EDIS.Domain.Circuits;
using EDIS.Service.Interfaces;
using EDIS.Shared.Helpers;
using EDIS.Shared.Pages.Boards.Circuits;
using EDIS.Shared.ViewModels.Base;
using Xamarin.Forms;

namespace EDIS.Shared.ViewModels.Circuits
{
    public class MainCircuitDataViewModel : BaseViewModel
    {
        private string _circuitId;

        private readonly IBoardsService _boardsService;

        public CircuitDetailsViewModel CircuitViewModel { get; set; }
        public CircuitTestDetailsViewModel CircuitTestViewModel { get; set; }

        public override async Task OnAppearing()
        {
            await CircuitTestViewModel.OnAppearing();
            await CircuitViewModel.OnAppearing();
        }

        public override async Task InitializeAsync(object navigationData)
        {
            MessagingCenter.Subscribe<CircuitDetailsPage, string>(this, MessengerCenterMessages.ChangeTitleOnCircuitTab, (circuit, s) =>
            {
                Title = s;
            });
            MessagingCenter.Subscribe<CircuitTestDetailsPage, string>(this, MessengerCenterMessages.ChangeTitleOnCircuitTab, (circuit, s) =>
            {
                Title = s;
            });

            var circuitId = navigationData as string;
            if (circuitId != null)
                _circuitId = circuitId;

            try
            {
                using (var dlg = this.Dialogs.Loading("Progress (No Cancel)"))
                {
                    var circuitResult = await _boardsService.GetTestedCircuit(_circuitId);
                    if (!circuitResult.Success)
                    {
                        Dialogs.Toast(circuitResult.Message);
                        return;
                    }

                    var circuit = circuitResult.ResultObject as CircuitTest;
                    if (circuit != null)
                    {
                        CircuitViewModel.Circuit = circuit;
                        CircuitTestViewModel.Circuit = circuit;

                        await CircuitTestViewModel.InitializeAsync(null);
                        await CircuitViewModel.InitializeAsync(null);
                    }
                }
            }
            catch (ServiceAuthenticationException e)
            {
                var result = await TryToLogin();
                if (!result)
                    await NavigationService.NavigateToAsync<LoginViewModel>();
                else
                    await InitializeAsync(_circuitId);
            }
            catch (Exception e)
            {
                await ShowErrorAlert(e?.Message);
            }
        }

        public MainCircuitDataViewModel(IBoardsService boardsService, IEstatesLookupsService lookupsService, ILookupsService lkpService)
        {
            Title = "Circuit Details - Circuit Test";

            _boardsService = boardsService;

            CircuitViewModel = new CircuitDetailsViewModel(boardsService, lkpService, lookupsService);
            CircuitTestViewModel = new CircuitTestDetailsViewModel(boardsService, lookupsService);
        }
    }
}