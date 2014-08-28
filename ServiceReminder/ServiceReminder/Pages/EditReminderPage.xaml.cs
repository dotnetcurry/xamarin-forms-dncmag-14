using ServiceReminder.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ServiceReminder.Pages
{

	public partial class EditReminderPage 
	{

        EditReminderPageViewModel vm;
		public EditReminderPage ()
		{
			InitializeComponent ();

            vm = new EditReminderPageViewModel();
            // Sometimes Commands in ViewModels will require to navigate.
            vm.Navigation = Navigation;

            this.BindingContext = vm;

            SetBinding(Page.TitleProperty, new Binding(EditReminderPageViewModel.TitlePropertyName));
            SetBinding(Page.IconProperty, new Binding(EditReminderPageViewModel.IconPropertyName));

		}

        private Action Save()
        {
            return async () => {
                var result = await vm.Save();
                if (!result)
                    await DisplayAlert("Error", "Name and Reg No are required.", "OK", null);
                else
                    await Navigation.PopAsync();

            };
        }

        ToolbarItem toolbarItem;
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ToolbarItems.Add(toolbarItem = new ToolbarItem("Save", null, Save(), 0, 0));

            vm.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            ToolbarItems.Remove(toolbarItem);
        }


	}
}
