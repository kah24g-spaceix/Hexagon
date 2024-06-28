using System;
using System.Collections.Generic;

public interface IView<TModel>
{
    void Bind(TModel model);
}
