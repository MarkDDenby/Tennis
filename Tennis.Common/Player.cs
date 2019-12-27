using System;
using Tennis.Contracts;

namespace Tennis
{
    public class Player : IPlayer
    {
        public string Name { get; private set; }
        public Boolean Advantage { get; set; } // should be part of scoring system

        public Player(String name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }
            this.Name = name;
        }
    }
}
