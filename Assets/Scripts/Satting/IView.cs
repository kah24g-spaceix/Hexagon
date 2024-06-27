using System;
using System.Collections.Generic;

public interface IView<TModel>
{
    void Show();
    void Bind(TModel model);
    void Hide();
}
