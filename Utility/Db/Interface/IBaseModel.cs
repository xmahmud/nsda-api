using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.Db.Interface {
    public interface IBaseModel {
        void OnUpdate();
        void OnCreate();
    }
}
