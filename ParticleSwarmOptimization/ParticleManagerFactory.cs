using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleSwarmOptimization
{
    public static class ParticleManagerFactory
    {
        public static ParticleManager<DataType> Create<DataType>(IEnumerable<IParticle<DataType>> particles, double tooClose)
        {
            return new ParticleManager<DataType>(particles, tooClose);
        }
    }
}
