// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TaskAggregate.cs" company="GBM"> GBM GRUPO BURSÁTIL MEXICANO, S.A. DE C.V. CASA DE BOLSA. ©2017</copyright>
//  --------------------------------------------------------------------------------------------------------------------

using System;

namespace CallCenterModel
{
    public class TaskAggregate
    {
        public Guid? _id { get; set; }

        //
        public TimeSpan Interval { get; set; }

        //NCO
        public int Quantity { get; set; }

        //NCH
        public int Resolved { get; set; }

        //AHT
        public int Erlang { get; set; }

        //SVL
        public decimal LevelService { get; set; }

        public Guid CorrelationId { get; set; }
    }
}