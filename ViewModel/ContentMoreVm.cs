using Steam.Messages;
using Steam.Models;
using Steam.Service;
using Steam.Service.Base;
using Steam.ViewModel.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Steam.ViewModel;

public class ContentMoreVm : ViewModelBase
{
    private IMessenger messenger;
    private bool AllreadyLike = false;
    private bool AllreadyDisLike = false;
    private bool LikeBool = false;
    private bool DisLikeBool = false;

    private User currentUser { get; set; }
    private Content currentContent { get; set; }

    private string title;
    public string Title
    {
        get => title;
        set => base.PropertyChange(out title, value);
    }

    private string desc;
    public string Desc
    {
        get => desc;
        set => base.PropertyChange(out desc, value);
    }

    private int like;
    public int Like
    {
        get => like;
        set => base.PropertyChange(out like, value);
    }

    private int dislike;
    public int Dislike
    {
        get => dislike;
        set => base.PropertyChange(out dislike, value);
    }

    private Command likec;
    public Command LikeC
    {
        get => new Command(() => LikeCommand());
        set => base.PropertyChange(out likec, value);
    }

    private void LikeCommand()
    {
        if (AllreadyLike)
        {
            MessageBox.Show("All Ready Liked");
            return;
        }
        if (DisLikeBool)
        {
            AllreadyLike = true;
            AllreadyDisLike = false;
            currentContent.Like += 1;
            currentContent.Dislike -= 1;
            LikeBool = true;
            DisLikeBool = false;
            App.ServiceContainer.GetInstance<EntityFramework>().SaveChanges();
            Update();
            return;
        }
        AllreadyLike = true;
        AllreadyDisLike = false;
        currentContent.Like += 1;
        LikeBool = true;
        DisLikeBool = false;
        Update();
        App.ServiceContainer.GetInstance<EntityFramework>().SaveChanges();
    }

    private Command dislikec;
    public Command Dislikec
    {
        get => new Command(() => DislikeCommand());
        set => base.PropertyChange(out likec, value);
    }

    private void DislikeCommand()
    {
        if (AllreadyDisLike)
        {
            MessageBox.Show("All Ready DisLiked");
            return;
        }
        if (LikeBool)
        {
            AllreadyDisLike = true;
            AllreadyLike = false;
            currentContent.Like -= 1;
            currentContent.Dislike += 1;
            DisLikeBool = true;
            LikeBool = false;
            App.ServiceContainer.GetInstance<EntityFramework>().SaveChanges();
            Update();
            return;
        }
        AllreadyDisLike = true;
        AllreadyLike = false;
        currentContent.Dislike += 1;
        DisLikeBool = true;
        LikeBool = false;
        Update();
        App.ServiceContainer.GetInstance<EntityFramework>().SaveChanges();
    }

    private void Update()
    {
        
        Title = currentContent.Title;
        Desc = currentContent.Desc;
        Like = currentContent.Like;
        Dislike = currentContent.Dislike;
    }

    public ContentMoreVm(IMessenger messenger)
    {
        this.messenger = messenger;
        messenger.Subscribe<GetCurrentUser>((message) =>
        {
            if (message is GetCurrentUser user)
            {
                currentUser = user.User;
            }
        });
        messenger.Subscribe<GetCurrentContent>((message) =>
        {
            if (message is GetCurrentContent content)
            {
                currentContent = content.content;
                Update();
            }
        });

    }
}
