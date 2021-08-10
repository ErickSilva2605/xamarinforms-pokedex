using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PokedexXF.Interfaces
{
    public interface IInitializeAsync
    {
        Task Initialization { get; }
    }
}
