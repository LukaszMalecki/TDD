﻿namespace Uqs.Weather.Wrappers
{
    public class RandomWrapper : IRandomWrapper
    {
        private readonly Random _random = Random.Shared;
        public int Next(int minValue, int maxValue) 
            => _random.Next(minValue, maxValue);
    }
}
