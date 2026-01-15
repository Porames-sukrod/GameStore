using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Api.Entites
{
    public class Game
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public int GenerId { get; set; }

        public Gener? Gener { get; set; }

        public decimal Price { get; set; }

        public DateTime ReleaseDate { get; set; }

    }
}