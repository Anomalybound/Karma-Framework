﻿using System.Threading.Tasks;

namespace Karma.UIStack
{
    public interface IWidget
    {
        Task OnShow();

        Task OnHide();

        Task OnResume();

        Task OnFreeze();

        void DestroyWidget();

        void SetManagerInfo(int id, string path, IUIStack manager, UIMessage message);
    }
}