using DM.AdvertisingService.Data.Repositories;
using DM.AdvertisingService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DM.AdvertisingService.Business.Services
{
    internal class BillBoardService
    {
        private readonly BiilBoardRepositoy _bBRepository;
        public BillBoardService()
        {
            _bBRepository = new BiilBoardRepositoy();
        }
        public List<BillBoard> GetRandomBillBoards()
        {
            var bBList = _bBRepository.GetAll().Take(2).ToList();
            //var randomNumberArray = GetRandomNumber(4, bBList.Count);
            //var resultList = new List<BillBoard>();

            //for (int i = 0; i < randomNumberArray.Count(); i++)
            //{
            //    if (randomNumberArray.Contains(i))
            //    {
            //        resultList.Add(bBList[i]);
            //    }
            //}

            return bBList;
        }

        private int[] GetRandomNumber(int size, int maxValue)
        {
            if (maxValue > size)
            {
                throw new Exception();
            }

            Random random = new Random();

            var array = new int[size];

            for (int i = 0; i < size; i++)
            {
                array[i] = random.Next(maxValue);
            }
            return array;
        }
    }
}