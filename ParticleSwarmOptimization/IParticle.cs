using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParticleSwarmOptimization
{
    public interface IParticle<DataType>
    {
        double Fitness { get; }
        DataType Model { get; }
        List<DataType> History { get; }

        void Step(IParticle<DataType> target);
        bool Overlaps(IParticle<DataType> target);
        bool TooClose(IParticle<DataType> target, double distance);
        void Orbit(IParticle<DataType> target, double radius);
    }
}
