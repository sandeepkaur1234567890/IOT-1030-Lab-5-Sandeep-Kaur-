using IOT_1030_Lab5_Sandeep_Kaur.Particles;
using IOT_1030_Lab5_Sandeep_Kaur.Geometry2D;
using System;

namespace IOT_1030_Lab5_Sandeep_Kaur.ModelComponents
{
	public interface ISurface
	{
		Cell HandlePhonon(Phonon p);
	}

	/// <summary>
	/// When a phonon collides with a boundary surface, it undergoes specular reflection
	/// and is redirected back into its current cell
	/// </summary>
	public class BoundarySurface : ISurface
	{
		public SurfaceLocation Location { get; }
		protected Cell cell;

		public BoundarySurface(SurfaceLocation location, Cell cell)
		{
			Location = location;
			this.cell = cell;
		}
		public virtual Cell HandlePhonon(Phonon p)
		{
			Vector direction = p.Direction;
			if (Location == SurfaceLocation.left || Location == SurfaceLocation.right)
			{
				p.SetDirection(-direction.DX, direction.DY);
			}
			else
			{
				p.SetDirection(direction.DX, -direction.DY);
			}
			return cell;
		}
	}

	public class TransitionSurface : BoundarySurface
	{
		//TODO: Implement constructor

		public TransitionSurface(SurfaceLocation location, Cell cell) : base(location, cell)
		{
		}

		/// <summary>
		/// When a phonon collides with a transition surface, it passes from
		/// one cell to the adjacent cell. This method updates the phonon's coordinates
		/// to be on the border of the new cell. The new cell is returned.
		/// </summary>
		/// <param name="p">The phonon that will be moved from one cell to another</param>
		/// <returns>The new cell in which the phonon will reside</returns>
		public override Cell HandlePhonon(Phonon p)
		{
			//TODO: Implement
			throw new NotImplementedException();
		}
	}

	public class EmitSurface : BoundarySurface
	{
		private readonly double emitEnergy;
		public Tuple<double, double>[] EmitTable { get; }
		public double Temp { get; }
		public int EmitPhonons { get; private set; }
		public double EmitPhononsFrac { get; private set; }
		public EmitSurface(SurfaceLocation location, Cell cell, double temp) : base(location, cell)
		{
			Temp = temp;
			EmitTable = cell.EmitData(temp, out emitEnergy);
		}

		/// <summary>
		/// If a phonon collides with an emitting surface it leaves the system and
		/// is no longer part of the simulation
		/// </summary>
		/// <param name="p">The phonon that will be removed from the simulation</param>
		/// <returns>The cell the phonon resided in prior to being removed from the simulation</returns>
		public override Cell HandlePhonon(Phonon p)
		{
			// TODO: implement
			throw new NotImplementedException();
		}
		public double GetEmitEnergy(double tEq, double simTime, double length)
		{
			return emitEnergy * length * simTime / 4 * Math.Abs(Temp - tEq);
		}

		/// <summary>
		/// Sets the number of phonons this surface will emit during each simulation time step
		/// </summary>
		/// <param name="tEq">The equilibrium temperature of the system</param>
		/// <param name="effEnergy">The effective energy of each phonon packet</param>
		/// <param name="timeStep">The simulation time step</param>
		/// <param name="length">The dimension of the surface (1D here)</param>
		public void SetEmitPhonons(double tEq, double effEnergy, double timeStep, double length)
		{
			double emitPhonons = GetEmitEnergy(tEq, timeStep, length) / effEnergy;
			//TODO: Implement -> need to split the double emitPhonons into it's
			// integer component (EmitPhonons) and its
			// fractional component (EmitPhononsFrac)
		}
	}
}
