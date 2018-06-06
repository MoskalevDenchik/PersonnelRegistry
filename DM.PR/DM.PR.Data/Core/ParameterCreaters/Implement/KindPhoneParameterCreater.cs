﻿using DM.PR.Data.Core.InputParameters.Creaters;
using DM.PR.Data.Specifications;
using System.Data.SqlClient;
using DM.PR.Common.Entities;
using DM.PR.Data.Entity;
using System;

namespace DM.PR.Data.Core.ParameterCreaters.Implement
{
    class KindPhoneParameterCreater : IParameterCreater<KindPhone>
    {
        public IInputParameter CreateForGetById(int id)
        {
            return new DbInputParameter
            {
                Procedure = "SelectKindPhoneById",
                Parameters = new SqlParameter[]
                {
                    new SqlParameter("@Id", id)
                }
            };
        }

        public IInputParameter CreateForGetAll()
        {
            return new DbInputParameter
            {
                Procedure = "SelectAllKindPhones",
                Parameters = null
            };
        }
        public IInputParameter CreateForFindBy(ISpecification specification)
        {
            return specification.GetSpecific();
        }

        public IInputParameter CreateForAdd(KindPhone item)
        {
            throw new NotImplementedException();
        }
        public IInputParameter CreateForUpdate(KindPhone item)
        {
            throw new NotImplementedException();
        }

        public IInputParameter CreateForRemove(int id)
        {
            throw new NotImplementedException();
        }

    }
}