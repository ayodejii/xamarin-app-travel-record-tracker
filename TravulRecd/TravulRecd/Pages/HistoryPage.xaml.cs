using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravulRecd.Model;
using TravulRecd.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravulRecd
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        HistoryViewModel historyViewModel; 
        public HistoryPage()
        {
            InitializeComponent();
            historyViewModel = new HistoryViewModel();
            BindingContext = historyViewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await historyViewModel.UpdatePosts();
        }

        private void MenuItem_Clicked(object sender, EventArgs e)
        {
            var post = ((MenuItem)sender).CommandParameter as Post;
            historyViewModel.DeletePost(post);
        }

        private async void postListView_Refreshing(object sender, EventArgs e)
        {
            await historyViewModel.UpdatePosts();
            postListView.EndRefresh();
        }
    }
}