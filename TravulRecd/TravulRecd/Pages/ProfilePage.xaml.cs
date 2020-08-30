using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravulRecd.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravulRecd
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var postList = await Post.GetPostByUser();
            var categoryByCount = await Post.GetCategoryByCount();
            if (categoryByCount != null)
                categoryList.ItemsSource = categoryByCount;

            postCount.Text = postList.Posts.Count.ToString();
        }
    }
}