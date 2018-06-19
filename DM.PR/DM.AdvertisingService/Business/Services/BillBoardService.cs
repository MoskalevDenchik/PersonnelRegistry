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
            var bBList = _bBRepository.GetAll();

            var randomNumberArray = GetRandomNumber(2, bBList.Count);

            int i = -1;
            return bBList.FindAll(x => { i++; return randomNumberArray.Contains(i) ? true : false; }).Select(x => x).ToList();
        }

        #region Helpers

        private int[] GetRandomNumber(int size, int maxValue)
        {
            if (size > maxValue)
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

        #endregion
    }
}