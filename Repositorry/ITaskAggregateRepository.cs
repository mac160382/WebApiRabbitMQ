// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ITaskAggregateRepository.cs" company="GBM"> GBM GRUPO BURSÁTIL MEXICANO, S.A. DE C.V. CASA DE BOLSA. ©2017</copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;

using CallCenterModel;

namespace Repository
{
    public interface ITaskAggregateRepository
    {
        void Add(TaskAggregate taskAggregate);

        TaskAggregate Update(TaskAggregate taskAggregate);

        TaskAggregate Get(TimeSpan intervalTime);
    }
}