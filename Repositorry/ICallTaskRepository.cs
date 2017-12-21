using CallCenterModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public interface ICallTaskRepository
    {
        void Add(Task task);

        Task Update(Task task);
    }
}
