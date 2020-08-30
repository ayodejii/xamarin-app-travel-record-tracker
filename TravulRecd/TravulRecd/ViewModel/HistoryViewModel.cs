using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravulRecd.Model;
using TravulRecd.ViewModel.Command;

namespace TravulRecd.ViewModel
{
    public class HistoryViewModel
    {
        public ObservableCollection<Post> Posts { get; set; }
        public RefreshPostCommand RefreshPostCmd { get; set; }

        public HistoryViewModel()
        {
            Posts = new ObservableCollection<Post>();
            RefreshPostCmd = new RefreshPostCommand(this); 
        }

        public async Task UpdatePosts()
        {
            var postList = (await Post.GetPostByUser()).Posts.Distinct();
            if (postList is null) return;
            Posts.Clear();
            foreach (var post in postList)
                Posts.Add(post);
        }

        internal async void RefreshPosts()
        {
            //await UpdatePosts();
            //App.Current.
        }

        public async void DeletePost(Post post)
        {
            bool toDelete = await App.Current.MainPage.DisplayAlert("Delete", "Delete Post?", "Yes", "No");
            if (!toDelete)
                return;
            bool status = await Post.DeletePost(post);
            await UpdatePosts();
        }

    }
}
